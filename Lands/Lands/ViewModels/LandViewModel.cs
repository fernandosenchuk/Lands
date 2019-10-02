using Lands.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Lands.ViewModels
{
    public class LandViewModel : BaseViewModel
    {
        #region Attributes

        private ObservableCollection<Border> borders;
        private ObservableCollection<Currency> currencies;
        private ObservableCollection<Language> languages;

        #endregion

        #region Properties

        public ObservableCollection<Border> Borders
        {
            get { return borders; }
            set { SetValue(ref borders, value); }
        }

        public ObservableCollection<Currency> Currencies
        {
            get { return currencies; }
            set { SetValue(ref currencies, value); }
        }

        public ObservableCollection<Language> Languages
        {
            get { return languages; }
            set { SetValue(ref languages, value); }
        }

        public Land Land { get; set; }

        #endregion

        #region Constructor

        public LandViewModel(Land land)
        {
            this.Land = land;

            this.LoadBorders();

            this.Currencies = new ObservableCollection<Currency>(this.Land.Currencies);

            this.Languages = new ObservableCollection<Language>(this.Land.Languages);
        }

        #endregion

        #region Methods

        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Border>();

            foreach (var border in Land.Borders)
            {
                var land = MainViewModel.GetInstance().LandsList.Where(x => x.Alpha3Code == border).FirstOrDefault();

                if (land != null)
                {
                    this.Borders.Add(new Border()
                    {
                        Code = land.Alpha3Code,
                        Name = land.Name
                    });
                }
            }
        }

        #endregion
    }
}
