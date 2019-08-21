using DocumentLight.System.Models.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLight.System.Interfaces
{
    public interface IFileSystem
    {
        Tuple<string, string> GetPhysicalPathAndMimeType(string filename);
        Tuple<string, string> GetPhysicalPathAndMimeType(Guid fieldId);

        Tuple<byte[], string> GetPhysicalFileAndFileName(string filename);
        Tuple<byte[], string> GetPhysicalFileAndFileName(Guid fieldId);

        Task<FileResponse> SaveFileAsync(IFormFile file);
        Task<FileResponse> SaveFileAsync(Stream fileStream, string filename);
        Task<FileResponse> SaveFileAsync(byte [] file, string filename);

        Task<FileResponse> UpdateFileAsync(Guid fileId, IFormFile file);
        Task<FileResponse> UpdateFileAsync(Guid fileId, Stream fileStream);
        Task<FileResponse> UpdateFileAsync(Guid fileId, byte[] byteArray);

        string GetLink(string filename);
        string GetLink(Guid fileId);
        Task DeleteFileAsync(Guid id);
    }
}
