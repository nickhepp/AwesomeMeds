namespace AwesomeMeds.Scheduling.DataContracts
{
    public class AppointmentSlot
    {

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public AppointmentSlotQuerterHourSegment QuarterHourSegment { get; set; }

        public AppointmentSlot()
        {
        }

        public AppointmentSlot(DateTime dateTime)
        {
            if ((dateTime.Minute % 15) != 0)
            {
                throw new ArgumentException("Time is not a 15 minute increment.");
            }
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
            Hour = dateTime.Hour;
            QuarterHourSegment = (AppointmentSlotQuerterHourSegment)(dateTime.Minute / 15);
        }


        public DateTime GetDateTimeUTC()
        {
            return new DateTime(Year, Month, Day, Hour, minute: (int)QuarterHourSegment * 15, second:0, DateTimeKind.Utc);
        }

        public override bool Equals(object? other)
        {
            if (other != null && other is AppointmentSlot otherApptSlot)
            {
                return this.GetDateTimeUTC() == otherApptSlot.GetDateTimeUTC();
            }
            return false;
        }

        public override int GetHashCode()
        {
            return GetDateTimeUTC().GetHashCode();
        }
    }
}