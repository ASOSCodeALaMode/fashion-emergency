using System.Collections.Generic;
using System.Linq;
using Asos.FashionEmergency.Web.Controllers;

namespace Asos.FashionEmergency.Web.Repositories
{
    using System;

    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;

    public class ProductRepository
    {
        private const string EndpointUrl = "https://asosemergencies.documents.azure.com:443/";

        private const string AuthorizationKey = "LFr9pLZTX8amDkwy6ZkGhU5f83tcknifOZL2SvabT+Py+pLzuy/iVGy5Gk26pGh3nCIyhoXAOboz0v+X6fNfwQ==";

        private const string DatabaseName = "AsosEmergencies";

        private const string ServiceCentreLngLat = "-0.12804000000005544, 51.4926642";

        private const string ServiceCentreMaxDistance = "16.09344 * 1000";

        private const double DeliveryLeadTime = 120.0;

        //private readonly List<Product> products = new List<Product>
        //{
        //    new Product
        //    {
        //        Id = 1,
        //        StoreId = "Asos-02",
        //        Name = "Red t-shirt",
        //        Description = "A nice red t-shirt",
        //        Price = 29.99m,
        //        Availability = 2,
        //        ImageUrl = "http://images.asos-media.com/inv/media/7/3/5/7/5967537/red/image1xl.jpg"
        //    },
        //    new Product
        //    {
        //        Id = 2,
        //        StoreId = "Asos-04",
        //        Name = "Green t-shirt",
        //        Description = "A nice green t-shirt",
        //        Price = 19.99m,
        //        Availability = 1,
        //        ImageUrl = "http://images.asos-media.com/inv/media/3/0/0/3/5173003/khaki/image1xl.jpg"
        //    },
        //    new Product
        //    {
        //        Id = 3,
        //        StoreId = "Asos-05",
        //        Name = "Blue t-shirt",
        //        Description = "A nice blue t-shirt",
        //        Price = 39.99m,
        //        Availability = 4,
        //        ImageUrl = "http://images.asos-media.com/inv/media/2/4/1/6/6216142/blue/image1xl.jpg"
        //    }
        //};

        public IList<Product> GetProductsForPostCode(string postcode)
        {
            Database database;
            DocumentCollection productCollection;
            var client = DocumentDbClient(out database, out productCollection);
            var productList = new List<Product>();

            //WHERE ST_DISTANCE(p.boutique.location, { " + "\"type\": \"Point\", "
            //        + "\"coordinates\": [" + ServiceCentreLngLat + "] " + "}) < " + ServiceCentreMaxDistance

            foreach (var product in
                client.CreateDocumentQuery<ProductDb>(
                    "dbs/" + database.Id + "/colls/" + productCollection.Id,
                    "SELECT * FROM product p "))
            {
                productList.Add(MapProduct(product));
            }

            client.Dispose();
            
            return productList.OrderBy(x => x.Availability).ToList();
        }

        public Product GetProductById(int id)
        {
            Database database;
            DocumentCollection productCollection;
            var client = DocumentDbClient(out database, out productCollection);

            var product =
                MapProduct(
                    client.CreateDocumentQuery<ProductDb>(
                        "dbs/" + database.Id + "/colls/" + productCollection.Id,
                        "SELECT * FROM product p WHERE p.id = \"" + id + "\"").AsEnumerable().FirstOrDefault());

            client.Dispose();

            return product;
        }

        private DocumentClient DocumentDbClient(out Database database, out DocumentCollection productCollection)
        {
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
            database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();
            productCollection = null;
            if (database != null)
            {
                productCollection =
                    client.CreateDocumentCollectionQuery("dbs/" + database.Id)
                        .Where(c => c.Id == "product")
                        .AsEnumerable()
                        .FirstOrDefault();
            }

            return client;
        }

        private Product MapProduct(ProductDb product)
        {
            return new Product
                       {
                           Id = int.Parse(product.Id),
                           Name = product.ItemName,
                           Description = product.ItemDescription,
                           ImageUrl = product.Images.FirstOrDefault(),
                           Price = product.Price,
                           StoreId = product.Boutique.Id,
                           StoreName = product.Boutique.Info.StoreName,
                           StorePostCode = product.Boutique.Address.postCode,
                           Availability =
                               (int)
                               Math.Ceiling(
                                   (product.Boutique.Info.BranchInStoreTime
                                    + product.Boutique.Info.CollectionLeadTime + DeliveryLeadTime) / 60.0)
                       };
        }
    }
}