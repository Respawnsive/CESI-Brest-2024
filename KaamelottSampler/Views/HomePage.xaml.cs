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

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            Task.Run(async() =>
            {
                await Task.WhenAll(
                    ClearButton.RotateTo(360, 2000, Easing.Default),
                    Lbl_Char.FadeTo(0, 1000, Easing.Default),
                    Lbl_Char.FadeTo(1, 1000, Easing.Default),
                    Cb_Char.RotateTo(90, 1000, Easing.Default),
                    Cb_Char.RotateTo(0, 1000, Easing.Default)
                );
            });
        }
    }

}
