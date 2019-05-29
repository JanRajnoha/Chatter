using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chatter.Converter
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string paraString && value is Common.Enum.Gender valueGender)
            {
                return paraString == valueGender.ToString();
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gender = Common.Enum.Gender.Male;

            if (parameter is string paraString && value is bool valueBool)
            {
                if (paraString == "Female")
                    gender = Common.Enum.Gender.Female;

                if (valueBool)
                    return gender;
                else
                    if (gender == Common.Enum.Gender.Male)
                        return Common.Enum.Gender.Female;
                    else
                        return Common.Enum.Gender.Male;
            }

            return Common.Enum.Gender.Male;
        }
    }
}
