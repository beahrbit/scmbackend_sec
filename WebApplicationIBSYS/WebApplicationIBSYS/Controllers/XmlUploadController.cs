using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationIBSYS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmlUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public XmlUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class XmlUpload
        {
            public IFormFile files { get; set; }
        }

        [HttpPost]
        public async Task<string> Post ([FromForm] XmlUpload objFile)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\Upload\\" + objFile.files.FileName;
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
