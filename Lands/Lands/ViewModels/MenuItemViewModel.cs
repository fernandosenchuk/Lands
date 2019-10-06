﻿using GalaSoft.MvvmLight.Command;
using Lands.Views;
using System.Windows.Input;

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
            if(this.PageName == "LoginPage")
            {
                App.Current.MainPage = new LoginPage();
            }
        }

        #endregion
    }
}
