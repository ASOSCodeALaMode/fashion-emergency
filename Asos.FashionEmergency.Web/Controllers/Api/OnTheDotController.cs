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
            return this.Json(AvailableTimeSlotsData(storeId, postcode));
        }

        private TimeSlotData AvailableTimeSlotsData(string storeId, string postcode)
        {
            var data = new TimeSlotRequest
                           {
                               store = new Store { storeId = storeId },
                               consumer =
                                   new ConsumerPostCode { address = new Address { postCode = postcode } },
                               items =
                                   new Items
                                       {
                                           deliveryDate = this.dateForDelivery.ToString("yyyy-MM-dd"),
                                           readyAt = this.dateForDelivery.ToString("s") + "Z"
                                       }
                           };

            var request = new RestRequest(ApiTimeSlots, Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer {0}", ApiKey));
            request.AddHeader("Channel", "ECOM");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);

            var client = new RestClient(ApiUrl);
            var response = client.Execute<TimeSlotResponse>(request);

            return response.Data.data;
        }

        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult CreateBooking(string storeId, string postCode, string timeslotId, string uuid)
        {
            return Json(CreateBookingData(storeId, postCode, timeslotId, uuid));
        }

        public bool CreateBookingData(string storeId, string postCode, string timeslotId, string uuid)
        {
            var data = new CreateBookingRequest
            {
                consumer =
                    new Consumer
                    {
                        address =
                            new Address
                            {
                                city = "London",
                                postCode = postCode,
                                firstLine = "11 giberrish"
                            },
                        name = "Test Name",
                        mobileNumber = "7777777777"
                    },
                item = new DeliveryItems { itemContentCount = 1 },
                orderType = "1",
                store = new Store { storeId = storeId },
                supplierId = "CitySprint",
                timeslot = new TimeSlotBase { timeslotId = timeslotId }
            };

            var request = new RestRequest(ApiBooking, Method.POST);
            request.AddHeader("Authorization", string.Format("Bearer {0}", ApiKey));
            request.AddHeader("Channel", "ECOM");
            request.AddHeader("UUID", uuid);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);

            var client = new RestClient(ApiUrl);
            var response = client.Execute<CreateBookingResponse>(request);

            return response.Data.data.status == "Booked";
        }
    }

    public class CreateBookingResponse
    {
        public CreateBookingData data { get; set; }
    }

    public class CreateBookingData
    {
        public string status { get; set; }
    }

    public class TimeSlotRequest
    {
        public Store store { get; set; }

        public ConsumerPostCode consumer { get; set; }

        public Items items { get; set; }
    }

    public class CreateBookingRequest
    {
        public TimeSlotBase timeslot { get; set; }

        public string orderType { get; set; }

        public string supplierId { get; set; }

        public Store store { get; set; }

        public Consumer consumer { get; set; }

        public DeliveryItems item { get; set; }

    }

    public class DeliveryItems
    {
        public int itemContentCount { get; set; }
    }

    public class TimeSlotResponse
    {
        public TimeSlotData data { get; set; }
    }

    public class Items
    {
        public string readyAt { get; set; }

        public string deliveryDate { get; set; }
    }

    public class ConsumerPostCode
    {
        public PostCode address { get; set; }
    }

    public class Consumer
    {
        public string name { get; set; }

        public string mobileNumber { get; set; }

        public Address address { get; set; }
    }

    public class Address : PostCode
    {
        public string firstLine { get; set; }

        public string city { get; set; }
    }

    public class PostCode
    {
        public string postCode { get; set; }
    }

    public class Store
    {
        public string storeId { get; set; }
    }
}