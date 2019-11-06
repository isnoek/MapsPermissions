using System;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TestApp.Permission.Interfaces;
using TestApp.Permission.Permissions;

namespace TestApp.Droid
{
    [Activity(Label = "TestApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int RequestLocationId = 0;
        private App parentApp;
        private readonly string[] LocationPermissions =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this,savedInstanceState);
            this.parentApp = new App();
            LoadApplication(parentApp);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            if (requestCode == RequestLocationId)
            {
                if ((grantResults.Length == 2) && (grantResults[0] == (int) Android.Content.PM.Permission.Granted))
                {
                    this.showText("OnRequest: permission granted");
                    var parent = parentApp as IPermissionsChanged;
                    parent.permissionsChanged(new GeneralPermission[]{GeneralPermission.Location});
                }
                else
                {
                    this.showText("OnRequest: permission denied");
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode,permissions,grantResults);
            }
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnStart()
        {
            base.OnStart();
            if ((int) Build.VERSION.SdkInt >= 23)
            {
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted)
                {
                    RequestPermissions(LocationPermissions,RequestLocationId);
                }
                else
                {
                    this.showText("Permission already granted");
                }
            }
        }

        //private stuff
        private void showText(string textToShow)
        {
            Toast.MakeText(this,textToShow,ToastLength.Long).Show();
        }
    }
}