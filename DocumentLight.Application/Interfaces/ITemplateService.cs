using Common.Core.TemplateModels;
using DocumentLight.Application.Models.Requests;
using DocumentLight.Application.Models.Responses;
using DocumentLight.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLight.Application.Interfaces
{
    public interface ITemplateService
    {
        IEnumerable<TemplateResponse> GetTemplates();
        Task<TemplateResponse> MakeTemplateAsync(Guid templateId, PdfTemplate request);
        Task<TemplateResponse> UploadTemplateAsync(UploadAsyncRequest request);
    }
}
