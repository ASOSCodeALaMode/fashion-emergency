using System;
using System.Web.Mvc;

namespace Asos.FashionEmergency.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository productRepository = new ProductRepository();

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
}