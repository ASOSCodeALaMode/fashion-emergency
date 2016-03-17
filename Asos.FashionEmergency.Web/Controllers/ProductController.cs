using System;
using System.Linq;
using System.Web.Mvc;
using Asos.FashionEmergency.Web.Controllers.Api;
using Asos.FashionEmergency.Web.Repositories;

namespace Asos.FashionEmergency.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository productRepository = new ProductRepository();
        private readonly StoreRepository storeRepository = new StoreRepository();
        private readonly OnTheDotController bookingController = new OnTheDotController();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string postcode, string floor = "-", string category = "")
        {
            return RedirectToAction("ViewProducts", new { postcode = postcode.Replace(" ", String.Empty).ToUpper(), category, floor });
        }

        [HttpGet]
        public ActionResult ViewProducts(string postcode, string floor = "-", string category = "")
        {
            var allProducts = productRepository.GetProductsForPostCode(postcode);
            var categories = allProducts.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();
            var floors = allProducts.Select(p => p.Floor).Distinct().Concat(new[] { "-" }).OrderBy(c => c).ToList();
            var displayProducts = category == "" ? allProducts : allProducts.Where(p => p.Category == category).ToList();
            displayProducts = floor == "-" ? displayProducts : displayProducts.Where(p => p.Floor == floor).ToList();

            return View(new ViewProductsViewModel { Products = displayProducts, Categories = categories, Floors = floors });
        }

        [HttpGet]
        public ActionResult ViewProduct(string postcode, int productId)
        {
            return View(productRepository.GetProductById(productId));
        }

        [HttpGet]
        public ActionResult BuyProduct(string postcode, int productId)
        {
            var product = productRepository.GetProductById(productId);

            var timeslots = bookingController.AvailableTimeSlotsData(product.StoreId, postcode);

            return View(new ProductPurchaseViewModel
            {
                ProductId = productId,
                ProductPrice = product.Price,
                DeliveryPrice = 6.99m,
                PostCode = ViewBag.PostCode,
                TimeSlotInfo = timeslots
            });
        }

        [HttpPost]
        public ActionResult BuyProduct(ProductPurchaseViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = productRepository.GetProductById(model.ProductId);

            bookingController.CreateBookingData(
                product.StoreId,
                model.Name,
                model.Address,
                model.PostCode,
                model.SelectedTimeSlotId,
                model.TimeSlotInfo.uuid);

            return RedirectToAction("OrderComplete");
        }

        [HttpGet]
        public ActionResult OrderComplete(string postcode, int productId)
        {
            var product = productRepository.GetProductById(productId);

            return View(model: new OrderCompleteViewModel { StoreName = product.StoreName, StorePostcode = product.StorePostCode, DestinationPostcode = postcode });
        }
    }
}