namespace WebAPI.Helpers
{
    public class ReflectionHelper
    {
        public static Boolean isImage(IFormFile image)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (image.ContentType.ToLower() != "image/jpg" &&
                        image.ContentType.ToLower() != "image/jpeg" &&
                        image.ContentType.ToLower() != "image/pjpeg" &&
                        image.ContentType.ToLower() != "image/gif" &&
                        image.ContentType.ToLower() != "image/x-png" &&
                        image.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(image.FileName).ToLower() != ".jpg"
                && Path.GetExtension(image.FileName).ToLower() != ".png"
                && Path.GetExtension(image.FileName).ToLower() != ".gif"
                && Path.GetExtension(image.FileName).ToLower() != ".jpeg")
            {
                return false;
            }
            return true;

        }
    }
}
