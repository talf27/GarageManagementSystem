using System;
using System.Text;

namespace GarageLogic
{
    public class Enums
    {
        internal enum eEngineType
        {
            Fueled = 1,
            Electric
        }

        public enum eFuelType
        {
            Soler = 1,
            Octane95,
            Octane96,
            Octane98
        }

        internal enum eCarColors
        {
            Red = 1,
            White,
            Green,
            Blue
        }

        internal enum eNumOfDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }

        internal enum eLicenseType
        {
            A = 1,
            A1,
            B1,
            BB
        }

        public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired,
            Paid
        }

        public static string PrintEnumMembers(Type i_EnumClass)
        {
            StringBuilder enumMembers = new StringBuilder();

            foreach(object enumMember in Enum.GetValues(i_EnumClass))
            {
                enumMembers.Append(string.Format("{0}: {1}, ", (int)enumMember, enumMember));
            }

            enumMembers.Remove(enumMembers.Length - 2, 2);
            
            return enumMembers.ToString();
        }

        public static bool ParseToEnumMember(Type i_EnumClass, string i_StringToParse, out object o_ParsedEnum)
        {
            bool isValid;
            o_ParsedEnum = null;

            try
            {
                o_ParsedEnum = Enum.Parse(i_EnumClass, i_StringToParse);
                isValid = Enum.IsDefined(i_EnumClass, o_ParsedEnum);
            }
            catch(Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
