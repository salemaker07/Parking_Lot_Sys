namespace Parking_Lot_Sys

//Notification will be used to notify for lack of Lot and overstaying

{
    public delegate void LotFullHandler(object sender, EventArgs e);
    public delegate void OverstayHandler(object sender, Vehicle vehicle);
    interface INotifiable
    {
        event LotFullHandler? OnLotFull;
        event OverstayHandler? OnOverstay;
        

        
         
    }
}