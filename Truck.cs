using System.Diagnostics.Contracts;

namespace Parking_Lot_Sys
{
    // Truck is a ChildClass of Vehicle
    public class Truck : Vehicle
    {
        public Truck(string licensePlate) : base(licensePlate)
        {
            VehicleType = "Truck";
            LicensePlate = licensePlate;
            EntryTime = DateTime.Now;
            
        }
        
    }
}