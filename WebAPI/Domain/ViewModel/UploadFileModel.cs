using Microsoft.AspNetCore.Http;

namespace Domain.ViewModel
{
    public class UploadFileModel
    {
        public IFormFile file { get; set; }
        public string? SavedUrl { get; set; }
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }
    }
}
