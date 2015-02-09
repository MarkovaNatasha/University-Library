using System;
using System.Globalization;
using System.Windows.Data;
using LibraryUniversity.Model;

namespace LibraryUniversity.ViewModel
{
    class ConverterNewBook : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
                              CultureInfo culture)
        {
            var parameters = new NewBook();
            parameters.Name = values[0].ToString();
            parameters.Writer = (Writer) values[1];
            parameters.Publication = (Publication) values[2];
            if (values[3] != null) parameters.Year = (DateTime)values[3];
            parameters.ISBN = values[4].ToString();
            int pages;
            if (Int32.TryParse(values[5].ToString(), NumberStyles.Number, null, out pages)) parameters.Pages = pages;
            int count;
            if (Int32.TryParse(values[6].ToString(), NumberStyles.Number, null, out count)) parameters.Count = count;
            parameters.Shelving = (Shelving) values[7];
            return parameters;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
                                    CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
