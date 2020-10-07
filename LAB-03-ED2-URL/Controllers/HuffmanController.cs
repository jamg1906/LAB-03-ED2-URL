using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                var filePath = Path.GetTempFileName();
                if (file != null)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else { return Ok("No file was posted"); }

                return Ok("Success");
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
