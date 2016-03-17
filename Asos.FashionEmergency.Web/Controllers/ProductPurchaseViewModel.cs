using System.ComponentModel.DataAnnotations;
using Asos.FashionEmergency.Web.Models;

namespace Asos.FashionEmergency.Web.Controllers
{
    public class ProductPurchaseViewModel
    {
        [Required]
<<<<<<< HEAD
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public int ProductId { get; set; }
=======
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public int ProductId { get; set; }
>>>>>>> refs/remotes/origin/master

        public TimeSlotData TimeSlotInfo { get; set; }

        [Display(Name = "Delivery Time Slot")]
<<<<<<< HEAD
        public string SelectedTimeSlotId { get; set; }

        public decimal DeliveryPrice { get; set; }
=======
        public string SelectedTimeSlotId { get; set; }

        public decimal DeliveryPrice { get; set; }
>>>>>>> refs/remotes/origin/master

        public decimal ProductPrice { get; set; }
    }
}