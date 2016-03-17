using System;
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
        public ActionResult Index(string postcode)
        {
            return RedirectToAction("ViewProducts", new { postcode = postcode.Replace(" ", String.Empty).ToUpper() });
        }

        [HttpGet]
        public ActionResult ViewProducts(string postcode, string category = "")
        {
            return View(productRepository.GetProductsForPostCode(postcode));
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
            var store = storeRepository.GetStoreById(product.StoreId);

            return View(model: new OrderCompleteViewModel { StoreName = store.Name, StorePostcode = store.Postcode, DestinationPostcode = postcode });
        }
    }
}