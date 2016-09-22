namespace SomeProducts.CrossCutting.Interfaces
{
    public interface IImageModel
    {
        byte[] Image { get; set; }

        string ImageType { get; set; }
    }
}
