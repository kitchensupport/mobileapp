using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace KitchenSupport
{
	public class App : Application
	{
		public static string StoredToken;
		public App ()
		{
			if (IsLoggedIn)
				MainPage = new HomePage ();
			else {
				MainPage = new LoginPage ();
			}
		}
			
		public static bool IsLoggedIn {
			//if(DependencyService.Get<ISaveAndLoad> ().LoadText ("token.txt") != null)
			get { 
				//returns Boolean for Login
				return !string.IsNullOrWhiteSpace(StoredToken); 
			}
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

