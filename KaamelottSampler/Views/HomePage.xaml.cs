using KaamelottSampler.Services;
using KaamelottSampler.ViewModels;

namespace KaamelottSampler.Views
{
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomePageViewModel();
        }

        
    }

}
