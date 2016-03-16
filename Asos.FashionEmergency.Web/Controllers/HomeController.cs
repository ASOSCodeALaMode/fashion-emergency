using System.Collections.Generic;
using System.Web.Mvc;

namespace Asos.FashionEmergency.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewProducts(string postcode)
        {
            var dummyProducts = new List<Product>
            {
                new Product
                {
                    Name = "Red t-shirt",
                    Description = "A nice red t-shirt",
                    Price = 29.99m,
                    Availability = 2,
                    ImageUrl = "http://images.asos.com/image1.jpg"
                },
                new Product
                {
                    Name = "Green t-shirt",
                    Description = "A nice green t-shirt",
                    Price = 19.99m,
                    Availability = 1,
                    ImageUrl = "http://images.asos.com/image1.jpg"
                },
                new Product
                {
                    Name = "Blue t-shirt",
                    Description = "A nice blue t-shirt",
                    Price = 39.99m,
                    Availability = 4,
                    ImageUrl = "http://images.asos.com/image1.jpg"
                }
            };

            return View(dummyProducts);
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Availability { get; set; }
        public string ImageUrl { get; set; }
    }
}