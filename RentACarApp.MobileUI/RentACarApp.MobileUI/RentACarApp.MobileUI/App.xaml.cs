using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQzMTg4QDMxMzcyZTM0MmUzMG1RdDhaUzZWdURKUUhla2hPZDV1TVU1MHZIS210dnZYWTRyRmN0cktFTzQ9;MjQzMTg5QDMxMzcyZTM0MmUzME9lMVpqVXRFTk9wZC9qclFtOHFYaWxpL2lZR2ZqZ2lweW9nVm1YWmNiQ1k9");

            InitializeComponent();

            MainPage = new RentACarApp.MobileUI.Views.Login.LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
