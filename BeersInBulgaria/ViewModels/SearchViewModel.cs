using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BeersInBulgaria.ViewModels
{
    public class SearchViewModel : BeersInBulgaria.Common.BindableBase
    {
        private string queryText = "";

        public string QueryText
        {
            get
            {
                return this.queryText;
            }
            set
            {
                this.queryText = value;
                this.OnPropertyChanged("QueryText");
                this.GetData();
            }
        }

        private ObservableCollection<BeerViewModel> results;
        public IEnumerable<BeerViewModel> Results
        {
            get
            {
                if (this.results == null)
                {
                    results = new ObservableCollection<BeerViewModel>();
                }

                return results;
            }
            set
            {
                this.results.Clear();

                foreach (var item in value)
                {
                    this.results.Add(item);
                }
            }
        }

        public async void GetData()
        {
            IEnumerable<BeerViewModel> resultData =
                await BeersInBulgaria.Data.DataPersister.GetAllBeers();
            foreach (var beer in resultData)
            {
                if (beer.BeerName.ToLower().Contains(this.QueryText.ToLower()))
                {
                    this.results.Add(beer);
                }
            }
        }
    }
}
