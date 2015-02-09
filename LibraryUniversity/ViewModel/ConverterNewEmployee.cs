using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace LibraryUniversity.ViewModel
{
    class ConverterNewEmployee : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return new NewEmployee
                       {Surname = values[0].ToString(), Name = values[1].ToString(), Patronymic = values[2].ToString(),
                        Password = values[3].ToString(), Permission = values[4].ToString()};
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
