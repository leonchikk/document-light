using DocumentLight.Core.Interfaces;
using DocumentLight.System.Helpers;
using DocumentLight.System.Interfaces;
using DocumentLight.System.Models.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = DocumentLight.Core.Entities.File;
using FileIO = System.IO.File;

namespace DocumentLight.System.Services
{
    public class FileSystem : IFileSystem
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _accessor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileSystem(IUnitOfWork unitOfWork, IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _accessor = accessor;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task DeleteFileAsync(Guid id)
        {
            var file = _unitOfWork.FilesRepository.GetById(id);

            if (file == null)
                throw new Exception("That file doesn't exist");

            var physicalFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, file.RelativePath);
            FileIO.Delete(physicalFilePath);

            await _unitOfWork.FilesRepository.DeleteAsync(file.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<FileResponse> SaveFileAsync(IFormFile uploadedFile)
         {
            var fileData = CreatePathAndFile(uploadedFile.FileName);
            var file = fileData.Item2;
            var absoluteFilePath = fileData.Item1;

            await FileSystemHelper.WriteToDiskAsync(uploadedFile, absoluteFilePath);
            await SaveFileToRepositoryAsync(file);
            return GetFileResponse(file);
        }

        public async Task<FileResponse> SaveFileAsync(Stream stream, string fileName)
        {
            var fileData = CreatePathAndFile(fileName);
            var file = fileData.Item2;
            var absoluteFilePath = fileData.Item1;

            await FileSystemHelper.WriteToDiskAsync(stream, fileData.Item1);
            await SaveFileToRepositoryAsync(fileData.Item2);
            return GetFileResponse(fileData.Item2);
        }

        public async Task<FileResponse> SaveFileAsync(byte[] byteArray, string name)
        {
            var fileData = CreatePathAndFile(name);
            var file = fileData.Item2;
            var absoluteFilePath = fileData.Item1;

            await FileSystemHelper.WriteToDiskAsync(byteArray, absoluteFilePath);
            await SaveFileToRepositoryAsync(file);
            return GetFileResponse(file);
        }

        public string GetLink(string filename)
        {
            var file = _unitOfWork.FilesRepository.FindBy(x => x.FileName == filename).FirstOrDefault();

            if (file == null)
                throw new Exception("That file doesn't exist");

            return $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/api/files/{file.FileName}";
        }

        public string GetLink(Guid fileId)
        {
            var file = _unitOfWork.FilesRepository.FindBy(x => x.Id == fileId).FirstOrDefault();

            if (file == null)
                throw new Exception("That file doesn't exist");

            return $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/api/files/{file.FileName}";
        }

        public Tuple<string, string> GetPhysicalPathAndMimeType(string filename)
        {
            var file = _unitOfWork.FilesRepository.FindBy(x => x.FileName == filename).FirstOrDefault();
            return GetPhysicalPathAndMimeType(file);
        }

        public Tuple<string, string> GetPhysicalPathAndMimeType(Guid fieldId)
        {
            var file = _unitOfWork.FilesRepository.FindBy(x => x.Id == fieldId).FirstOrDefault();
            return GetPhysicalPathAndMimeType(file);
        }

        public Tuple<byte[], string> GetPhysicalFileAndFileName(string filename)
        {
            var file = _unitOfWork.FilesRepository.FindBy(x => x.FileName == filename).FirstOrDefault();
            return GetPhysicalFileAndFileName(file);
        }

        public Tuple<byte[], string> GetPhysicalFileAndFileName(Guid fieldId)
        {
            var file = _unitOfWork.FilesRepository.FindBy(x => x.Id == fieldId).FirstOrDefault();
            return GetPhysicalFileAndFileName(file);
        }

        public async Task<FileResponse> UpdateFileAsync(Guid fileId, IFormFile formFile)
        {
            var file = _unitOfWork.FilesRepository.FindBy(x => x.Id == fileId).FirstOrDefault();

            if (file == null)
                throw new Exception("That file doesn't exist");

            var absoluteFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, file.RelativePath);
            await FileSystemHelper.WriteToDiskAsync(formFile, absoluteFilePath);
            return GetFileResponse(file);
        }

        public async Task<FileResponse> UpdateFileAsync(Guid fileId, Stream stream)
        {
            var file = _unitOfWork.FilesRepository.FindBy(x => x.Id == fileId).FirstOrDefault();

            if (file == null)
                throw new Exception("That file doesn't exist");

            var absoluteFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, file.RelativePath);
            await FileSystemHelper.WriteToDiskAsync(stream, absoluteFilePath);
            return GetFileResponse(file);
        }

        public async Task<FileResponse> UpdateFileAsync(Guid fileId, byte[] fileArray)
        {
            var file = _unitOfWork.FilesRepository.FindBy(x => x.Id == fileId).FirstOrDefault();

            if (file == null)
                throw new Exception("That file doesn't exist");

            var absoluteFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, file.RelativePath);
            await FileSystemHelper.WriteToDiskAsync(fileArray, absoluteFilePath);
            return GetFileResponse(file);
        }

        private Tuple<byte[], string> GetPhysicalFileAndFileName(File file)
        {
            if (file == null)
                throw new Exception("That file doesn't exist");

            var physicalFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, file.RelativePath);
            return new Tuple<byte[], string>(FileIO.ReadAllBytes(physicalFilePath), file.FileName);
        }

        private Tuple<string, string> GetPhysicalPathAndMimeType(File file)
        {
            if (file == null)
                throw new Exception("That file doesn't exist");

            var physicalFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, file.RelativePath);

            return new Tuple<string, string>(physicalFilePath, MimeTypes.GetMimeType(Path.GetFileName(file.RelativePath)));
        }

        private Tuple<string, File> CreatePathAndFile(string filename)
        {
            var salt = Guid.NewGuid().ToString();
            var saltFileName = $"{Path.GetFileNameWithoutExtension(filename)}_{Path.Combine(HashHelper.GetHash(salt) + Path.GetExtension(filename))}";

            var relativeFileDirectory = Path.Combine("wwwroot", "files");
            var absoluteFileDirectoryPath = Path.Combine(_hostingEnvironment.ContentRootPath, relativeFileDirectory);

            var relativeFilePath = Path.Combine(relativeFileDirectory, saltFileName);
            var absoluteFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, relativeFilePath);

            if (!Directory.Exists(absoluteFileDirectoryPath))
                Directory.CreateDirectory(absoluteFileDirectoryPath);

            var file = new File()
            {
                Id = Guid.NewGuid(),
                RelativePath = relativeFilePath,
                FileName = saltFileName
            };

            return new Tuple<string, File>(absoluteFilePath, file);
        }

        private async Task SaveFileToRepositoryAsync(File file)
        {
            _unitOfWork.FilesRepository.Add(file);
            await _unitOfWork.SaveAsync();
        }

        private FileResponse GetFileResponse(File file)
        {
            return new FileResponse
            {
                Id = file.Id,
                Link = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/api/files/{file.FileName}"
            };
        }
    }
}
