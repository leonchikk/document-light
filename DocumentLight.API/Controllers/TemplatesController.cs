using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentLight.Core.Interfaces;
using DocumentLight.Application.Interfaces;
using DocumentLight.Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Common.Core.TemplateModels;

namespace DocumentLight.API.Controllers
{
    [Route("api/[controller]")]
    public class TemplatesController : BaseController
    {
        private readonly ITemplateService _templateService;

        public TemplatesController (ITemplateService template)
        {
            _templateService = template;
        }

        [HttpGet]
        public IActionResult GetTemplates()
        {
            return Ok(_templateService.GetTemplates());
        }

        [HttpPut("make-template/{templateId}")]
        public async Task<IActionResult> TemplateUploadAsync(Guid templateId, [FromBody] PdfTemplate template)
        {
            return Ok(await _templateService.MakeTemplateAsync(templateId, template));
        }

        [HttpPost("template-upload-async")]
        public async Task<IActionResult> TemplateUploadAsync(UploadAsyncRequest request)
        {
            return Ok(await _templateService.UploadTemplateAsync(request));
        }
    }
}
