using KaamelottSampler.ViewModels;

namespace KaamelottSampler.Views;

public partial class SampleDetailPage : ContentPage
{
	public SampleDetailPage()
	{
		InitializeComponent();
        BindingContext = new SampleDetailPageViewModel();
    }
}