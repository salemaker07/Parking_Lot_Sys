namespace Parking_Lot_Sys
{
// Here is the ChildClass Car
    public class Car : Vehicle
    {
        public Car(string licensePlate) : base(licensePlate )
        {
            VehicleType = "Car";
            LicensePlate = licensePlate;
            EntryTime = DateTime.Now;
        }
    }
}