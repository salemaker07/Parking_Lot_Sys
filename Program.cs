using System;

namespace Parking_Lot_Sys
{
    class Program 
    {
        static void Main(string[] args)
        {   
            ParkingLotManager manager = new ParkingLotManager();
            ConsoleHelper helper = new ConsoleHelper();

            // SUBSCRIBE TO EVENTS: Tell the program what to do when events are fired
            manager.OnLotFull += (sender, e) => 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[ALERT] The parking lot has reached maximum capacity!");
                 Console.ResetColor();

            };

            manager.OnOverstay += (sender, vehicle) => 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[WARNING] Vehicle {vehicle.LicensePlate} has overstayed (24+ hours).");
                Console.ResetColor();
            };
            Console.WriteLine("\n === Welcome to Asal's Parking Lot System! ===\n");

            while(true)
            {
                helper.DisplayMenu();
                string choice = helper.Prompt("\nEnter your choice:"); 
                
                // EXCEPTION HANDLING: Wrap the switch statement in a try-catch block
                try 
                {
                    switch(choice)
                    {
                        case "1":
                            string type = helper.Prompt("Enter the type of vehicle (Car, Motorcycle, Truck):");
                            switch(type.ToLower())
                            {
                                case "car":
                                    type = "Car";
                                    manager.ParkVehicle(new Car(helper.Prompt("Enter the license plate number:")));
                                    break;
                                case "motorcycle":
                                    type = "Motorcycle";
                                    manager.ParkVehicle(new Motorcycle(helper.Prompt("Enter the license plate number:")));
                                    break;
                                case "truck":
                                    type = "Truck";
                                    manager.ParkVehicle(new Truck(helper.Prompt("Enter the license plate number:")));
                                    break;
                                default:
                                    Console.WriteLine("Invalid vehicle type.");
                                    break;
                            }
                            break;

                        case "2":
                            string checkoutLicensePlate = helper.Prompt("Enter the license plate number to check out:");
                            manager.CheckOutVehicle(checkoutLicensePlate);
                            break;

                        case "3":
                            // Check for overstays right before viewing the lot!
                            manager.CheckForOverstays();
                            manager.ViewParkedVehicles();
                            break;

                        case "4":
                            manager.ViewBills(); 
                            break;

                        case "5":
                            string filterType = helper.Prompt("Enter the type of vehicle to filter by:");
                            manager.FilterVehiclesByType(filterType);
                            break;

                        case "6":
                            Console.WriteLine("Exiting the system. Goodbye!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (ParkingLotException ex) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nError: {ex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex) 
                {
                    Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
                }
            }
        }
    }
}
