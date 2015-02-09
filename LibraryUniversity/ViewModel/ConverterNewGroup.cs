using System;
using System.Windows.Data;
using LibraryUniversity.Model;

namespace LibraryUniversity.ViewModel
{
    public class ConverterNewGroup : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new NewGroup {Name = values[0].ToString(), Faculty = (Faculty) values[1]};
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
