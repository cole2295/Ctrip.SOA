using System;
using System.Globalization;
using Ctrip.SOA.Infratructure.Utility;

namespace Ctrip.SOA.Infratructure.TypeConversion
{   
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DateTimeConverter : BaseTypeConverter
    {
        public override bool CanConvertFrom(Type sourceType)
        {
            return sourceType == typeof(DateTime) || base.CanConvertFrom(sourceType);
        }

        public override object ConvertFrom(object source, CultureInfo culture)
        {
            if (source == null)
            {
                return DateTimeHelper.MinValue;
            }
            return Convert.ToDateTime(source);
        }
    }
}