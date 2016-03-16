namespace Asos.FashionEmergency.Web.Controllers.Api
{
    using System;
    using System.Web.Http;

    using Asos.FashionEmergency.Web.Models;

    using RestSharp;

    public class OnTheDotController : ApiController
    {

        private const string ApiKey = "_agdRUoCb9tAoldlD6vZD9Z3XzIa";

        private const string ApiUrl = "https://sbapi.onthedot.com/api/v1.0";

        private const string ApiTimeSlots = "timeslots";
        
        private const string ApiBooking = "booking";

        private readonly DateTime dateForDelivery = new DateTime(2016, 3, 17, 10, 55, 0);

        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult AvailableTimeSlots(string storeId, string postcode)
        {
            var data = new TimeSlotRequest
                           {
                               store = new Store { storeId = storeId },
                               consumer = new Consumer { address = new Address { postCode = postcode } },
                               items =
                                   new Items
                                       {
                                           deliveryDate = dateForDelivery.ToString("yyyy-MM-dd"),
                                           readyAt = dateForDelivery.ToString("s") + "Z" 
                                       }
                           };
            
            var client = new RestClient(ApiUrl);
            var request = new RestRequest(ApiTimeSlots, Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer {0}", ApiKey));
            request.AddHeader("Channel", "ECOM");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);
            var response = client.Execute<TimeSlotData>(request);

            return Json(response.Data.data);
        }
    }

    public class TimeSlotData
    {
        public Data data { get; set; }
    }

    public class TimeSlotRequest
    {
        public Store store { get; set; }
        public Consumer consumer { get; set; }
        public Items items { get; set; }
    }

    public class Items
    {
        public string readyAt { get; set; }
        public string deliveryDate { get; set; }
    }

    public class Consumer
    {
        public Address address { get; set; }
    }

    public class Address
    {
        public string postCode { get; set; }
    }

    public class Store
    {
        public string storeId { get; set; }
    }
}