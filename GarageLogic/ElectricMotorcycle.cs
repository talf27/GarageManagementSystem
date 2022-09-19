namespace GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        internal ElectricMotorcycle()
            : base()
        {
            base.VehicleEngine = new ElectricEngine();
            base.VehicleEngine.MaxEnergy = 2.5F;
        }
    }
}
