namespace Parking_Lot_Sys
{
    //Bill is created to to hold the values of the bills issued at the time of checout
    public class Bill
    {
        public string LicensePlate { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public double AmountDue { get; set; }

        public Bill(string licensePlate, DateTime entryTime, DateTime exitTime, double amountDue)
        {
            LicensePlate = licensePlate;
            EntryTime = entryTime;
            ExitTime = exitTime;
            AmountDue = amountDue;

        }
    }
}