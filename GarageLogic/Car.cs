using System;
using System.Text;

namespace GarageLogic
{
    internal abstract class Car : Vehicle
    {
        internal Enums.eCarColors Color { get; set; }
        internal Enums.eNumOfDoors NumOfDoors { get; set; }

        internal Car()
        {
            base.InitTires(4, 29);
        }

        protected internal override string GetVehiclePropertiesNames()
        {
            StringBuilder vehiclePropertiesNames = new StringBuilder();

            vehiclePropertiesNames.Append(base.GetVehiclePropertiesNames());
            vehiclePropertiesNames.Append(string.Format(" ,color ({0}) and the number of doors ({1})", Enums.PrintEnumMembers(typeof(Enums.eCarColors)), Enums.PrintEnumMembers(typeof(Enums.eNumOfDoors))));
            
            return vehiclePropertiesNames.ToString();
        }

        protected internal override int GetNumberOfProperties()
        {
            return base.GetNumberOfProperties() + 2;
        }

        protected internal override void SetVehicleProperties(string[] vehicleProperties)
        {
            object enumAsObject;
            int numOfProperties = this.GetNumberOfProperties();

            if(vehicleProperties.Length == numOfProperties)
            {
                base.SetVehicleProperties(vehicleProperties);
                if(!Enums.ParseToEnumMember(typeof(Enums.eCarColors), vehicleProperties[numOfProperties - 2], out enumAsObject))
                {
                    throw new FormatException(string.Format("The color options are: {0} (by number or by string)", Enums.PrintEnumMembers(typeof(Enums.eCarColors))));
                }

                this.Color = (Enums.eCarColors)enumAsObject;
                if(!Enums.ParseToEnumMember(typeof(Enums.eNumOfDoors), vehicleProperties[numOfProperties - 1], out enumAsObject))
                {
                    throw new FormatException(string.Format("The number of doors can be: {0} (by number or by string)", Enums.PrintEnumMembers(typeof(Enums.eNumOfDoors))));
                }

                this.NumOfDoors = (Enums.eNumOfDoors)enumAsObject;
            }
            else
            {
                throw new ArgumentException(string.Format("Car needs to set {0} properties.", numOfProperties));
            }
        }

        protected internal override string ShowVehicleDetails()
        {
            StringBuilder vehicleDetails = new StringBuilder();
            
            vehicleDetails.Append($"This is a Car.{Environment.NewLine}");
            vehicleDetails.Append(base.ShowVehicleDetails());
            vehicleDetails.Append(base.VehicleEngine.ShowEngineDetails());
            vehicleDetails.Append(string.Format("The car is {0} and has {1} doors.", this.Color, this.NumOfDoors));
            
            return vehicleDetails.ToString();
        }
    }
}
