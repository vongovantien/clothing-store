
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.CloudStorageService;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly myDBContext _context;
        private readonly ICloudStorageService _cloudStorageService;

        public UploadController(myDBContext context, ICloudStorageService cloudStorageService)
        {
            _context = context;
            _cloudStorageService = cloudStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file)
        {
            // START: Handling file upload to GCS
            if (file != null)
            {
                GenerateFileNameToSave(file.FileName);
                var result = await _cloudStorageService.UploadFileAsync(file, "tiendeptrai");
            }
            // END: Handling file upload to GCS
            //_context.Add(animal);
            //await _context.SaveChangesAsync();
            return Ok();
        }

        private string GenerateFileNameToSave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        }
    }
}
