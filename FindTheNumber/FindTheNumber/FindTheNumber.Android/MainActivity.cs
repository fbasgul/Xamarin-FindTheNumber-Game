using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace FindTheNumber.Droid
{
    [Activity(Label = "FindTheNumber", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //statusbar
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            //end

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //App.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels); // / Resources.DisplayMetrics.Density);
            //App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels); // / Resources.DisplayMetrics.Density);
            this.Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn); //ekran uyku moduna geçmeyi engeller
            LoadApplication(new App());
        }
        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait; //ekranın dönmesini engeller
        }
    }
}