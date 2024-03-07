using KaamelottSampler.Views;

namespace KaamelottSampler
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        void RegisterRoutes()
        {
            Routing.RegisterRoute("HomePage", typeof(HomePage));
            Routing.RegisterRoute("AboutPage", typeof(AboutPage));
            Routing.RegisterRoute("SampleDetailPage", typeof(SampleDetailPage));
        }
    }
}
