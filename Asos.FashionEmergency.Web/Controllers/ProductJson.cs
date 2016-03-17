namespace Asos.FashionEmergency.Web.Controllers
{
    using System.Collections.Generic;

    using Asos.FashionEmergency.Web.Controllers.Api;

    using Microsoft.Azure.Documents.Spatial;

    public class ProductDb
    {
        public string Id { get; set; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<string> Images { get; set; }

        public BoutiqueDb Boutique { get; set; }
    }

    public class Info
    {
        public int BranchInStoreTime { get; set; }
        
        public int CollectionLeadTime { get; set; }

        public string StoreName { get; set; }
        public Dictionary<string, string> openingHours { get; set; }
    }

    public class BoutiqueDb
    {
        public string Id { get; set; }

        public Info Info { get; set; }

        public PostCode Address { get; set; }
    }
}