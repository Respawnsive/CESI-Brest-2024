using KaamelottSampler.Services;
using KaamelottSampler.ViewModels;

namespace KaamelottSampler.Views
{
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();
            var dataservice = this.Handler.MauiContext.Services.GetServices<KaamelottDataService>().FirstOrDefault();
            BindingContext = new HomePageViewModel(dataservice);
        }

        
    }

}
