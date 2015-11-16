using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

using Xamarin.Forms;

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
            else
            {
                string data = "{\n    \"email\" : \"" + email.Text + "\",\n    \"password\" : \"" + password.Text + "\"\n}";
                var httpContent = new StringContent(data);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(new Uri(url), httpContent);

                if (response.Result.StatusCode.ToString() == "OK")
                {
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