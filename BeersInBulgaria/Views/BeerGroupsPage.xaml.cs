using System;
using System.Collections.Generic;
using System.Linq;
using BeersInBulgaria.Common;
using BeersInBulgaria.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace BeersInBulgaria.Views
{
    public sealed partial class BeerGroupsPage : LayoutAwarePage
    {
        public BeerGroupsPage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var sampleDataGroups = BeerGroupsViewModel.GetGroups((String)navigationParameter);
            this.DefaultViewModel["Groups"] = sampleDataGroups;
        }

        private void Header_Click(object sender, RoutedEventArgs e)
        {
            var group = (sender as FrameworkElement).DataContext;
            this.Frame.Navigate(typeof(Views.GroupDetailPage), ((BeerTypeViewModel)group).Id);
        }

        private void ItemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((BeerViewModel)e.ClickedItem).Id;
            this.Frame.Navigate(typeof(Views.BeerDetailsPage), itemId);
        }
    }
}
