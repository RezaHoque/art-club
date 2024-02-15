using artclub.Pages;

namespace artclub
{
    public partial class App : Application
    {
       
        public App(HomePage mainPage)
        {
            InitializeComponent();

            
            MainPage = new NavigationPage(mainPage);
        }
    }
}
