using System;
using System.Linq;
using TestApp.Permission.Interfaces;
using TestApp.Permission.Permissions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
{
    public partial class App : Application,IPermissionsChanged
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
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

        

        public void permissionsChanged(GeneralPermission[] changedPermissions)
        {
            if (changedPermissions.Any())
            {
                if (changedPermissions.Contains(GeneralPermission.Location))
                {
                    if (MainPage is MainPage)
                    {
                        var page = MainPage as MainPage;
                        page.toggleUserOnMap();
                    }
                }
            }
        }
    }
}
