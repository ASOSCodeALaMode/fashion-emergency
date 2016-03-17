namespace Asos.FashionEmergency.Web.Controllers.Api
{
    using System.Web.Http;

    public class ProductDataController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Json(new ProductRepository().GetProductsForPostCode("Dummy Postcode"));
        }
    }
}