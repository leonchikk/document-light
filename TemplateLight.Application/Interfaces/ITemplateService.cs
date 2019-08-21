using Common.Core.TemplateModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateLight.Core.Interfaces
{
    public interface ITemplateService
    {
        byte[] MakePdfTemplate(IFormFile file, PdfTemplate template);
    }
}
