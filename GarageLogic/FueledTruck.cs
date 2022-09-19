namespace GarageLogic
{
    internal class FueledTruck : Truck
    {
        internal FueledTruck()
            : base()
        {
            base.VehicleEngine = new FueledEngine();
            base.VehicleEngine.MaxEnergy = 120;
            (base.VehicleEngine as FueledEngine).FuelType = Enums.eFuelType.Soler;
        }
    }
}
