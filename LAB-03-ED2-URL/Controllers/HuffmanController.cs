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
                string CompressedName = name;
                var filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\" + file.FileName);
                if (file != null)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else { return StatusCode(500); }
                //Aquí se debería mandar esa ruta o string al compresor, obtener la ruta del comprimido y regresarlo aquí.
                return Ok("OK");
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
                var filePath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\" + file.Name + ".huff");
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
            return Created("", Compression.GetAllCompressions());
        }

    }
}
