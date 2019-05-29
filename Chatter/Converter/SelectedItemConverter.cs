using Chatter.BL.DTO.Group;
using Chatter.BL.DTO.Organization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Chatter.Converter
{
    public class SelectedItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is GroupDetailModelDTO group && values[1] is OrganizationDetailModelDTO organization)
                return group.Organization?.Id != organization.Id ? null : values[0];

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { value, null };
        }
    }
}
