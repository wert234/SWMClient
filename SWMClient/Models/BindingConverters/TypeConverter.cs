using SWMClient.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SWMClient.Models.BindingConverters
{
    public class TypeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IEnumerable<Dwarvecs>)parameter).First(x => x.id == int.Parse(value.ToString())).name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IEnumerable<Dwarvecs>)parameter).First(x => x.name == value.ToString()).id;
        }
        
    }
}
