using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Core.TemplateModels
{
    public class Field
    {
        public int PageNum { get; set; }
        public string FieldType { get; set; }
        public string FieldValue { get; set; }
        public string FieldName { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsMultiLine { get; set; }
        public bool IsRequired { get; set; }
        public Rectangle Rect { get; set; }
        public string Subtype { get; set; }
    }
}
