using System;
using System.Timers;
using Newtonsoft.Json;
using RestSharp;

using LoginScreen;

namespace LoginScreen.Android.Sample
{
	public class TestCredentialsProvider : ICredentialsProvider
	{
		readonly Random rnd = new Random ();

		public bool NeedLoginAfterRegistration {
			get { return false; }
		}

		public bool ShowPasswordResetLink {
			get { return true; }
		}

		public bool ShowRegistration {
			get { return true; }
		}

		public void Login ( string email, string password, Action successCallback, Action<LoginScreenFaultDetails> failCallback)
		{
			var client = new RestClient ("http://api.kitchen.support/accounts/login");
			var request = new RestRequest (Method.POST);
			request.AddHeader ("content-type", "application/json");
			request.AddParameter ("application/json", "{\n    \"email\": \"" + email + "\",\n    \"password\": \"" + password + "\"\n}",ParameterType.RequestBody);
			client.ExecuteAsync (request, response => {
				if(response.Content == ""){
					failCallback (new LoginScreenFaultDetails { PasswordErrorMessage = "Unable to login to account." });
					throw new Exception("Unable to register account.");
				}
				else{
					Console.WriteLine (response.Content);
					successCallback ();
				}
			});
		}

		public void Register (string username, string email, string password, Action successCallback, Action<LoginScreenFaultDetails> failCallback)
		{
			//should get rid of username on this page.
			if (password.Length < 4) {
				failCallback (new LoginScreenFaultDetails { PasswordErrorMessage = "Password must be at least 4 chars." });
			} else {
				var client = new RestClient ("http://api.kitchen.support/accounts/create");
				var request = new RestRequest (Method.POST);
				request.AddHeader ("content-type", "application/json");
				request.AddParameter ("application/json", "{\n    \"email\": \"" + email + "\",\n    \"password\": \"" + password + "\"\n}",ParameterType.RequestBody);
				client.ExecuteAsync (request, response => {
					if(response.Content == ""){
						failCallback (new LoginScreenFaultDetails { PasswordErrorMessage = "Unable to create account." });
						throw new Exception("Unable to register account.");
					}
					else{
						Console.WriteLine (response.Content);
						successCallback ();
					}
				});
			}
		}

		public void ResetPassword (string email, Action successCallback, Action<LoginScreenFaultDetails> failCallback)
		{
			DelayInvoke (EquiprobableSelect (successCallback, () => failCallback (new LoginScreenFaultDetails { CommonErrorMessage = "Something wrong happened." })));
		}

		private void DelayInvoke (Action operation)
		{
			Timer timer = new Timer ();
			timer.AutoReset = false;
			timer.Interval = 3000;
			timer.Elapsed += (object sender, ElapsedEventArgs e) => operation.Invoke ();
			timer.Start ();
		}

		private T EquiprobableSelect<T> (T first, T second)
		{
			return rnd.Next (100) < 50 ? first : second;
		}
	}
}

