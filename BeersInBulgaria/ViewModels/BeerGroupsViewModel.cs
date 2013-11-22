using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BeersInBulgaria.ViewModels
{
    public class BeerGroupsViewModel : BeersInBulgaria.Common.BindableBase
    {
        private static BeerGroupsViewModel _sampleDataSource = new BeerGroupsViewModel();

        private ObservableCollection<BeerTypeViewModel> _allGroups;

        public ObservableCollection<BeerTypeViewModel> AllGroups
        {
            get
            {
                if (this._allGroups == null)
                {
                    this._allGroups = new ObservableCollection<BeerTypeViewModel>();
                    this.GetData();
                }

                return this._allGroups;
            }
        }

        public static IEnumerable<BeerTypeViewModel> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            return _sampleDataSource.AllGroups;
        }

        public static BeerTypeViewModel GetGroup(int uniqueId)
        {
            var matches = _sampleDataSource.AllGroups.Where(group => group.Id.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                var match = matches.First();
                match.CommonImage = "ms-appx:/Assets/beer" + uniqueId + ".png";
                return match;
            }
            return null;
        }

        public static BeerViewModel GetItem(int uniqueId)
        {
            var matches = _sampleDataSource.AllGroups.SelectMany(group => group.Beers).Where((beer) => beer.Id.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }
            return null;
        }

        public async void GetData()
        {
            try
            {
                IEnumerable<BeerTypeViewModel> resultData =
                    await BeersInBulgaria.Data.DataPersister.GetAllBeersTypes();
                this.SetObservableValues(this.AllGroups, resultData);
            }

            catch (Exception)
            {
                new Windows.UI.Popups.MessageDialog("Не е налично съдържание. Проверете връзката си с интернет.").ShowAsync();
            }
        }
    }
}
