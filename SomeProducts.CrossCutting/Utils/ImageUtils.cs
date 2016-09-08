using System.Web;
using SomeProducts.CrossCutting.Intefaces;

namespace SomeProducts.CrossCutting.Utils
{
    public static class ImageUtils
    {

        public static void AddImageToModel(IImageModel model, HttpRequestBase request)
        {
            if (request.Files.Count > 0)
            {
                var image = request.Files[0];
                if (image != null && image.ContentLength > 0)
                {
                    model.Image = new byte[image.ContentLength];
                    image.InputStream.Read(model.Image, 0, image.ContentLength);
                    model.ImageType = image.ContentType;
                }
            }
        }
    }
}
