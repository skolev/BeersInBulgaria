using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace BeersInBulgaria.ViewModels
{
    public class BeerTypeViewModel : BeersInBulgaria.Common.BindableBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CommonImage { get; set; }

        public IEnumerable<BeerViewModel> BeersEnum;

        private ObservableCollection<BeerViewModel> beers;
        private ObservableCollection<BeerViewModel> topBeers;

        public BeerTypeViewModel()
        {
            Beers.CollectionChanged += ItemsCollectionChanged;
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12 || Beers.Count != 0)
                    {
                        TopBeers.Insert(e.NewStartingIndex, Beers[e.NewStartingIndex]);
                        if (TopBeers.Count > 12)
                        {
                            TopBeers.RemoveAt(12);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        TopBeers.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        TopBeers.RemoveAt(e.OldStartingIndex);
                        TopBeers.Add(Beers[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        TopBeers.Insert(e.NewStartingIndex, Beers[e.NewStartingIndex]);
                        TopBeers.RemoveAt(12);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        TopBeers.RemoveAt(e.OldStartingIndex);
                        if (Beers.Count >= 12)
                        {
                            TopBeers.Add(Beers[11]);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        TopBeers[e.OldStartingIndex] = Beers[e.OldStartingIndex];
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopBeers.Clear();
                    while (TopBeers.Count < Beers.Count && TopBeers.Count < 12)
                    {
                        TopBeers.Add(Beers[TopBeers.Count]);
                    }
                    break;

            }
        }

        public ObservableCollection<BeerViewModel> Beers
        {
            get
            {
                if (this.beers == null)
                {
                    this.beers = new ObservableCollection<BeerViewModel>();

                }
                if (this.beers.Count == 0 && this.BeersEnum != null)
                {
                    foreach (var beer in BeersEnum)
                    {
                        beer.BeerType = this;
                        this.beers.Add(beer);
                    }
                }
                return this.beers;
            }
            set
            {
                if (this.beers == null)
                {
                    this.beers = new ObservableCollection<BeerViewModel>();
                }
                this.SetObservableValues(this.beers, value);
            }
        }

        public ObservableCollection<BeerViewModel> TopBeers
        {
            get 
            {
                if (this.topBeers == null)
                {
                    this.topBeers = new ObservableCollection<BeerViewModel>();
                }
                return this.topBeers; 
            }
        }
    }
}
