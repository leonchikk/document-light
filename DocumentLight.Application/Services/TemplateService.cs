using DocumentLight.Core.Entities;
using DocumentLight.Core.Interfaces;
using DocumentLight.Application.Interfaces;
using DocumentLight.Application.Models.Requests;
using DocumentLight.Application.Models.Responses;
using DocumentLight.System.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Common.Core.Extensions;
using Newtonsoft.Json;
using DocumentLight.Application.Settings;
using Common.Core.TemplateModels;
using AutoMapper;

namespace DocumentLight.Application.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileSystem _fileSystem;
        private readonly TemplatesApiSettings _settings;

        public TemplateService(IUnitOfWork unitOfWork, IFileSystem fileSystem, TemplatesApiSettings settings, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileSystem = fileSystem;
            _settings = settings;
            _mapper = mapper;
        }

        public IEnumerable<TemplateResponse> GetTemplates()
            => _mapper.Map<IEnumerable<TemplateResponse>>(_unitOfWork.TemplatesRepository.GetAllWithIncludies(x => x.File));

        public async Task<TemplateResponse> MakeTemplateAsync(Guid templateId, PdfTemplate requestedTemplate)
        {
            var template = _unitOfWork.TemplatesRepository.GetByIdWithIncludies(templateId, x => x.File);

            var fileTuple = _fileSystem.GetPhysicalFileAndFileName(template.File.Id);
            var fileByteArray = fileTuple.Item1;
            var fileName = fileTuple.Item2;

            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(new MemoryStream(fileByteArray)), "file", fileName);
                    content.Add(new StringContent(JsonConvert.SerializeObject(requestedTemplate), Encoding.UTF8), "pdfTemplate");

                    using (var requestToService = await client.PostAsync($"{_settings.Host}:{_settings.Port}/api/templates/create-pdf-template", content))
                    {
                        var receivedTemplate = await requestToService.Content.ReadAsStreamAsync();
                        var savedFileInfo = await _fileSystem.UpdateFileAsync(template.File.Id, receivedTemplate);
                        
                        return new TemplateResponse
                        {
                            FileId = savedFileInfo.Id,
                            TemplateId = template.Id,
                            Thumbnail = template.Thumbnail,
                            Title = template.Title,
                            Link = _fileSystem.GetLink(savedFileInfo.Id)
                        };
                    }
                }
            }
        }

        public async Task<TemplateResponse> UploadTemplateAsync(UploadAsyncRequest request)
        {
            var savedFile = await _fileSystem.SaveFileAsync(request.File.FormFile);
            var file = _unitOfWork.FilesRepository.GetById(savedFile.Id);
            var template = new Template(request.TemplateId, request.Title, request.Thumbnail, file);

            await _unitOfWork.TemplatesRepository.AddAsync(template);
            await _unitOfWork.SaveAsync();

            return new TemplateResponse
            {
                FileId = file.Id,
                TemplateId = template.Id,
                Thumbnail = template.Thumbnail,
                Title = template.Title,
                Link = _fileSystem.GetLink(file.Id)
            };
        }
    }
}
