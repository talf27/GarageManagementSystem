using System;
using System.Text;

namespace GarageLogic
{
    internal abstract class Truck : Vehicle
    {
        internal bool IsContainingCooledCargo { get; set; }
        internal float CargoTankVolume { get; set; }

        internal Truck()
        {
            base.InitTires(16, 24);
        }

        protected internal override string GetVehiclePropertiesNames()
        {
            StringBuilder vehiclePropertiesNames = new StringBuilder();

            vehiclePropertiesNames.Append(base.GetVehiclePropertiesNames());
            vehiclePropertiesNames.Append("if it is containing cooled cargo (true/false) and the cargo tank volume");
            
            return vehiclePropertiesNames.ToString();
        }

        protected internal override int GetNumberOfProperties()
        {
            return base.GetNumberOfProperties() + 2;
        }

        protected internal override void SetVehicleProperties(string[] vehicleProperties)
        {
            bool isContainingCooledCargo;
            float cargoTankVolume;
            int numOfProperties = this.GetNumberOfProperties();

            if(vehicleProperties.Length == numOfProperties)
            {
                base.SetVehicleProperties(vehicleProperties);
                if(!bool.TryParse(vehicleProperties[numOfProperties - 2], out isContainingCooledCargo))
                {
                    throw new FormatException("The containing of cooled cargo needs to be 'true' or 'false'.");
                }

                this.IsContainingCooledCargo = isContainingCooledCargo;
                if(!float.TryParse(vehicleProperties[numOfProperties - 1], out cargoTankVolume))
                {
                    throw new FormatException("The truck's cargo tank volume needs to be a float value.");
                }

                this.CargoTankVolume = cargoTankVolume;
            }
            else
            {
                throw new ArgumentException(string.Format("Truck needs to set {0} properties.", numOfProperties));
            }
        }

        protected internal override string ShowVehicleDetails()
        {
            StringBuilder vehicleDetails = new StringBuilder();
            string isContainingString = this.IsContainingCooledCargo ? "is" : "is not";

            vehicleDetails.Append($"This is a Truck.{Environment.NewLine}");
            vehicleDetails.Append(base.ShowVehicleDetails());
            vehicleDetails.Append(base.VehicleEngine.ShowEngineDetails());
            vehicleDetails.Append(string.Format("The truck {0} containing cool cargo, and its cargo tank is: {1}", isContainingString, this.CargoTankVolume));
            
            return vehicleDetails.ToString();
        }
    }
}
