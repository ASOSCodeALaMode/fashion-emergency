using System.Collections.Generic;
using System.Linq;

namespace Asos.FashionEmergency.Web.Controllers
{
    public class ProductRepository
    {
        private readonly List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                StoreId = 2,
                Name = "Red t-shirt",
                Description = "A nice red t-shirt",
                Price = 29.99m,
                Availability = 2,
                ImageUrl = "http://images.asos-media.com/inv/media/7/3/5/7/5967537/red/image1xl.jpg"
            },
            new Product
            {
                Id = 2,
                StoreId = 4,
                Name = "Green t-shirt",
                Description = "A nice green t-shirt",
                Price = 19.99m,
                Availability = 1,
                ImageUrl = "http://images.asos-media.com/inv/media/3/0/0/3/5173003/khaki/image1xl.jpg"
            },
            new Product
            {
                Id = 3,
                StoreId = 5,
                Name = "Blue t-shirt",
                Description = "A nice blue t-shirt",
                Price = 39.99m,
                Availability = 4,
                ImageUrl = "http://images.asos-media.com/inv/media/2/4/1/6/6216142/blue/image1xl.jpg"
            }
        };

        public IList<Product> GetProductsForPostCode(string postcode)
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            return products.First(p => p.Id == id);
        }
    }
}