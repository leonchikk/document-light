using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.TemplateModels
{
    public class PdfTemplate
    {
        public string Title { get; set; }
        public IEnumerable<Field> Fields { get; set; }
    }
}
