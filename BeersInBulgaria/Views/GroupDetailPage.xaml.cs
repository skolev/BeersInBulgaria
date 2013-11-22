using System;
using System.Collections.Generic;
using System.Linq;
using BeersInBulgaria.ViewModels;
using Windows.UI.Xaml.Controls;

namespace BeersInBulgaria.Views
{
    public sealed partial class GroupDetailPage : BeersInBulgaria.Common.LayoutAwarePage
    {
        public GroupDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            var group = BeerGroupsViewModel.GetGroup((int)navigationParameter);
            this.DefaultViewModel["Group"] = group;
            this.DefaultViewModel["Beers"] = group.Beers;
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            var selectedItem = (BeerTypeViewModel)this.DefaultViewModel["Group"];
            pageState["SelectedItem"] = selectedItem.Id;
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemId = ((BeerViewModel)e.ClickedItem).Id;
            this.Frame.Navigate(typeof(BeersInBulgaria.Views.BeerDetailsPage), itemId);
        }
    }
}
