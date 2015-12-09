using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace KitchenSupport
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void register(object sender, EventArgs args)
        {
            var client = new System.Net.Http.HttpClient();
            string url = "http://api.kitchen.support/accounts/create";
            if (password.Text != confirmPassword.Text)
                DisplayAlert("Alert", "Passwords do not match.", "OK");
            else if (password.Text == "" || email.Text == "")
            {
                DisplayAlert("Alert", "You must enter a value in all fields.", "OK");
            }
            else
            {
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
                    Navigation.PushModalAsync(new NavigationPage(new HomePage()));
                }
                else
                {
                    DisplayAlert("Alert", "Unable to create account at this time, sorry.", "OK");
                }
            }
        }

        private void navigateBack(object sender, EventArgs args)
        {
            Navigation.PopModalAsync();
        }
    }
}