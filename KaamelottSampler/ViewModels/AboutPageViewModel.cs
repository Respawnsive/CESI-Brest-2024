using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Windows.Input;

namespace KaamelottSampler.ViewModels
{
    public class AboutPageViewModel : ReactiveObject
    {

        public AboutPageViewModel()
        {
            CurrentURL = "https://github.com/2ec0b4/kaamelott-soundboard";
        }

        #region BindableProperties

        [Reactive]
        public string CurrentURL { get; set; }


        public ICommand GotoLinkCommand => new Command(async () => await OpenLinkURL());

        private async Task OpenLinkURL()
        {
            await Launcher.OpenAsync(CurrentURL);
        }

        #endregion


    }
}
