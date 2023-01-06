using Domain.Enum;
using Domain.Models;

namespace WebAPI.Services
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData, FileType fileType);

        public Task PostMultiFileAsync(List<FileUploadModel> fileData);

        public Task DownloadFileById(int fileName);
    }
}
