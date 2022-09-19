using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleOwnerProfile> r_VehicleOwnersDict = new Dictionary<string, VehicleOwnerProfile>();
        private readonly Dictionary<string, Enums.eVehicleStatus> r_VehicleStatusesDict = new Dictionary<string, Enums.eVehicleStatus>();
        private readonly List<Vehicle> r_VehiclesInGarage = new List<Vehicle>();

        internal void AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            r_VehicleOwnersDict.Add(i_Vehicle.LicenseNumber, new VehicleOwnerProfile(i_OwnerName, i_OwnerPhone));
            r_VehicleStatusesDict.Add(i_Vehicle.LicenseNumber, Enums.eVehicleStatus.InRepair);
            r_VehiclesInGarage.Add(i_Vehicle);
        }

        public void ChangeStatus(string i_LicenseNumber, Enums.eVehicleStatus i_Status = Enums.eVehicleStatus.InRepair)
        {
            if(isVehicleInGarage(i_LicenseNumber))
            {
                r_VehicleStatusesDict[i_LicenseNumber] = i_Status;
            }
            else
            {
                throw new ArgumentException("The requested vehicle is not in the garage.");
            }
        }

        public LinkedList<string> GetLicenseNumbers(LinkedList<Enums.eVehicleStatus> i_FilterStatuses)
        {
            LinkedList<string> filteredVehicles;

            if(i_FilterStatuses.Count == 0)
            {
                filteredVehicles = new LinkedList<string>(r_VehicleStatusesDict.Keys);
            }
            else
            {
                filteredVehicles = new LinkedList<string>(r_VehicleStatusesDict.Keys.Where(vehicle => i_FilterStatuses.Contains(r_VehicleStatusesDict[vehicle])));
            }

            return filteredVehicles;
        }

        public void InflateTiresToFull(string i_LicenseNumber)
        {
            LinkedList<Tire> vehicleTires = findVehicleInGarage(i_LicenseNumber).Tires;
            
            foreach(Tire tire in vehicleTires)
            {
                tire.InflateAir(tire.MaxAirPressure - tire.CurrentAirPressure);
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, Enums.eFuelType i_FuelType, float i_FuelToAdd)
        {
            Vehicle vehicle = findVehicleInGarage(i_LicenseNumber);

            if(vehicle.VehicleEngine is FueledEngine)
            {
                (vehicle.VehicleEngine as FueledEngine).FillEnergyInEngine(i_FuelToAdd, i_FuelType);
                vehicle.LeftEnergy = (vehicle.VehicleEngine.CurrentEnergy / vehicle.VehicleEngine.MaxEnergy) * 100;
            }
            else
            {
                throw new ArgumentException("The vehicle is not with fueled engine.");
            }
        }

        public void RechargeVehicle(string i_LicenseNumber, float i_HoursToAdd)
        {
            Vehicle vehicle = findVehicleInGarage(i_LicenseNumber);

            if(vehicle.VehicleEngine is ElectricEngine)
            {
                vehicle.VehicleEngine.FillEnergyInEngine(i_HoursToAdd);
                vehicle.LeftEnergy = (vehicle.VehicleEngine.CurrentEnergy / vehicle.VehicleEngine.MaxEnergy) * 100;
            }
            else
            {
                throw new ArgumentException("The vehicle is not with electric engine.");
            }
        }

        public string VehicleInfo(string i_LicenseNumber)
        {
            string info;
            Vehicle vehicle = findVehicleInGarage(i_LicenseNumber); 
            
            info = string.Format(
@"{0}
The vehicle's owner name is: {1}, and his/her phone number is: {2}.
The vehicle's status in the garage is: {3}.",
                vehicle.ShowVehicleDetails(),
                r_VehicleOwnersDict[i_LicenseNumber].m_OwnerName,
                r_VehicleOwnersDict[i_LicenseNumber].m_OwnerPhone,
                r_VehicleStatusesDict[i_LicenseNumber]);

            return info;
        }

        private Vehicle findVehicleInGarage(string i_LicenseNumber)
        {
            Vehicle vehicleFound = r_VehiclesInGarage.Find(vehicle => vehicle.LicenseNumber == i_LicenseNumber);

            if(vehicleFound == null)
            {
                 throw new ArgumentException("The requested vehicle is not in the garage.");
            }

            return vehicleFound;
        }

        private bool isVehicleInGarage(string i_LicenseNumber)
        {
            return r_VehiclesInGarage.Exists(vehicle => vehicle.LicenseNumber == i_LicenseNumber);
        }
    }
}
