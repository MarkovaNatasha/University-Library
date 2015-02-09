using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LibraryUniversity.Model;
using LibraryUniversity.View;
using System.ComponentModel;

namespace LibraryUniversity.ViewModel
{
    internal class AuthorizationViewModel : INotifyPropertyChanged
    {
        private string _login;
        public ICommand ClickCommandCancel { get; set; }
        public ICommand ClickCommandOk { get; set; }

        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        public AuthorizationViewModel()
        {
            ClickCommandOk = new Command(ClickOK);
            ClickCommandCancel = new Command(args => ClickCancel());
        }

        private void ClickOK(object param)
        {
            var pass = param as PasswordBox;
            var db = new DBUniversityLibraryEntities();
            if (db.Employee.Count(e => e.surname == Login && e.password == pass.Password && e.permission == "admin") > 0)
            {
                var mw = new MainWindow {DataContext = new MainWindowViewModel()};
                mw.Show();
                mw.labelEmp.Content += Login;
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = mw;
            }
            else if (db.Employee.Count(e => e.surname == Login && e.password == pass.Password) > 0)
            {
                var mw = new MainWindow { DataContext = new MainWindowViewModel() };
                mw.Show();
                mw.manual.Visibility = Visibility.Collapsed;
                mw.labelEmp.Content += Login;
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = mw;
            }
            else
            {
                MessageBox.Show("Невірні дані! Спробуйте увійти ще раз!", "Помилка");
            }
        }

        private  void ClickCancel()
        {
            Application.Current.MainWindow.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
