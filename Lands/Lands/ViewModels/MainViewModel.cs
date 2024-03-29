﻿using Lands.Models;
using Lands.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lands.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Attributes

        private UserLocal userLocal;

        #endregion

        #region Properties

        public List<Land> LandsList { get; set; }
        public TokenResponse Token { get; set; }

        public UserLocal UserLocal
        {
            get { return userLocal; }
            set { SetValue(ref userLocal, value); }
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        #endregion

        #region ViewModels

        public LoginViewModel Login { get; set; }
        public LandsViewModel Lands { get; set; }
        public LandViewModel Land { get; set; }
        public MyProfileViewModel MyProfile { get; set; }
        public RegisterViewModel Register { get; set; }
        public ChangePasswordViewModel ChangePassword { get; set; }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            instance = this;

            this.Login = new LoginViewModel();

            this.LoadMenu();
        }

        #endregion

        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
                return new MainViewModel();

            return instance;
        }

        #endregion

        #region Methods

        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();

            this.Menus.Add(new MenuItemViewModel()
            {
                Icon = "ic_settings.png",
                PageName = "MyProfilePage",
                Title = Resource.MyProfile
            });

            this.Menus.Add(new MenuItemViewModel()
            {
                Icon = "ic_insert_chart.png",
                PageName = "StatisticsPage",
                Title = Resource.Statistics
            });

            this.Menus.Add(new MenuItemViewModel()
            {
                Icon = "ic_exit_to_app.png",
                PageName = "LoginPage",
                Title = Resource.LogOut
            });
        }

        #endregion
    }
}
