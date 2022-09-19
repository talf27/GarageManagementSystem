using System;
using System.Collections.Generic;
using GarageLogic;
using static GarageLogic.Enums;

namespace ConsoleUI
{
    internal class GarageOperations
    {
        internal static void ShowGarageOptions(Garage i_Garage)
        {
            bool showOptions = true;
            int userChoice;

            while (showOptions)
            {
                Console.WriteLine(
                    @"Please enter your choice number:
1 - add existing vehicle to the garage
2 - show a list of license numbers of the garage's vehicles (with possibility to filter by vehicle's status)
3 - change a vehicle's status in the garage
4 - inflate vehicle's tires to maximum capacity
5 - refuel a fueled vehicle
6 - recharge an electric vehicle
7 - show full details about a vehicle in the garage
8 - back to the previous menu");

                while(!int.TryParse(Console.ReadLine(), out userChoice) && userChoice < 1 || userChoice > 8)
                {
                    Console.Write("The choice's number can be 1-8. Try again: ");
                }

                if(userChoice == 1)
                {
                    addExistingVehicleToGarage(i_Garage);
                }
                else if(userChoice == 2)
                {
                    getLicenseNumbersFromGarage(i_Garage);
                }
                else if(userChoice == 3)
                {
                    changeStatusInGarage(i_Garage);
                }
                else if(userChoice == 4)
                {
                    inflateTiresToMaxInGarage(i_Garage);
                }
                else if(userChoice == 5)
                {
                    refuelVehicleInGarage(i_Garage);
                }
                else if(userChoice == 6)
                {
                    rechargeVehicleInGarage(i_Garage);
                }
                else if(userChoice == 7)
                {
                    showFullDetailsAboutVehicleInGarage(i_Garage);
                }
                else
                {
                    Console.Clear();
                    showOptions = false;
                }
            }
        }

        private static void addExistingVehicleToGarage(Garage i_Garage)
        {
            string[] inputs;
            
            Console.Write("Please enter the license number, owner name and owner phone (separated by ','): ");
            try
            {
                checkNumOfParameters(out inputs, 3);
                VehicleProducer.AddToGarage(i_Garage, inputs[0], inputs[1], inputs[2]);
                Console.WriteLine($"Vehicle was added successfully to the garage.{Environment.NewLine}");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(string.Format("{0}{1}", ex.Message, Environment.NewLine));
            }
        }

        private static void getLicenseNumbersFromGarage(Garage i_Garage)
        {
            string[] inputs;
            LinkedList<string> licenseNumbers;
            LinkedList<eVehicleStatus> statuses = new LinkedList<eVehicleStatus>();

            Console.WriteLine(string.Format("If you want to filter by status, please enter the wanted ones separated by ',' (by number or by string - {0}). Else enter 'No': ", Enums.PrintEnumMembers(typeof(eVehicleStatus))));
            inputs = Console.ReadLine().Split(',');
            if(inputs[0] != "No")
            {
                statusesValidation(inputs, ref statuses);
            }

            licenseNumbers = i_Garage.GetLicenseNumbers(statuses);
            Console.WriteLine(string.Format("License numbers of the requested vehicles: {0}{1}", string.Join(", ", licenseNumbers), Environment.NewLine));
        }

        private static void statusesValidation(string[] i_Inputs, ref LinkedList<Enums.eVehicleStatus> io_StatusesToFilterBy)
        {
            string[] statusesToFilterByAsStrings = i_Inputs;
            bool isLegalFilters = false;
            object parsedEnum;

            while (!isLegalFilters)
            {
                foreach (string statusToFilterBy in statusesToFilterByAsStrings)
                {
                    if (!(isLegalFilters = ParseToEnumMember(typeof(Enums.eVehicleStatus), statusToFilterBy, out parsedEnum)))
                    {
                        Console.WriteLine("Hi");
                        Console.WriteLine("Please enter only 1-3 (separated by ',') if you want to filter. Try again: ");
                        break;
                    }

                    io_StatusesToFilterBy.AddLast((Enums.eVehicleStatus)parsedEnum);
                }

                if (!isLegalFilters)
                {
                    statusesToFilterByAsStrings = Console.ReadLine().Split(',');
                }
            }
        }

