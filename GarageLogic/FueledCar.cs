namespace GarageLogic
{
    internal class FueledCar : Car
    {
        internal FueledCar()
            : base()
        {
            base.VehicleEngine = new FueledEngine();
            base.VehicleEngine.MaxEnergy = 38;
            (base.VehicleEngine as FueledEngine).FuelType = Enums.eFuelType.Octane95;
        }
    }
}
