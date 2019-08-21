using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentLight.System.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentLight.API.Controllers
{
    [Route("api/[controller]")]
    public class FilesController : BaseController
    {
        private readonly IFileSystem _fileSystem;

        public FilesController(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        [HttpGet("{filename}")]
        public IActionResult GetFile(string filename)
        {
            var fileInfo = _fileSystem.GetPhysicalPathAndMimeType(filename);
            return PhysicalFile(fileInfo.Item1, fileInfo.Item2);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            return Ok(await _fileSystem.SaveFileAsync(file));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _fileSystem.DeleteFileAsync(id);
            return Ok();
        }
    }
}
