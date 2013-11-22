using System;
using System.Linq;
using Callisto.Controls;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml.Controls;

namespace BeersInBulgaria.Common
{
    public static class PrivacyPolicy
    {
        public static void Initialise()
        {
            SettingsPane settingsPane = SettingsPane.GetForCurrentView();

            settingsPane.CommandsRequested += (s, e) =>
            {
                SettingsCommand settingsCommand = new SettingsCommand(
                  "PrivacyPolicy",
                  "Privacy Policy",    // NB: should be in a resource for internationalisation  
                  command =>
                  {
                      SettingsFlyout flyout = new SettingsFlyout();
                      flyout.HeaderText = "Privacy Policy"; // NB: should be in a resource again.  
                      StackPanel stackPanel = new StackPanel();
                      TextBlock textBlock = new TextBlock()
                      {
                          Text =
                          "   Това приложение не използва данни от компютъра Ви, потребителски имена или пароли или каквато и да е друга информация. Никога не събираме и не обработваме данни на потребителите. Използването на Бирите в България е напълно безопасно." +
                          "Ако имате въпроси, моля пишете ни на: svetlio_ts@abv.bg\n" +
                          "\n   This application does not use your computer data, connection data, device information or anything else for any reason what so ever. We never collect or process anything from our users. All is safe when you use Бирите в България" +
                          "If you have any questions send us an email to: svetlio_ts@abv.bg"
                      };

                      textBlock.TextWrapping = Windows.UI.Xaml.TextWrapping.Wrap;
                      HyperlinkButton hyperlink = new HyperlinkButton();
                      stackPanel.Children.Add(textBlock);
                      stackPanel.Children.Add(hyperlink);
                      flyout.Content = stackPanel;

                      flyout.Width = (double)SettingsFlyout.SettingsFlyoutWidth.Wide;
                      flyout.FontSize = 18;
                      flyout.IsOpen = true;
                  }
                );
                e.Request.ApplicationCommands.Add(settingsCommand);
            };
        }
    }
}
