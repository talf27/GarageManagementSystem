namespace GarageLogic
{
    internal abstract class Engine
    {
        internal float CurrentEnergy { get; set; } = 0;
        internal float MaxEnergy { get; set; }
        
        protected internal abstract string ShowEngineDetails();
        
        internal virtual void FillEnergyInEngine(float i_EnergyToAdd)
        {
            if(this.CurrentEnergy + i_EnergyToAdd <= this.MaxEnergy && i_EnergyToAdd >= 0)
            {
                this.CurrentEnergy += i_EnergyToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, this.MaxEnergy - this.CurrentEnergy);
            }
        }

    }

}
