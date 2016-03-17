using System.ComponentModel.DataAnnotations;
using Asos.FashionEmergency.Web.Models;

namespace Asos.FashionEmergency.Web.Controllers
{
    public class ProductPurchaseViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public int ProductId { get; set; }

        public TimeSlotData TimeSlotInfo { get; set; }

        [Display(Name = "Delivery Time Slot")]
        public string SelectedTimeSlotId { get; set; }

        public decimal DeliveryPrice { get; set; }

        public decimal ProductPrice { get; set; }
    }
}