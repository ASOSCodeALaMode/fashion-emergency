namespace Asos.FashionEmergency.Web.Controllers
{
    public class ProductPurchaseViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string CreditCardNumber { get; set; }
        public int DeliveryHour { get; set; }
        public int ProductId { get; set; }
    }
}