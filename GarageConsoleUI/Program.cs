using System;
using GarageLogic;

namespace ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            Run();
        }

        internal static void Run()
        {
            Garage garage = new Garage();
            bool showOptions = true;
            int userChoice;

            Console.WriteLine("Hi, welcome to our garage! First, produce a vehicle.");
            VehicleProducerUI.CreateNewVehicle();
            while(showOptions)
            {
                Console.WriteLine(
@"Please choose your action (by number):
1 - create new vehicle
2 - go to garage
3 - exit");
                while(!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 1 || userChoice > 3)
                {
                    Console.Write("The choice's number can be 1-3. Try again: ");
                }

                if(userChoice == 1)
                {
                    Console.Clear();
                    VehicleProducerUI.CreateNewVehicle();
                }
                else if(userChoice == 2)
                {
                    Console.Clear();
                    GarageOperations.ShowGarageOptions(garage);
                }
                else
                {
                    Console.WriteLine("GoodBye. Enjoy your day!");
                    showOptions = false;
                }
            }
        }
    }
}


