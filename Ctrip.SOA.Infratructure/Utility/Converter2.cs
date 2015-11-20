using System;
using Ctrip.SOA.Infratructure.TypeConversion;

namespace Ctrip.SOA.Infratructure.Utility
{   
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Converter2
    {
        public static object ToType(object source, Type targetType)
        {
            ITypeConverter typeConverter = TypeConverterRegistry.GetConverter(targetType);
            if (typeConverter == null)
            {
                return Convert.ChangeType(source, targetType);
            }

            return typeConverter.ConvertFrom(source);
        }
    }
}
