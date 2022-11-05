
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
                //GenerateFileNameToSave(file.FileName);
                await _cloudStorageService.UploadFileAsync(file, "tiendeptrai");
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

        //[HttpGet("{documentName}")]
        //public IActionResult GetDocumentFromS3(string documentName)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(documentName))
        //            return ReturnMessage("The 'documentName' parameter is required", (int)HttpStatusCode.BadRequest);

        //        var _aws3Services = new Aws3Services(_appConfiguration.AwsAccessKey, _appConfiguration.AwsSecretAccessKey, _appConfiguration.AwsSessionToken, _appConfiguration.Region, _appConfiguration.BucketName);

        //        var document = _aws3Services.DownloadFileAsync(documentName).Result;

        //        return File(document, "application/octet-stream", documentName);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private IActionResult ReturnMessage(string v, int badRequest)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpPost]
        //public IActionResult UploadDocumentToS3(IFormFile file)
        //{
        //    try
        //    {
        //        if (file is null || file.Length <= 0)
        //            return ReturnMessage("file is required to upload", (int)HttpStatusCode.BadRequest);

        //        var _aws3Services = new Aws3Services(_appConfiguration.AwsAccessKey, _appConfiguration.AwsSecretAccessKey, _appConfiguration.AwsSessionToken, _appConfiguration.Region, _appConfiguration.BucketName);

        //        var result = _aws3Services.UploadFileAsync(file);

        //        return ReturnMessage(string.Empty, (int)HttpStatusCode.Created);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ReturnMessage(ex.Message, (int)HttpStatusCode.InternalServerError);
        //    }
        //}

        //[HttpDelete("{documentName}")]
        //public IActionResult DeletetDocumentFromS3(string documentName)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(documentName))
        //            return ReturnMessage("The 'documentName' parameter is required", (int)HttpStatusCode.BadRequest);

        //        var _aws3Services = new Aws3Services(_appConfiguration.AwsAccessKey, _appConfiguration.AwsSecretAccessKey, _appConfiguration.AwsSessionToken, _appConfiguration.Region, _appConfiguration.BucketName);

        //        _aws3Services.DeleteFileAsync(documentName);

        //        return ReturnMessage(string.Format("The document '{0}' is deleted successfully", documentName));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private IActionResult ReturnMessage(string v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
