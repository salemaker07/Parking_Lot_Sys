namespace Parking_Lot_Sys
{
    // Here is the ChildClass Motorcycle
    public class Motorcycle : Vehicle
    {
        public Motorcycle(string licensePlate) : base(licensePlate)
        {
            VehicleType = "Motorcycle";
            LicensePlate = licensePlate;
            EntryTime = DateTime.Now;
        }
    }
}