using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core.TemplateModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateLight.Core.Interfaces;

namespace TemplateLight.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class TemplatesController : Controller
    {
        private readonly ITemplateService _templateService;

        public TemplatesController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpPost("create-pdf-template")]
        public IActionResult CreatePdfTemplate(IFormFile file, [ModelBinder(BinderType = typeof(JsonModelBinder))] PdfTemplate pdfTemplate)
        {
            return File(_templateService.MakePdfTemplate(file, pdfTemplate), "application/pdf", "template.pdf");
        }
    }
}
