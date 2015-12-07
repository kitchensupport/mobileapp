using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KitchenSupport
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            string accountUrl = "http://api.kitchen.support/account?api_token=";
            accountUrl += DependencyService.Get<localDataInterface>().load("token");
            //accountUrl += "yolo";
            var client = new HttpClient();
            var accountResponseTest = client.GetAsync(new Uri(accountUrl));
            if (accountResponseTest.Result.StatusCode.ToString() != "OK")
            {
                InitializeComponent();
            }
            else
            {
                //var accountResponse = client.GetStringAsync(new Uri(accountUrl));
                //var status = parseAccountResponse(accountResponse.Result);
                Navigation.PushModalAsync(new NavigationPage(new HomePage()));
            }
            
            
            
        }
    

        public class AccountResponse
        {
            public string status { get; set; }
        }

        public string parseAccountResponse(string request)
        {
            var result = JsonConvert.DeserializeObject<AccountResponse>(request);
            return result.status;
        }
        async private void login(object sender, EventArgs args)
        {
            var client = new HttpClient();

            string url = "http://api.kitchen.support/accounts/login";
           
            string data = "{\n    \"email\" : \"" + email.Text + "\",\n    \"password\" : \"" + password.Text + "\"\n}";
            var httpContent = new StringContent(data);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync(new Uri(url), httpContent);

            if (response.Result.StatusCode.ToString() == "OK")
            {
                var message = response.Result.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(message);
                var token = json["api_token"];
                //await storeToken(token.ToString());
                DependencyService.Get<localDataInterface>().save("token", token.ToString());

                // read: DependencyService.Get<localDataInterface> ().load();
                await Navigation.PushModalAsync(new NavigationPage(new HomePage()));
            }
            else
            {
                await DisplayAlert("Alert", "Invalid Username or Password.", "OK");
            }
            
            
        }



        private void register(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new NavigationPage(new RegisterPage()));
        }

        private void forgotPassword(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new NavigationPage(new ResetPage()));

        }
    }
}
