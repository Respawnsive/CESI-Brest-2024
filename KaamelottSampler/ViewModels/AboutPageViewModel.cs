using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace KaamelottSampler.ViewModels
{
    public class AboutPageViewModel : ObservableObject
    {

        public AboutPageViewModel()
        {
            CurrentURL = "https://github.com/2ec0b4/kaamelott-soundboard";
        }

        #region BindableProperties

        private string _currentURL;
        public string CurrentURL
        {
            get
            {
                return _currentURL;
            }
            set
            {
                SetProperty(ref _currentURL, value);
            }
        }

        public ICommand GotoLinkCommand => new Command(async () => await OpenLinkURL());

        private async Task OpenLinkURL()
        {
            await Launcher.OpenAsync(CurrentURL);
        }

        #endregion


    }
}
