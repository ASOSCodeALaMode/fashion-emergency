using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Asos.FashionEmergency.Web.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string postcode)
        {
            return RedirectToAction("ViewProducts", new { postcode = postcode.Replace(" ", String.Empty).ToUpper() });
        }

        [HttpGet]
        public ActionResult ViewProducts(string postcode, string category = "")
        {
            var dummyProducts = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Red t-shirt",
                    Description = "A nice red t-shirt",
                    Price = 29.99m,
                    Availability = 2,
                    ImageUrl = "http://images.asos-media.com/inv/media/7/3/5/7/5967537/red/image1xl.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Green t-shirt",
                    Description = "A nice green t-shirt",
                    Price = 19.99m,
                    Availability = 1,
                    ImageUrl = "http://images.asos-media.com/inv/media/3/0/0/3/5173003/khaki/image1xl.jpg"
                },
                new Product 
                {
                    Id = 3,
                    Name = "Blue t-shirt",
                    Description = "A nice blue t-shirt",
                    Price = 39.99m,
                    Availability = 4,
                    ImageUrl = "http://images.asos-media.com/inv/media/2/4/1/6/6216142/blue/image1xl.jpg"
                }
            };

            return View(dummyProducts);
        }

        [HttpGet]
        public ActionResult ViewProduct(string postcode, int productId)
        {
            return View(new Product
            {
                Id = productId,
                Name = "Blue t-shirt",
                Description = "A nice blue t-shirt",
                Price = 39.99m,
                Availability = 4,
                ImageUrl = "http://images.asos-media.com/inv/media/2/4/1/6/6216142/blue/image1xl.jpg"
            });
        }

        [HttpGet]
        public ActionResult BuyProduct(string postcode, int productId)
        {
            return View(new ProductPurchaseViewModel { ProductId = productId, PostCode = ViewBag.PostCode });
        }

        [HttpPost]
        public ActionResult BuyProduct(ProductPurchaseViewModel model)
        {
            if (ModelState.IsValid) return RedirectToAction("OrderComplete");

            return View(model);
        }

        [HttpGet]
        public ActionResult OrderComplete()
        {
            return View();
        }
    }

    public class ProductPurchaseViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string CreditCardNumber { get; set; }
        public int DeliveryHour { get; set; }
        public int ProductId { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Availability { get; set; }
        public string ImageUrl { get; set; }
    }
}