namespace Asos.FashionEmergency.Web.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;
    using System.Web.Http;

    public class ProductDataController : ApiController
    {
        private const string EndpointUrl = "https://codealamode.documents.azure.com/";
        private const string AuthorizationKey = "fZyFP5K9LswHv0o1/tfQ2fDh6SkRvrLxJ15tkXs7oKFR7HN/ZneMwq799bMlwIyc9ZQ3+M6InQB4xgyMNIZH8w==";
        private const string ServicePostcode = "EC2A 4PH";

        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Json(GetData());
        }

        [AllowAnonymous]
        [HttpGet]
        private object GetData()
        {
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);   
            var database =
                client.CreateDatabaseQuery().Where(db => db.Id == "marketplace").AsEnumerable().FirstOrDefault();
            var productCollection =
                client.CreateDocumentCollectionQuery("dbs/" + database.Id)
                    .Where(c => c.Id == "product")
                    .AsEnumerable()
                    .FirstOrDefault();

            var products = client.CreateDocumentQuery(
                "dbs/" + database.Id + "/colls/" + productCollection.Id,
                "SELECT * " + "FROM product p ").ToList();

            //var products = client.CreateDocumentQuery(
            //    "dbs/" + database.Id + "/colls/" + productCollection.Id,
            //    "SELECT * " + "FROM product p " + "WHERE ST_DISTANCE(p.Location, { " + "\"type\": \"Point\", "
            //    + "\"coordinates\": [-0.08101320000002943, 51.5233512] " + "}) < 100 * 1000");

            return products;
        }
    }
}