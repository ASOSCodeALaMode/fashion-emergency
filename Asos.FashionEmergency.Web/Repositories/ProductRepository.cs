using System.Collections.Generic;
using System.Linq;
using Asos.FashionEmergency.Web.Controllers;

namespace Asos.FashionEmergency.Web.Repositories
{
    using System;

    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;
    using System.Text.RegularExpressions;
    using Models;
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
                           Availability = Availability(product.Boutique)
            };
        }
        public int Availability(BoutiqueDb boutique) {
            // calculates how soon a product can be delivered

            // set up
            int waitMinutes, maxWaitHours;
            string weekday = DateTime.Now.ToString("ddd").ToUpper();
            TimeSpan currentTimeOfDay = DateTime.Now.TimeOfDay;

            // Calculate opening times and last order time
            var storeOpeningTimeDetails = OpeningTimeDetails(boutique.Info.openingHours[weekday]);
            TimeSpan openingTimeToday = storeOpeningTimeDetails.openingOffset;
            TimeSpan lastOrderTime = storeOpeningTimeDetails.closingOffset
                .Subtract(TimeSpan.FromMinutes(boutique.Info.CollectionLeadTime))
                .Subtract(TimeSpan.FromMinutes(boutique.Info.BranchInStoreTime))
                .Subtract(TimeSpan.FromMinutes(DeliveryLeadTime));
            
            // Case where store is not yet open
            if (currentTimeOfDay < openingTimeToday)
            {
                waitMinutes = (int)((openingTimeToday - currentTimeOfDay).TotalMinutes
                + boutique.Info.CollectionLeadTime + boutique.Info.BranchInStoreTime
                + DeliveryLeadTime);
            }
            // case where it is too late to collect from store
            // set to max of 14 hours
            else if (currentTimeOfDay > lastOrderTime)
            {
                waitMinutes = 840;
            }
            // sweet spot where store is open and delivery is available ASAP
            else {
                waitMinutes = (int)(boutique.Info.BranchInStoreTime
                + boutique.Info.CollectionLeadTime
                + DeliveryLeadTime);
            }

            // Turn wait minutes in to hours and 
            maxWaitHours = (int)Math.Ceiling(waitMinutes / 60.0);

            return maxWaitHours;

        }

        private OpeningTimeDetails OpeningTimeDetails(string openingHours) {
            // Parses opening time strings e.g. "10:00 - 18:00" in to time span offsets
            var openingTimeDetails = new OpeningTimeDetails { }; // initiate object to return

            // set up regex
            string pat = "^([0-9]{2}):([0-9]{2}) - ([0-9]{2}):([0-9]{2})$";
            Regex r = new Regex(pat);
            Match m = r.Match(openingHours);

            // map returned regex groups to variables
            int openingHour = Convert.ToInt32(m.Groups[1].Value);
            int openingMinute = Convert.ToInt32(m.Groups[2].Value);
            int closingHour = Convert.ToInt32(m.Groups[3].Value);
            int closingMinute = Convert.ToInt32(m.Groups[4].Value);

            // calculate timespans
            openingTimeDetails.openingOffset = TimeSpan.FromHours(openingHour).Add(TimeSpan.FromSeconds(openingMinute));
            openingTimeDetails.closingOffset = TimeSpan.FromHours(closingHour).Add(TimeSpan.FromSeconds(closingMinute));

            return openingTimeDetails; // return results
        }
    }
}