namespace Asos.FashionEmergency.Web.Models
{
    using System;
    using System.Collections.Generic;

    public class TimeSlotData
    {
        public string uuid { set; get; }

        public List<TimeSlot> timeslots { set; get; }
    }

    public class TimeSlot : TimeSlotBase
    {
        public DateTime startTime { set; get; }
    }

    public class TimeSlotBase
    {
        public string timeslotId { set; get; }
    }
}