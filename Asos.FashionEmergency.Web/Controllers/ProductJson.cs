namespace Asos.FashionEmergency.Web.Controllers
{
    using System.Collections.Generic;

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
    }

    public class BoutiqueDb
    {
        public string Id { get; set; }
        public Info Info { get; set; }
    }
}