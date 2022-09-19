namespace GarageLogic
{
    internal class Tire
    {
        internal string ManufacturerName { get; set; }
        internal float CurrentAirPressure { get; set; }
        internal float MaxAirPressure { get; }

        internal Tire(float i_MaxAirPressure)
        {
            this.MaxAirPressure = i_MaxAirPressure;
        }

        internal void InflateAir(float i_AirToAdd)
        {
            if(this.CurrentAirPressure + i_AirToAdd <= this.MaxAirPressure && i_AirToAdd >= 0)
            {
                this.CurrentAirPressure += i_AirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, this.MaxAirPressure - this.CurrentAirPressure);
            }
        }
    }
}
