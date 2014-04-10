using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KinectToMidi.Helpers
{
    /// <summary>
    /// This ValueConverter converts an Enum value to its corresponding string value.
    /// </summary>
    public sealed class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string EnumString;
            try
            {
                if (value is Enum)
                {
                    EnumString = ((Enum)value).GetDescription();// Enum.GetName((value.GetType()), value);
                    return EnumString;
                }
                else
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// No need to implement converting back on a one-way binding 
        /// </summary>
        public object ConvertBack(object value, Type targetType,
          object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

