using artclub.Pages;

namespace artclub
{
    public partial class App : Application
    {
       
        public App()
        {
            InitializeComponent();

            
            MainPage = new AppShell();
        }
    }
}
