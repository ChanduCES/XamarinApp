using AutoMapper;
using Flurl.Http;
using Flurl.Http.Configuration;
using MyXamarinApp.DTO;
using MyXamarinApp.Localization;
using MyXamarinApp.Models;
using MyXamarinApp.Services;
using MyXamarinApp.Services.Interfaces;
using MyXamarinApp.ViewModels;
using MyXamarinApp.Views;
using Prism;
using Prism.Ioc;
using System.Net.Http;
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
            containerRegistry.RegisterSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
            FlurlHttp.Configure(settings => {
                settings.HttpClientFactory = new UntrustedCertClientFactory();
            });

            LocalizationResourceManager.Current.PropertyChanged += (_, _) => AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);

            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<EmployeesPage, EmployeesPageViewModel>();


            containerRegistry.RegisterSingleton<IEmployeeService, EmployeeService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeResponseDTO, EmployeeModel>();
                cfg.CreateMap<EmployeeModel, EmployeeRequestDTO>();
            }); 

            containerRegistry.RegisterInstance(config.CreateMapper());
        }
    }

    /// <summary>
    /// To ignore SSL errors when running from emulators. 
    /// https://learn.microsoft.com/en-us/xamarin/cross-platform/deploy-test/connect-to-local-web-services#bypass-the-certificate-security-check
    /// </summary>
    public class UntrustedCertClientFactory : DefaultHttpClientFactory
    {
        public override HttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            return new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });
        }
       
    }
}
