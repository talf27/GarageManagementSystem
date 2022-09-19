namespace GarageLogic
{
    internal struct VehicleOwnerProfile
    {
        internal string m_OwnerName;
        internal string m_OwnerPhone;

        internal VehicleOwnerProfile(string i_OwnerName, string i_OwnerPhone)
        {
            this.m_OwnerName = i_OwnerName;
            this.m_OwnerPhone = i_OwnerPhone;
        }
    }
}
