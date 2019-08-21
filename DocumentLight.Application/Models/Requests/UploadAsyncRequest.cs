using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLight.Application.Models.Requests
{
    public class UploadAsyncRequest
    {
        public Guid TemplateId { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public File File { get; set; }
    }

    public class File
    {
        public IFormFile FormFile { get; set; }
    }
}
