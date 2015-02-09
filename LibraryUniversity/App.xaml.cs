using LibraryUniversity.ViewModel;
using LibraryUniversity.View;


namespace LibraryUniversity
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var mw = new Authorization
            {
                DataContext = new AuthorizationViewModel()
            };

            mw.Show();
        }
    }
}
