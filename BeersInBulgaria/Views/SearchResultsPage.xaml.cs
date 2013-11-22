using System;
using System.Collections.Generic;
using System.Linq;
using BeersInBulgaria.ViewModels;
using Windows.UI.Xaml.Controls;

namespace BeersInBulgaria.Views
{
    public sealed partial class SearchResultsPage : BeersInBulgaria.Common.LayoutAwarePage
    {
        BeersInBulgaria.ViewModels.SearchViewModel currentViewModel = null;

        public SearchResultsPage()
        {
            this.InitializeComponent();

            this.currentViewModel = new BeersInBulgaria.ViewModels.SearchViewModel();

            this.DataContext = this.currentViewModel;
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var queryText = navigationParameter as String;
            this.currentViewModel.QueryText = queryText;
        }

        private void ResultsGridViewItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((BeerViewModel)e.ClickedItem).Id;
            this.Frame.Navigate(typeof(BeerDetailsPage), itemId);
        }
    }
}
