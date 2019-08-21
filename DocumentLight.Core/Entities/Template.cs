using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLight.Core.Entities
{
    public class Template: BaseEntity
    {
        private Template() { }
        public Template(Guid id, string title, string thumbnail, File file)
        {
            Id = id;
            Title = title;
            File = file;
            Thumbnail = thumbnail;
        }

        public File File { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
    }
}
