namespace Asos.FashionEmergency.Web.Controllers
{
    using Microsoft.Azure.Documents.Spatial;
    using System.Collections.Generic;
    public class Product
    {
        public Dictionary<string, string> StoreOpeningHours;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        
        public int Availability { get; set; }

        public string ImageUrl { get; set; }

        public string StoreId { get; set; }

        public string StoreName { get; set; }
        public Dictionary<string, string> openingHours { get; set; }

        public string StorePostCode { get; set; }

        public string Category { get; set; }

        public string Floor { get; set; }
    }
}