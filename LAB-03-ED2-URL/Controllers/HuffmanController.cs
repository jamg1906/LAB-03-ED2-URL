using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using System.Threading;
using LAB_03_ED2_URL.Models;
using System.Text.Json;

namespace LAB_03_ED2_URL.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HuffmanController : ControllerBase
    {
        [HttpPost("compress/{name}")]
        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file, [FromRoute] string name)
        {
            try
            {
                var filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\NonCompressed\\" + file.FileName);
                if (file != null)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else { return StatusCode(500); }
                Compression.CompressFile(filePath, file.FileName, name);
                FileStream Sender = new FileStream(Directory.GetCurrentDirectory() + "\\Compressed\\" + name + ".huff", FileMode.OpenOrCreate);
                return File(Sender, "text/plain", name + ".huff");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("decompress")]

        public async Task<IActionResult> OnPostUploadAsync([FromForm] IFormFile file)
        {
            try
            {
                string ogName = "";
                int x = file.FileName.Length - 1;
                int howMany = 0;
                while (file.FileName[x] != '.')
                {
                    howMany++;
                    x--;
                }
                for (int i = 0; i < file.FileName.Length-howMany-1; i++)
                {
                    ogName += file.FileName[i];
                }
                var filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\" + ogName + ".huff");
                if (file != null)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else { return StatusCode(500); }
                //Aquí se debería mandar esa ruta o string al compresor, obtener la ruta del descomprimido y regresarlo aquí.
                return Ok("OK");
            }
            catch
            {
                return StatusCode(500);
            }

        }

        [HttpGet("compressions")]
        
        public ActionResult GetCompressionsJSON()
        {
            var Registries = Compression.GetAllCompressions();
            if (Registries != null)
            {
                return Created("", Registries);
            }
            return StatusCode(500);
        }

    }
}
