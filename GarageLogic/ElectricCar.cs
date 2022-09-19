namespace GarageLogic
{
    internal class ElectricCar : Car
    {
        internal ElectricCar() 
            :base()
        {
            base.VehicleEngine = new ElectricEngine();
            base.VehicleEngine.MaxEnergy = 3.3F;
        }
    }
}
