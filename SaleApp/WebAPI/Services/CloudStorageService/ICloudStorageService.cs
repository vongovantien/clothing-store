using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using WebAPI.Utils.ConfigOptions;

namespace WebAPI.Services.CloudStorageService
{
    public interface ICloudStorageService
    {
        Task<string> GetSignedUrlAsync(string fileNameToRead, int timeOutInMinutes = 30);
        Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave);
        Task DeleteFileAsync(string fileNameToDelete);
    }
}
