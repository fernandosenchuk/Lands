﻿using GalaSoft.MvvmLight.Command;
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
            if (this.PageName == "LoginPage")
            {
                Settings.Token = string.Empty;
                Settings.TokenType = string.Empty;

                var mainViewModel = MainViewModel.GetInstance();

                mainViewModel.Token = string.Empty;
                mainViewModel.TokenType = string.Empty;

                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }

        #endregion
    }
}