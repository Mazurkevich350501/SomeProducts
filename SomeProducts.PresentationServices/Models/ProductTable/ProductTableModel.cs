namespace SomeProducts.PresentationServices.Models.ProductTable
{
    public class ProductTableModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public string Color { get; set; }
        
        public int Quantity { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }
        
    }
}
