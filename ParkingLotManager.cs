using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Parking_Lot_Sys
{
    //inhareting multifaces 
    public class ParkingLotManager : IBillable, INotifiable
    {
        // 2 events
        public event LotFullHandler? OnLotFull;
        public event OverstayHandler? OnOverstay;
        // 2 lists for Vehicle and Bills to serialze to and deserialize from json files
        private List<Vehicle> _parkedVehicles = new List<Vehicle>();
        private List<Bill> _bills = new List<Bill>();
        //path to json files
        private string _filePath = "ParkingLotLog.json";
        private string _billFilePath = "Bills.json";
        // I chose the number 6 to make it easier for testing (20 is to much for testing events and exceptions)
        public int NumberOfSpots = 6;

        //mainly used to create an nstance of class in the Program class and retrive data from json
        public ParkingLotManager()
        {
            loadParkingLot();
            loadBills();
        }

        //hourly rate can not be manipulate or changed from outside.
        private double GetHourlyRate(Vehicle vehicle)
        {
            return vehicle.VehicleType switch
            {
                "Car" => 5.0,
                "Motorcycle" => 3.0,
                "Truck" => 8.0,
                _ => throw new InvalidOperationException("Unknown Type of Vehicle")
            };
        }
       //deserialize from json if exists
        public void loadParkingLot()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                _parkedVehicles = JsonSerializer.Deserialize<List<Vehicle>>(json) ?? new List<Vehicle>(); 
            }
        }
        // serialize vehicle parked to json
        public void saveParkingLot()
        {
            string json = JsonSerializer.Serialize(_parkedVehicles, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        // serialize bills to json
        public void saveBills()
        {
            string json = JsonSerializer.Serialize(_bills, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_billFilePath, json);
        }

        public void loadBills()
        {
            if (File.Exists(_billFilePath))
            {
                string json = File.ReadAllText(_billFilePath);
                _bills = JsonSerializer.Deserialize<List<Bill>>(json) ?? new List<Bill>();
            }
        }
    // Calculate time to calculate the fees, seprate func for cleaner code for probal change of code, eg.printing reports
        public double CalculateTimeParked(Vehicle vehicle)
        {
             TimeSpan timeParked = DateTime.Now - vehicle.EntryTime;
             return timeParked.TotalHours;
        }
        //using the funcs of CalculateTimeParked and GetHourlyRate
        public double CalculateParkingFee(Vehicle vehicle)
        {
            return CalculateTimeParked(vehicle) * GetHourlyRate(vehicle);
        }
        // /* ******main func to park vehicle ***********
                public void ParkVehicle(Vehicle vehicle)
        {
            // Throw an exception if they try to park in a full lot
            if (_parkedVehicles.Count >= NumberOfSpots)
            {
                throw new ParkingLotException("Cannot park: The parking lot is at full capacity!");
            }
            //check if vehicle is not already parked
            if (!_parkedVehicles.Any(v => v.LicensePlate == vehicle.LicensePlate))
            {
                _parkedVehicles.Add(vehicle);
                saveParkingLot(); 
                Console.WriteLine($"{vehicle.VehicleType} with plate {vehicle.LicensePlate} parked successfully.");

                // invoke event if the park is full
                if (_parkedVehicles.Count == NumberOfSpots)
                {
                    OnLotFull?.Invoke(this, EventArgs.Empty);
                }
            } 
            else
            {
                throw new ParkingLotException("Cannot park: Vehicle is already in the lot.");
            }
        }
        //this func will be used at checkout
        private void generateBill(Vehicle vehicle)
        {
            Bill bill = new Bill(vehicle.LicensePlate, vehicle.EntryTime, DateTime.Now, CalculateParkingFee(vehicle));
            _bills.Add(bill);
        }
        
        public void CheckOutVehicle(string licensePlate)
        {
            //FirstOrDefult is used even though can not be repeated due to our coding for check in
           var vehicle = _parkedVehicles.FirstOrDefault(v => v.LicensePlate == licensePlate);
           // exception if not found
           if (vehicle == null)
           {
               throw new ParkingLotException($"Vehicle with license plate '{licensePlate}' not found.");
           }

            double fee = CalculateParkingFee(vehicle);
            Console.WriteLine($"Parking fee for {vehicle.LicensePlate}: {fee}");
            
            
            
            /*while true, it came to my mind if the user input yea by mistake it will cancle the operation, i apply it for
            this part only as its not neccessary */
            while(true)
            {
                Console.WriteLine("Do you want to pay? (yes/no)");
                string? input = Console.ReadLine();
                if(input.ToLower() == "yes")
                {
                   generateBill(vehicle);
                _parkedVehicles.Remove(vehicle);
                saveBills();
                saveParkingLot(); 
                Console.WriteLine("Payment successful. Thank you!"); 
                break;
                }else if (input.ToLower() == "no")
                {
                    Console.WriteLine("Payment cancelled. Please pay before leaving.");
                    break;
                }
                else if (input.ToLower() =="exit" )
                {
                    Console.WriteLine("invalid option please tr again");
                }
            }
        }
        // func to veiw parked vehicle if exist 
        public void ViewParkedVehicles()
        {
            if (_parkedVehicles.Count == 0)
            {
                // if no parked vehicle
                Console.WriteLine("No Vehicles are parked!");
                return;
            }

            Console.WriteLine("Parked Vehicles (Sorted by Entry Time):");
            //first sort by time
            var sortedVehicles = _parkedVehicles.OrderBy(v => v.EntryTime).ToList();

            foreach (var v in sortedVehicles)
            {
                // RUBRIC REQUIREMENT: Safe Type Casting using Pattern Matching
                if (v is Car c) 
                {
                    Console.WriteLine($"[CAR] Plate: {c.LicensePlate}, Entry: {c.EntryTime}");
                }
                else if (v is Truck t)
                {
                    Console.WriteLine($"[TRUCK] Plate: {t.LicensePlate}, Entry: {t.EntryTime}");
                }
                else if (v is Motorcycle m)
                {
                    Console.WriteLine($"[MOTO] Plate: {m.LicensePlate}, Entry: {m.EntryTime}");
                }
                else
                {
                    Console.WriteLine($"[UNKNOWN] Plate: {v.LicensePlate}, Entry: {v.EntryTime}");
                }
            
            }
        }
        //retrive bill from list 
        public void ViewBills() 
        {
            if (_bills.Count == 0)
            {
                Console.WriteLine("No bills generated!");
            }
            else
            {
                Console.WriteLine("Generated Bills:");
                foreach(var bill in _bills)
                {
                    Console.WriteLine($"License Plate: {bill.LicensePlate}, Entry Time: {bill.EntryTime}, Exit Time: {bill.ExitTime}, Amount Due: {bill.AmountDue:C}");
                }
            }
        }

        public void FilterVehiclesByType(string type)
        {
            // Fixed: Changed Type to VehicleType
            // https://learn.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings I stole this expression from here
            
            var filteredVehicles = _parkedVehicles.Where(v => v.VehicleType.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();

            if (filteredVehicles.Count == 0)
            {
                Console.WriteLine($"No {type}s currently parked.");
            }
            else 
            {
                Console.WriteLine($"--- Parked {type}s ---");
                foreach (var v in filteredVehicles)
                {
                    Console.WriteLine($"License Plate: {v.LicensePlate}, Entry Time: {v.EntryTime}");
                }
            }
        }
        public void CheckForOverstays()
        {
            foreach (var vehicle in _parkedVehicles)
            {
                // Let's define an overstay as more than 24 hours
                if (CalculateTimeParked(vehicle) > 24) 
                {
                    
                    OnOverstay?.Invoke(this, vehicle);
                }
            }
        }
    }
}