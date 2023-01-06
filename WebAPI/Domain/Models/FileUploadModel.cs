using Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public class FileUploadModel
    {
        public IFormFile FileDetails { get; set; }
        public FileType FileType { get; set; }
    }
}
