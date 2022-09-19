using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageLogic
{
    public class VehicleProducer
    {
        private static readonly List<Vehicle> s_ExistingVehicles = new List<Vehicle>();

        public static string GetPropertiesOfVehicle(Vehicle i_Vehicle)
        {
            return i_Vehicle.GetVehiclePropertiesNames();
        }

        public static void SetPropertiesOfVehicle(Vehicle i_Vehicle, string[] i_Properties)
        {
            i_Vehicle.SetVehicleProperties(i_Properties);
            AddExistingVehicle(i_Vehicle);
        }

        public static void InitTiresAirPressure(Vehicle i_Vehicle, LinkedList<int> i_AirPressures)
        {
            int tireIndex = 0;

            foreach(Tire tire in i_Vehicle.Tires)
            {
                tire.InflateAir(i_AirPressures.ElementAt(tireIndex));
                tireIndex++;
            }
        }

        public static void InitEnergy(Vehicle i_Vehicle, float i_EnergyToAdd)
        {
            i_Vehicle.VehicleEngine.FillEnergyInEngine(i_EnergyToAdd);
            i_Vehicle.LeftEnergy = (i_Vehicle.VehicleEngine.CurrentEnergy / i_Vehicle.VehicleEngine.MaxEnergy) * 100;
        }

        public static string VehicleInfo(Vehicle i_Vehicle)
        {
            return i_Vehicle.ShowVehicleDetails();
        }

        public static void AddExistingVehicle(Vehicle i_Vehicle)
        {
            s_ExistingVehicles.Add(i_Vehicle);
        }

        public static void AddToGarage(Garage i_Garage, string i_LicenseNumber, string i_OwnerName, string i_OwnerPhone)
        {
            if(IsVehicleExist(i_LicenseNumber))
            {
                try
                {
                    i_Garage.AddVehicle(findExistingVehicle(i_LicenseNumber), i_OwnerName, i_OwnerPhone);
                }
                catch(ArgumentException ex)
                {
                    i_Garage.ChangeStatus(i_LicenseNumber);
                }
            }
            else
            {
                throw new ArgumentException("There is no existing vehicle with this license number.");
            }
        }

        private static Vehicle findExistingVehicle(string i_LicenseNumber)
        {
            Vehicle vehicleFound = s_ExistingVehicles.Find(vehicle => vehicle.LicenseNumber == i_LicenseNumber);

            if(vehicleFound == null)
            {
                throw new ArgumentException();
            }

            return vehicleFound;
        }

        public static bool IsVehicleExist(string i_LicenseNumber)
        {
            return s_ExistingVehicles.Exists(vehicle => vehicle.LicenseNumber == i_LicenseNumber);
        }
    }
}
