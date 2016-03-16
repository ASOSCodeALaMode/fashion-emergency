namespace Asos.FashionEmergency.Web.Models
{
    using System;
    using System.Collections.Generic;

    public class Data
    {
        public List<TimeSlot> timeslots { set; get; }
    }

    public class TimeSlot
    {
        public string timeslotId { set; get; }

        public DateTime startTime { set; get; }
    }
}