using GalaSoft.MvvmLight.Command;
using Lands.Helpers;
using Lands.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lands.ViewModels
{
    public class MenuItemViewModel
    {
        #region Properties

        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }

        #endregion

        #region Commands

        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        #endregion

        #region Methods

        private void Navigate()
        {
            App.Master.IsPresented = false;

            if (this.PageName == "LoginPage")
            {
                Settings.IsRemembered = "false";

                var mainViewModel = MainViewModel.GetInstance();

                mainViewModel.Token = null;
                mainViewModel.UserLocal = null;

                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if(this.PageName == "MyProfilePage")
            {
                var mainViewModel = MainViewModel.GetInstance();

                mainViewModel.MyProfile = new MyProfileViewModel();

                App.Navigator.PushAsync(new MyProfilePage());
            }
            
        }

        #endregion
    }
}
