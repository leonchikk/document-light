using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLight.System.Helpers
{
    public static class FileSystemHelper
    {
        public static async Task WriteToDiskAsync(IFormFile file, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 4192, true))
                await file.CopyToAsync(fileStream);
        }

        public static async Task WriteToDiskAsync(Stream stream, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 4192, true))
                await stream.CopyToAsync(fileStream);
        }

        public static async Task WriteToDiskAsync(byte[] byteArray, string path)
        {
            await File.WriteAllBytesAsync(path, byteArray);
        }
    }
}
