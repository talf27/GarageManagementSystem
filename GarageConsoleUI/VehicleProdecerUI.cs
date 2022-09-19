using System;
using System.Collections.Generic;
using GarageLogic;

namespace ConsoleUI
{
    internal class VehicleProducerUI
    {
        internal static void CreateNewVehicle()
        {
            bool tryCreating = true, trySetting = true;
            Factory.eVehicleTypes vehicleType;
            Vehicle vehicle = null;
            string[] vehicleProperties;

            Console.WriteLine("Please choose your required vehicle by its number:");
            foreach(Factory.eVehicleTypes type in Enum.GetValues(typeof(Factory.eVehicleTypes)))
            {
                Console.WriteLine(string.Format("{0}: {1}", (int)type, type.ToString()));
            }

            while(tryCreating)
            {
                while(!Enum.TryParse<Factory.eVehicleTypes>(Console.ReadLine(), out vehicleType))
                {
                    Console.Write("Please enter a valid number to choose the vehicle's type: ");
                }

                try
                {
                    vehicle = Factory.CreateVehicle(vehicleType);
                    tryCreating = false;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write("Please enter a valid number to choose the vehicle's type: ");
                }
            }

            Console.WriteLine(string.Format(
@"Please provide your requested properties for the new vehicle separated by ','.
you should give {0}", VehicleProducer.GetPropertiesOfVehicle(vehicle)));
            while(trySetting)
            {
                vehicleProperties = Console.ReadLine().Split(',');
                while(VehicleProducer.IsVehicleExist(vehicleProperties[0]))
                {
                    Console.Write("There is already a vehicle with this license number. Please give another one: ");
                    vehicleProperties[0] = Console.ReadLine();
                }

                try
                {
                    VehicleProducer.SetPropertiesOfVehicle(vehicle, vehicleProperties);
                    trySetting = false;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.Write("Try again: ");
                }
            }

            Console.WriteLine();
            initAirPressuresInVehicle(vehicle);
            Console.WriteLine();
            initEnergyInVehicle(vehicle);
            Console.WriteLine($"{Environment.NewLine}A new vehicle was produced successfully!");
            Console.WriteLine(string.Format("{0}{1}", VehicleProducer.VehicleInfo(vehicle), Environment.NewLine));
        }

        private static void initAirPressuresInVehicle(Vehicle i_Vehicle)
        {
            string[] airPressuresAsStrings;
            int currentAirPressure;
            LinkedList<int> airPressures = new LinkedList<int>();
            bool canInflateTires = false;

            Console.Write("Inflate the vehicle's tires. Provide air pressure to inflate for each tire (separated by ','): ");
            while(!canInflateTires)
            {
                airPressuresAsStrings = Console.ReadLine().Split(',');
                if(airPressuresAsStrings.Length == i_Vehicle.GetNumOfTires())
                {
                    foreach(string airPressureAsString in airPressuresAsStrings)
                    {
                        if(canInflateTires = int.TryParse(airPressureAsString, out currentAirPressure))
                        {
                            airPressures.AddLast(currentAirPressure);
                        }
                        else
                        {
                            Console.Write("Please enter only numbers. Try again: ");
                            break;
                        }
                    }
                }
                else
                {
                    Console.Write("Please provide air pressure for each of the tires. Try again: ");
                }
            }

            try
            {
                VehicleProducer.InitTiresAirPressure(i_Vehicle, airPressures);
            }
            catch(ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                initAirPressuresInVehicle(i_Vehicle);
            }
        }

        private static void initEnergyInVehicle(Vehicle i_Vehicle)
        {
            float energyToAdd;

            Console.Write("Put energy in the vehicle. Provide float number: ");
            while(!float.TryParse(Console.ReadLine(), out energyToAdd))
            {
                Console.Write("Please provide a float number: ");
            }

            try
            {
                VehicleProducer.InitEnergy(i_Vehicle, energyToAdd);
            }
            catch(ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                initEnergyInVehicle(i_Vehicle);
            }
        }
    }
}
