using Chatter.BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Chatter.Converter
{
    public class MessageBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture) => value[0] is UserListModelDTO user && value[1] is UserDetailModelDTO poster
                ? (string)parameter == "Icon"
                ? user.Id == poster.Id ? Brushes.Red : (Brush)new BrushConverter().ConvertFrom("#02949d")
                : user.Id == poster.Id ? Brushes.White : (Brush)new BrushConverter().ConvertFrom("#A1D6E2")
                : (Brush)new BrushConverter().ConvertFrom("#A1D6E2");

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
