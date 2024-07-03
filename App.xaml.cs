using acabreraS6.Views;

namespace acabreraS6
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new EstudiantePrincipal();
        }
    }
}
