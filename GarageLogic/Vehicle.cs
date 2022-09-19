using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        internal string ModelName { get; set; }
        internal string LicenseNumber { get; set; }
        internal float LeftEnergy { get; set; }
        internal LinkedList<Tire> Tires { get; set; }
        internal Engine VehicleEngine { get; set; }

        protected internal void InitTires(int i_NumOfTires, float i_MaxAirPressure)
        {
            this.Tires = new LinkedList<Tire>();
            for(int i = 0; i < i_NumOfTires; i++)
            {
                Tires.AddLast(new Tire(i_MaxAirPressure));
            }
        }
        
        protected internal virtual string GetVehiclePropertiesNames()
        {
            return string.Format(string.Format("License number, model name, tire's manufacturer for {0} tires", this.GetNumOfTires()));
        }

        protected internal virtual int GetNumberOfProperties()
        {
            return this.GetNumOfTires() + 2;
        }

        public int GetNumOfTires()
        {
            return this.Tires.Count;
        }

        protected internal virtual void SetVehicleProperties(string[] vehicleProperties)
        {
            int tireIndex = 2;

            this.LicenseNumber = vehicleProperties[0];
            this.ModelName = vehicleProperties[1];
            foreach(Tire tire in this.Tires)
            {
                tire.ManufacturerName = vehicleProperties[tireIndex];
                tireIndex++;
            }
        }

        protected internal virtual string ShowVehicleDetails()
        {
            StringBuilder vehicleDetails = new StringBuilder(
                string.Format(
@"License number: {0}.
Model name: {1}.
It has {2} tires with maximal air pressure {3}.
It's remaining Energy Percentage is {4}%.{5}",
                    this.LicenseNumber,
                    this.ModelName,
                    GetNumOfTires(),
                    this.Tires.First.Value.MaxAirPressure,
                    this.LeftEnergy,
                    Environment.NewLine));
            vehicleDetails.Append(ShowTiresDetails());
            
            return vehicleDetails.ToString();
        }

        protected internal string ShowTiresDetails()
        {
            StringBuilder tiresDetails = new StringBuilder();
            int tireIndex = 1;

            foreach(Tire tire in this.Tires)
            {
                tiresDetails.Append(
                    string.Format(
                        "Tire Number {0} is manufactured by {1} and its current air pressure is: {2}.{3}",
                        tireIndex,
                        tire.ManufacturerName,
                        tire.CurrentAirPressure,
                        Environment.NewLine));
                tireIndex++;
            }

            return tiresDetails.ToString();
        }
        
        public override int GetHashCode()
        {
            return this.LicenseNumber.GetHashCode();
        }

        public override bool Equals(Object i_OtherVehicle)
        {
            bool equals = false;

            Vehicle vehicleToCompare = i_OtherVehicle as Vehicle;
            if(vehicleToCompare != null)
            {
                equals = this.LicenseNumber == vehicleToCompare.LicenseNumber;
            }
            
            return equals;
        }

        public static bool operator ==(Vehicle i_Vehicle1, Vehicle i_Vehicle2)
        {
            return Equals(i_Vehicle1, i_Vehicle2);
        }

        public static bool operator !=(Vehicle i_Vehicle1, Vehicle i_Vehicle2)
        {
            return !Equals(i_Vehicle1, i_Vehicle2);
        }
    }
}
