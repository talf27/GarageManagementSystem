using System;

namespace GarageLogic
{
    internal class ElectricEngine : Engine
    {
        protected internal override string ShowEngineDetails()
        {
            return string.Format(
@"This vehicle is with electric engine.
It has {0} hours remaining on its battery, out of {1}.{2}",
                base.CurrentEnergy,
                base.MaxEnergy,
                Environment.NewLine);
        }
    }
}
