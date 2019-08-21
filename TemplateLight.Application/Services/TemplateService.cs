using Common.Core.TemplateModels;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common.Core.Extensions;
using TemplateLight.Core.Interfaces;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using iTextSharp.text;

namespace TemplateLight.Core.Services
{
    public class TemplateService : ITemplateService
    {
        public byte[] MakePdfTemplate(IFormFile file, Common.Core.TemplateModels.PdfTemplate template)
        {
            var templateByteArray = file.ConvertToByteArray();
            var pdfReader = new PdfReader(templateByteArray);

            using (var pdfStream = new MemoryStream())
            {
                var pdfStamper = new PdfStamper(pdfReader, pdfStream);

                // Clear all old fields
                pdfStamper.AcroFields.Fields.Clear();

                // Handle the fields
                foreach (var field in template.Fields)
                {
                    // Text field
                    if (field.FieldType == "/Tx")
                    {
                        var textField = new TextField(pdfStamper.Writer, new iTextSharp.text.Rectangle(field.Rect.X1, field.Rect.Y1, field.Rect.X2, field.Rect.Y2), field.FieldName)
                        {
                            Text = field.FieldValue
                        };

                        if (field.IsReadOnly)
                            textField.Options |= TextField.READ_ONLY;

                        if (field.IsMultiLine)
                            textField.Options |= TextField.MULTILINE;

                        if (field.IsRequired)
                            textField.Options |= TextField.REQUIRED;

                        pdfStamper.AddAnnotation(textField.GetTextField(), field.PageNum);
                    }
                }
                
                pdfStamper.Close();
                pdfReader.Close();

                return pdfStream.ToArray();
            }
        }
    }
}
