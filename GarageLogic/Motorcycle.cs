using System;
using System.Text;

namespace GarageLogic
{
    internal abstract class Motorcycle : Vehicle
    {
        internal Enums.eLicenseType LicenseType { get; set; }
        internal int EngineVolume { get; set; }

        internal Motorcycle()
        {
            base.InitTires(2, 31);
        }

        protected internal override string GetVehiclePropertiesNames()
        {
            StringBuilder vehiclePropertiesNames = new StringBuilder();

            vehiclePropertiesNames.Append(base.GetVehiclePropertiesNames());
            vehiclePropertiesNames.Append(string.Format(" ,license type ({0}) and engine's volume", Enums.PrintEnumMembers(typeof(Enums.eLicenseType))));
           
            return vehiclePropertiesNames.ToString();
        }

        protected internal override int GetNumberOfProperties()
        {
            return base.GetNumberOfProperties() + 2;
        }

        protected internal override void SetVehicleProperties(string[] vehicleProperties)
        {
            int engineVolume;
            object enumAsObject;
            int numOfProperties = this.GetNumberOfProperties();

            if(vehicleProperties.Length == numOfProperties)
            {
                base.SetVehicleProperties(vehicleProperties);
                if(!Enums.ParseToEnumMember(typeof(Enums.eLicenseType), vehicleProperties[numOfProperties - 2], out enumAsObject))
                {
                    throw new FormatException(string.Format("The license's type options are: {0} (by number or by string)", Enums.PrintEnumMembers(typeof(Enums.eLicenseType))));
                }

                this.LicenseType = (Enums.eLicenseType)enumAsObject;
                if(!int.TryParse(vehicleProperties[numOfProperties - 1], out engineVolume))
                {
                    throw new FormatException("The motorcycle's engine volume needs to be an integer value.");
                }

                this.EngineVolume = engineVolume;
            }
            else
            {
                throw new ArgumentException(string.Format("Motorcycle needs to set {0} properties.", numOfProperties));
            }
        }

        protected internal override string ShowVehicleDetails()
        {
            StringBuilder vehicleDetails = new StringBuilder();
            
            vehicleDetails.Append($"This is a Motorcycle.{Environment.NewLine}");
            vehicleDetails.Append(base.ShowVehicleDetails());
            vehicleDetails.Append(base.VehicleEngine.ShowEngineDetails());
            vehicleDetails.Append(string.Format("The motorcycle's license type is: {0}, and its engine's volume is: {1}.", this.LicenseType, this.EngineVolume));
            
            return vehicleDetails.ToString();
        }
    }
}
