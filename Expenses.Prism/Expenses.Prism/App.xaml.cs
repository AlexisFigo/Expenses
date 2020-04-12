using Prism;
using Prism.Ioc;
using Expenses.Prism.ViewModels;
using Expenses.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Expenses.Common.Services;
using Syncfusion.Licensing;
using Expenses.Common.Helpers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Expenses.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjM4MzU0QDMxMzgyZTMxMmUzMGdwWEhOV0dpSnlFUVVDaVA4UkMreTlhbWlOVFdNWEo3YmdBR0tDeHRrSVU9");
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ExpensesMasterDetailPage, ExpensesMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<TripsPage, TripsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddTripPages, AddTripPagesViewModel>();
            containerRegistry.RegisterForNavigation<logoutPages, logoutPagesViewModel>();
            containerRegistry.RegisterForNavigation<TripDetailsPages, TripDetailsPagesViewModel>();
            containerRegistry.RegisterForNavigation<AddTripDetailsPage, AddTripDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
        }
    }
}
