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
    public partial class ResetPage : ContentPage
    {
        public ResetPage()
        {
            InitializeComponent();
        }

        private void submitClicked(object sender, EventArgs args)
        {
            var client = new System.Net.Http.HttpClient();
            string url = "http://api.kitchen.support/accounts/reset/request";
            string data = "{\n    \"email\" : \"" + email.Text + "\"\n}";
            var httpContent = new StringContent(data);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync(new Uri(url), httpContent);

            if (response.Result.StatusCode.ToString() == "OK")
            {
                ContentPage conformationPage = new ConformationPage();
                Navigation.PushModalAsync(conformationPage);
            }
            else
            {
                throw new Exception("Bad response from server forgot password request.");
            }
        }
    }
}