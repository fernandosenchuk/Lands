using GalaSoft.MvvmLight.Command;
using Lands.Models;
using Lands.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lands.ViewModels
{
    public class LandsViewModel : BaseViewModel
    {
        #region Services

        private ApiService apiService;

        #endregion

        #region Attributes

        private ObservableCollection<LandItemViewModel> lands;
        private bool isRefreshing;
        private string filter;

        #endregion

        #region Properties

        public ObservableCollection<LandItemViewModel> Lands
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

            MainViewModel.GetInstance().LandsList = (List<Land>)response.Result;

            this.Lands = new ObservableCollection<LandItemViewModel>(this.ToLandItemViewModel());

            this.IsRefreshing = false;
        }

        private IEnumerable<LandItemViewModel> ToLandItemViewModel()
        {
            return MainViewModel.GetInstance().LandsList.Select(x => new LandItemViewModel()
            {
                Alpha2Code = x.Alpha2Code,
                Alpha3Code = x.Alpha3Code,
                AltSpellings = x.AltSpellings,
                Area = x.Area,
                Borders = x.Borders,
                CallingCodes = x.CallingCodes,
                Capital = x.Capital,
                Cioc = x.Cioc,
                Currencies = x.Currencies,
                Demonym = x.Demonym,
                Flag = x.Flag,
                Gini = x.Gini,
                Languages = x.Languages,
                Latlng = x.Latlng,
                Name = x.Name,
                NativeName = x.NativeName,
                NumericCode = x.NumericCode,
                Population = x.Population,
                Region = x.Region,
                RegionalBlocs = x.RegionalBlocs,
                Subregion = x.Subregion,
                Timezones = x.Timezones,
                TopLevelDomain = x.TopLevelDomain,
                Translations = x.Translations
            });
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(this.ToLandItemViewModel());
            }
            else
            {
                this.Lands = new ObservableCollection<LandItemViewModel>(
                    this.ToLandItemViewModel().Where(x => !string.IsNullOrEmpty(x.Name)
                                                        && !string.IsNullOrEmpty(x.Capital)
                                                        && (x.Name.ToLower().Contains(this.Filter.ToLower())
                                                            || x.Capital.ToLower().Contains(this.Filter.ToLower()))
                                                        ));
            }
        }

        #endregion
    }
}
