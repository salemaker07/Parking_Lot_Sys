using System;
// Exception is a microsoft built in Class.
//This exception will be used when no Park space available(check in is not available)
namespace Parking_Lot_Sys
{
    public class ParkingLotException : Exception
    {
        public ParkingLotException(string message) : base(message) { }
    }
}