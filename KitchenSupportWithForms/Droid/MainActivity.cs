using System;

using Android;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Util;
using Android.Support.V4.App;

using Xamarin.Forms.Platform.Android;
using Android.Support.V4.Content;

namespace KitchenSupport.Droid
{
	[Activity(Label = "Kitchen.Support", MainLauncher = true, Icon = "@drawable/icon",
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity//, ActivityCompat.IOnRequestPermissionsResultCallback
    {

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication (new App ());
		}
	}
}
