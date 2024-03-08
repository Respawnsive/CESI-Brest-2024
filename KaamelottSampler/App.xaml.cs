using Controls.UserDialogs.Maui;
using KaamelottSampler.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Maui.Audio;

namespace KaamelottSampler
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppCenter.Start("android =ba94abba-5b2f-433f-8e48-6e8e2f4cdd9e;uwp=e1f5a50e-6236-4416-8acd-ebe290f4948b", typeof(Analytics), typeof(Crashes));

            Services = ConfigureServices();

            MainPage = new AppShell();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton(AudioManager.Current);
            services.AddSingleton<KaamelottDataService>();
            services.AddSingleton(UserDialogs.Instance);

            return services.BuildServiceProvider();
        }
    }
}
