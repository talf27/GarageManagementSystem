using System;

namespace GarageLogic
{
    public class Factory
    {
        public enum eVehicleTypes
        {
            FueledMotorcycle = 1,
            ElectricMotorcycle,
            FueledCar,
            ElectricCar,
            FueledTruck
        }

        public static Vehicle CreateVehicle(eVehicleTypes i_VehicleType)
        {
            Vehicle newVehicle = null;

            if(i_VehicleType == eVehicleTypes.FueledMotorcycle)
            {
                newVehicle = new FueledMotorcycle();
            }
            else if(i_VehicleType == eVehicleTypes.ElectricMotorcycle)
            {
                newVehicle = new ElectricMotorcycle();
            }
            else if(i_VehicleType == eVehicleTypes.FueledCar)
            {
                newVehicle = new FueledCar();
            }
            else if(i_VehicleType == eVehicleTypes.ElectricCar)
            {
                newVehicle = new ElectricCar();
            }
            else if(i_VehicleType == eVehicleTypes.FueledTruck)
            {
                newVehicle = new FueledTruck();
            }
            else
            {
                throw new FormatException("There is no such a vehicle type.");
            }

            return newVehicle;
        }
    }
}
