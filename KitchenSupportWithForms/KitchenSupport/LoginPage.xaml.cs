using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

using Xamarin.Forms;

namespace KitchenSupport
{
	public partial class LoginPage : TabbedPage //, INotifyPropertyChanged
	{
		public LoginPage ()
		{
			this.LoginCommand = new Command<string>((key) =>
				{
					login(username.Text,password.Text);
				});
			InitializeComponent ();
		}

		/*public void OnButtonClicked(object sender, EventArgs args)
		{
			count++;

			((Button)sender).Text = 
				String.Format("{0} click{1}!", count, count == 1 ? "" : "s");
		}
		*/
	
		public ICommand LoginCommand { protected set; get; }

		private void login(string username, string password){

			var client = new System.Net.Http.HttpClient ();
			client.BaseAddress = new Uri("http://api.kitchen.support/accounts/login");
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
			var response = client.PostAsync("{\n    \"email\": \"" + email + "\",\n    \"password\": \"" + password + "\"\n}");
			if (response.Status == 200) {
				//success
			}
		}
	}
}

