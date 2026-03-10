using SystemLosowania.Views;

namespace SystemLosowania
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainMenuPage())
            {
                BarBackgroundColor = Color.FromArgb("#667eea"),
                BarTextColor = Colors.White
            };
        }
    }
}
