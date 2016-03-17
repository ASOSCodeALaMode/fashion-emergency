namespace Asos.FashionEmergency.Web.Controllers
{
    using Microsoft.Azure.Documents.Spatial;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        
        public int Availability { get; set; }

        public string ImageUrl { get; set; }

        public string StoreId { get; set; }

        public string StoreName { get; set; }

        public string StorePostCode { get; set; }

    }
}