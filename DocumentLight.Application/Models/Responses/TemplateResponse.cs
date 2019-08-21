using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLight.Application.Models.Responses
{
    public class TemplateResponse
    {
        public string Title { get; set; }
        public string Thumbnail{ get; set; }
        public Guid TemplateId { get; set; }
        public Guid FileId { get; set; }
        public string Link { get; set; }
    }
}
