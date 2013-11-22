using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeersInBulgaria.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI.Xaml.Navigation;

namespace BeersInBulgaria.Views
{
    public sealed partial class BeerDetailsPage : BeersInBulgaria.Common.LayoutAwarePage
    {
        public BeerDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            var item = BeerGroupsViewModel.GetItem((int)navigationParameter);
            this.DefaultViewModel["Group"] = item.BeerType;
            this.DefaultViewModel["Beers"] = item.BeerType.Beers;
            this.flipView.SelectedItem = item;
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            var selectedItem = (BeerViewModel)this.flipView.SelectedItem;
            pageState["SelectedItem"] = selectedItem.Id;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();


            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
            DataRequestedEventArgs>(this.ShareTextHandler);

            base.OnNavigatedTo(e);

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();


            dataTransferManager.DataRequested -= new TypedEventHandler<DataTransferManager,
            DataRequestedEventArgs>(this.ShareTextHandler);
            base.OnNavigatedFrom(e);
        }

        private void ShareTextHandler(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.Properties.Title = "Share Text Example";
            request.Data.Properties.Description = "A demonstration that shows how to share text.";
            var item = flipView.SelectedItem as BeerViewModel;
            StringBuilder itemInfo = new StringBuilder();
            itemInfo.Append("<div><table border=\"1\" style=\"border-collapse:collapse;\">");
            itemInfo.Append("<tr><td>Име</td>" + "<td>" + item.BeerName + "</td>");
            itemInfo.Append("<tr><td>Бутилка</td>" + "<td><img src=\"" + item.ImageSource + "\" alt=\"W3Schools.com\" width=\"104\" height=\"142\"></td>");
            itemInfo.Append("<tr><td>Вид</td>" + "<td>" + item.BeerType.Name + "</td>");
            itemInfo.Append("<tr><td>Алкохолност (%)</td>" + "<td>" + item.AlcoholByVolume + "</td>");
            itemInfo.Append("<tr><td>Производител</td>" + "<td>" + item.BreweryName + "</td>");
            itemInfo.Append("<tr><td>Етикет</td>" + "<td><img src=\"" + item.LabelSource + "\" alt=\"W3Schools.com\" width=\"104\" height=\"142\"></td>");
            request.Data.SetHtmlFormat(HtmlFormatHelper.CreateHtmlFormat(itemInfo.ToString()));
        }
    }
}
