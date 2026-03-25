namespace Parking_Lot_Sys
{
   public class ConsoleHelper
    {
        //this method display a massage and assign a value to the string var
        public string Prompt(string userPrompt)
        {
            Console.WriteLine(userPrompt);
            string? input = Console.ReadLine(); 
            return input ?? string.Empty; 
        }
        // same but for var of type int
        public int PromptInt(string userPrompt)
        {
            Console.WriteLine(userPrompt);
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Please input a valid integer:");
            }
            return result;
        }
        // void func to disply Menue
        public void DisplayMenu()
        {
            //removed this line as it's noisy to be repeated and moved to before while loop in Program.cs
   //         Console.WriteLine("\n === Welcome to Asal's Parking Lot System! ===\n");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Park a vehicle");
            Console.WriteLine("2. Check out a vehicle");
            Console.WriteLine("3. View parked vehicles");
            Console.WriteLine("4. View bills");
            Console.WriteLine("5. filter vehicles by type");
            Console.WriteLine("6. Exit"); 
        }
        
    } 
}