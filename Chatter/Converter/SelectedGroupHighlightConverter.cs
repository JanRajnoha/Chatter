using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Organization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Chatter.Converter
{
    public class SelectedGroupHighlightConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            return value[0] is GroupDetailModelDTO group && value[1] is GroupDetailModelDTO selected
                ? group.Id == selected.Id && group?.Organization?.Id == selected.Organization.Id ? FontWeights.Bold : FontWeights.Normal
                : FontWeights.Normal;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