        private static void changeStatusInGarage(Garage i_Garage)
        {
            string[] inputs;
            object status;
            string errorMsg = "Please enter only 1-3 for the status: ";

            Console.WriteLine(String.Format("Please enter license number and a status (by number or by string - {0}) separated by ',': ", Enums.PrintEnumMembers(typeof(eVehicleStatus))));
            checkNumOfParameters(out inputs, 2);
            enumValidation(typeof(eVehicleStatus), ref inputs[1], out status, errorMsg);
            try
            {
                i_Garage.ChangeStatus(inputs[0], (eVehicleStatus)status);
                Console.WriteLine($"Vehicle was changed its status successfully.{Environment.NewLine}");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(string.Format("{0}{1}", ex.Message, Environment.NewLine));
            }
        }

        private static void inflateTiresToMaxInGarage(Garage i_Garage)
        {
            Console.Write("Please enter license number: ");
            try
            {
                i_Garage.InflateTiresToFull(Console.ReadLine());
                Console.WriteLine($"Vehicle's tires were inflated to maximum successfully.{Environment.NewLine}");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(string.Format("{0}{1}", ex.Message, Environment.NewLine));
            }
        }

        private static void refuelVehicleInGarage(Garage i_Garage)
        {
            string[] inputs;
            object fuelType;
            float fuelToAdd;
            string errorMsg = String.Format("Please enter a valid fuel type (by number or by string - {0} ): ", PrintEnumMembers(typeof(eFuelType)));
            
            Console.WriteLine(String.Format("Please enter license number, fuel type ({0}) and fuel amount to add (separated by ','): ", PrintEnumMembers(typeof(Enums.eFuelType))));
            checkNumOfParameters(out inputs, 3);
            enumValidation(typeof(eFuelType),ref inputs[1], out fuelType, errorMsg);
            checkEnergyValidityAsFloat(ref inputs[2], out fuelToAdd);
            try
            {
                i_Garage.RefuelVehicle(inputs[0], (eFuelType)fuelType, fuelToAdd);
                Console.WriteLine($"The vehicle was refueled successfully.{Environment.NewLine}");
            }
            catch(ValueOutOfRangeException ex)
            {
                Console.WriteLine(string.Format("{0}{1}", ex.Message, Environment.NewLine));
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(string.Format("{0}{1}", ex.Message, Environment.NewLine));
            }
        }

        private static void rechargeVehicleInGarage(Garage i_Garage)
        {
            string[] inputs;
            float hoursToAdd;

            Console.Write("Please enter license number, and number of hours to charge (separated by ','): ");
            checkNumOfParameters(out inputs, 2);
            checkEnergyValidityAsFloat(ref inputs[1], out hoursToAdd);
            try
            {
                i_Garage.RechargeVehicle(inputs[0], hoursToAdd);
                Console.WriteLine($"The vehicle was recharged successfully.{ Environment.NewLine}");
            }
            catch(ValueOutOfRangeException ex)
            {
                Console.WriteLine(string.Format("{0}{1}", ex.Message, Environment.NewLine));
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(string.Format("{0}{1}", ex.Message, Environment.NewLine));
            }
        }

        private static void showFullDetailsAboutVehicleInGarage(Garage i_Garage)
        {
            Console.Write("Please enter license number: ");
            try
            {
                Console.WriteLine(string.Format("{0}{1}", i_Garage.VehicleInfo(Console.ReadLine()), Environment.NewLine));
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(string.Format("{0}{1}", ex.Message, Environment.NewLine));
            }
        }

        private static void checkEnergyValidityAsFloat(ref string i_Input, out float i_Energy)
        {
            while(!float.TryParse(i_Input, out i_Energy))
            {
                Console.Write("Please enter a valid float number for the energy: ");
                i_Input = Console.ReadLine();
            }
        }

        private static void checkNumOfParameters(out string[] i_Parameters, int i_NumOfParameters)
        {
            while((i_Parameters = Console.ReadLine().Split(',')).Length != i_NumOfParameters)
            {
                Console.Write(string.Format("Please provide {0} parameters (separated by ','): ", i_NumOfParameters));
            }
        }

        private static void enumValidation(Type i_EnumClass,ref string io_Input, out object o_ParsedEnum, string i_ErrorMsg)
        {
            while(!ParseToEnumMember(i_EnumClass, io_Input, out o_ParsedEnum))
            {
                Console.Write(i_ErrorMsg);
                io_Input = Console.ReadLine();
            }
        }
    }
}
