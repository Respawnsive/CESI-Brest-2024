using KaamelottSampler.Services;
using Plugin.Maui.Audio;

namespace KaamelottSampler
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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

            return services.BuildServiceProvider();
        }
    }
}
