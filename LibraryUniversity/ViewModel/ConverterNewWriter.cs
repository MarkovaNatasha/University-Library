using System;
using System.Windows.Data;

namespace LibraryUniversity.ViewModel
{
    public class ConverterNewWriter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return new NewWriter
                       {Surname = values[0].ToString(), Name = values[1].ToString(), Patronymic = values[2].ToString()};
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
