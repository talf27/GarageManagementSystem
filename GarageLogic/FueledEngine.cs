using System;

namespace GarageLogic
{
    internal class FueledEngine : Engine
    {
        internal Enums.eFuelType FuelType { get; set; }

        internal void FillEnergyInEngine(float i_FuelToAdd, Enums.eFuelType i_FuelType)
        {
            if(i_FuelType != this.FuelType)
            {
                throw new ArgumentException("The given fuel type is not the right one.");
            }

            base.FillEnergyInEngine(i_FuelToAdd);
        }

        protected internal override string ShowEngineDetails()
        {
            return string.Format(
@"This vehicle is with fueled engine.
It has {0} fuel type, {1} liters fuel remaining on its tank, out of {2}.{3}",
                this.FuelType.ToString(),
                base.CurrentEnergy,
                base.MaxEnergy,
                Environment.NewLine);
        }
    }
}
