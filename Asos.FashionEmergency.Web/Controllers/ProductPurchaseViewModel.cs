using System.ComponentModel.DataAnnotations;
using Asos.FashionEmergency.Web.Models;

namespace Asos.FashionEmergency.Web.Controllers
{
    public class ProductPurchaseViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string CreditCardNumber { get; set; }
        public int ProductId { get; set; }
        public TimeSlotData TimeSlotInfo { get; set; }

        [Display(Name = "Delivery Time Slot")]
        public string SelectedTimeSlotId { get; set; }
    }
}