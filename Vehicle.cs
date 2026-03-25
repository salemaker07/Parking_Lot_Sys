
using System;
using System.Collections.Generic;

namespace Parking_Lot_Sys
{
    //Here the Parent class Vehicle, Type attribute will be given a value in the subclasses inhereted.
    // The EntryTime is assigned at the checkin of the Vehicle.
    public class Vehicle
    {
        public string LicensePlate { get; set;}
        public DateTime EntryTime { get; set;}
        public string VehicleType { get; set; } = string.Empty;
        

        

    public Vehicle(string licensePlate)
        {
            LicensePlate = licensePlate;
            EntryTime = DateTime.Now;
            

        }
        
    }
}