using GalaSoft.MvvmLight.Command;
using Lands.Models;
using Lands.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace Lands.ViewModels
{
    public class LandsViewModel : BaseViewModel
    {
        #region Services

        private ApiService apiService;

        #endregion

        #region Attributes

        private ObservableCollection<Land> lands;
        private bool isRefreshing;
        private string filter;

        private List<Land> landsList;

        #endregion

        #region Properties

        public ObservableCollection<Land> Lands
        {
            get { return lands; }
            set { SetValue(ref lands, value); }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetValue(ref isRefreshing, value); }
        }

        public string Filter
        {
            get { return filter; }
            set
            {
                SetValue(ref filter, value);

                this.Search();
            }
        }

        #endregion

        #region Commands

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        #endregion

        #region Constructors

        public LandsViewModel()
        {
            this.apiService = new ApiService();

            this.LoadLands();
        }

        #endregion

        #region Methods

        private async void LoadLands()
        {
            this.IsRefreshing = true;

            //var connection = await this.apiService.CheckConnection();

            //if (!connection.IsSuccess)
            //{
            //    this.IsRefreshing = false;

            //    await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");

            //    await Application.Current.MainPage.Navigation.PopAsync();

            //    return;
            //}

            var response = await this.apiService.GetList<Land>("http://restcountries.eu", "/rest", "/v2/all");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");

                await Application.Current.MainPage.Navigation.PopAsync();

                return;
            }

            this.landsList = (List<Land>)response.Result;

            this.Lands = new ObservableCollection<Land>(this.landsList);

            this.IsRefreshing = false;
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<Land>(this.landsList);
            }
            else
            {
                this.Lands = new ObservableCollection<Land>(
                    this.landsList.Where(x =>
                        !string.IsNullOrEmpty(x.Name)
                        && !string.IsNullOrEmpty(x.Capital)
                        && (x.Name.ToLower().Contains(this.Filter.ToLower()) 
                            || x.Capital.ToLower().Contains(this.Filter.ToLower()))
                        ));
            }
        }

        #endregion
    }
}
