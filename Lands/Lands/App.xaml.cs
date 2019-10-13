using Lands.Helpers;
using Lands.Models;
using Lands.Services;
using Lands.ViewModels;
using Lands.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Lands
{
    public partial class App : Application
    {
        #region Properties

        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        #endregion

        #region Constructor

        public App()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Settings.Token))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var dataService = new DataService();

                var userLocal = dataService.First<UserLocal>(false);

                var mainViewModel = MainViewModel.GetInstance();

                mainViewModel.Token = Settings.Token;
                mainViewModel.TokenType = Settings.TokenType;

                mainViewModel.Lands = new LandsViewModel();

                mainViewModel.UserLocal = userLocal;

                Application.Current.MainPage = new MasterPage();
            }
        }

        #endregion

        #region Methods

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion
    }
}
