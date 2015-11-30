using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace KitchenSupport
{
	public partial class LoginPage : ContentPage //, INotifyPropertyChanged
	{
		public LoginPage ()
		{
			/*this.LoginCommand = new Command<string>((key) =>
				{
					login(email.Text,password.Text);
				});*/
			InitializeComponent ();
		}

		/*public void OnButtonClicked(object sender, EventArgs args)
		{
			count++;

			((Button)sender).Text = 
				String.Format("{0} click{1}!", count, count == 1 ? "" : "s");
		}
		*/
	
		//public ICommand LoginCommand { protected set; get; }

		private void login(object sender, EventArgs args){
			var client = new System.Net.Http.HttpClient ();
			string url = "http://api.kitchen.support/accounts/login";
			string data = "{\n    \"email\" : \"" + email.Text + "\",\n    \"password\" : \"" + password.Text + "\"\n}";
			var httpContent = new StringContent (data);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue ("application/json");
			var response = client.PostAsync(new Uri(url), httpContent);

			if (response.Result.StatusCode.ToString() == "OK") {
				Navigation.PushModalAsync (new HomePage());
			}
			else {
				throw new Exception ("Bad response from server loggin in.");
			}
		}

		private void register(object sender, EventArgs args){
			Navigation.PushModalAsync (new RegisterPage());
		}

		private void forgotPassword(object sender, EventArgs args){
			Navigation.PushModalAsync (new ResetPage());

		}
	}
}

