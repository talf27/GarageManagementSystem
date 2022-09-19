namespace GarageLogic
{
    internal class FueledMotorcycle : Motorcycle
    {
        internal FueledMotorcycle()
            : base()
        {
            base.VehicleEngine = new FueledEngine();
            base.VehicleEngine.MaxEnergy = 6.2F;
            (base.VehicleEngine as FueledEngine).FuelType = Enums.eFuelType.Octane98;
        }
    }
}
