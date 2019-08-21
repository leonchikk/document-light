using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLight.Core.Entities
{
    public class File: BaseEntity
    {
        public string RelativePath { get; set; }
        public string FileName { get; set; }
    }
}
