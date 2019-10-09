﻿using GalaSoft.MvvmLight.Command;
using Lands.Helpers;
using Lands.Services;
using Lands.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lands.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Services

        private ApiService apiService;

        #endregion

        #region Attributes

        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;

        #endregion

        #region Properties

        public string Email
        {
            get { return email; }
            set { SetValue(ref email, value); }
        }
        public string Password
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }
        public bool IsRunning
        {
            get { return isRunning; }
            set { SetValue(ref isRunning, value); }
        }
        public bool IsRemembered { get; set; }
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetValue(ref isEnabled, value); }
        }

        #endregion

        #region Constructors

        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;

            this.apiService = new ApiService();

            this.Email = "fernando.senchuk@gmail.com";
            this.Password = "123456";

            //http://restcountries.eu/rest/v2/all
        }

        #endregion

        #region Commands

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password.",
                    "Accept");

                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");

                return;
            }

            var token = await this.apiService.GetToken("https://landsapifds.azurewebsites.net", this.Email, this.Password);

            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Something was wrong, please try later.",
                    "Accept");

                return;
            }

            if (string.IsNullOrEmpty(token.TokenType))
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    token.ErrorDescription,
                    "Accept");

                this.password = string.Empty;

                return;
            }

            //if (this.Email != "fernando.senchuk@gmail.com" || this.Password != "1234")
            //{
            //    this.IsRunning = false;
            //    this.IsEnabled = true;

            //    await Application.Current.MainPage.DisplayAlert(
            //        "Error",
            //        "Email or password incorrect.",
            //        "Accept");

            //    this.Password = string.Empty;

            //    return;
            //}


            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.Lands = new LandsViewModel();

            if (this.IsRemembered)
            {
                mainViewModel.Token = token.AccessToken;
                mainViewModel.TokenType = token.TokenType;

                Settings.Token = token.AccessToken;
                Settings.TokenType = token.TokenType;
            }

            Application.Current.MainPage = new MasterPage();
            //App.Current.MainPage = new MasterPage();
            //await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;
        }

        #endregion
    }
}
