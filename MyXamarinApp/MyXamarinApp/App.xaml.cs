using Akavache;
using MyXamarinApp.Localization;
using MyXamarinApp.Services;
using MyXamarinApp.Services.Interfaces;
using MyXamarinApp.ViewModels;
using MyXamarinApp.Views;
using Prism;
using Prism.Ioc;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MyXamarinApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/EmployeesPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Akavache.Registrations.Start("MyXamarinApp");
            containerRegistry.RegisterInstance(BlobCache.LocalMachine);

            LocalizationResourceManager.Current.PropertyChanged += (_, _) => AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);

            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<EmployeesPage, EmployeesPageViewModel>();


            containerRegistry.RegisterSingleton<IBlobStorageService, BlobStorageService>();
        }
    }
}
