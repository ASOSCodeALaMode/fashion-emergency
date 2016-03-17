using System.Collections.Generic;

namespace Asos.FashionEmergency.Web.Controllers
{
    public class ViewProductsViewModel
    {
        public IList<Product> Products { get; set; }

        public IList<string> Categories { get; set; }

        public IList<string> Floors { get; set; }

        public string Category { get; set; }

        public string Floor { get; set; }
    }
}