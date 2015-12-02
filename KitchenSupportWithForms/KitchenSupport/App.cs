using System;

using Xamarin.Forms;

namespace KitchenSupport
{
	public class App : Application
	{
        public static string StoredToken { get; internal set; }

        public App ()
		{
			// The root page of your application
			MainPage = new LoginPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

