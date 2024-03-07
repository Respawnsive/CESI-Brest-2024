using KaamelottSampler.Services;
using KaamelottSampler.ViewModels;

namespace KaamelottSampler.Views
{
    public partial class AboutPage : ContentPage
    {

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutPageViewModel();
        }

        
    }

}
