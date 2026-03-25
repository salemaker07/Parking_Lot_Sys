namespace Parking_Lot_Sys
{
    // To calculate bill Time is also need to calculate.
    public interface IBillable
    {
        public double CalculateTimeParked(Vehicle vehicle);
        double CalculateParkingFee(Vehicle vehicle);
    }
}