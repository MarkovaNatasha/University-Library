using System;
using System.Windows.Data;
using LibraryUniversity.Model;

namespace LibraryUniversity.ViewModel
{
    public class ConverterNewReader : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var parameters = new NewReader();

                parameters.isStudent = (bool)values[0];
                parameters.isTeacher = (bool)values[1];
                parameters.NewSurname = values[2].ToString();
                parameters.NewName = values[3].ToString();
                parameters.NewPatronymic = values[4].ToString();
                if(values[5] != null) parameters.NewCourse = (Int32)values[5];
                parameters.NewFaculty = (Faculty)values[6];
                parameters.NewGroup = (Group)values[7];
                return parameters;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
