using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;

using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace KitchenSupport
{
    public class RecipeStream : ContentPage
    {
        
        public class Recipes
        {
            public List<recipe> recipes { get; set; }
        }

        public List<recipe> parseRecipes(string request)
        {
            var result = JsonConvert.DeserializeObject<Recipes>(request);
            return result.recipes;
        }

        public class Ingredient
        {
            public string name { get; set; }
        }

        public RecipeStream()
        {
            var tapImage = new TapGestureRecognizer();
            int count = 0;
            var client = new HttpClient();
            string url = "http://api.kitchen.support/stream";
            var response = client.GetStringAsync(new Uri(url));
            
            if (response == null)
            {
                return;
            }
            Button back = new Button
            {
                Text = "Back",
                HorizontalOptions = LayoutOptions.Start
            };
            var recipes = parseRecipes(response.Result);
            var recipePic = new Image
            {
                Aspect = Aspect.AspectFill,
                HeightRequest = 200,
                WidthRequest = 200
            };
            recipePic.Source = ImageSource.FromUri(new Uri(recipes[count].smallImageUrls[0].Substring(0, recipes[count].smallImageUrls[0].Length - 4)));

            Label header = new Label
            {
                Text = "Your Recipe Stream",
                Font = Font.BoldSystemFontOfSize(30),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
               
            };
            Label recipeName = new Label
            {
                Text = recipes[0].recipeName,
                Font = Font.BoldSystemFontOfSize(20),
                HorizontalOptions = LayoutOptions.Center
            };
            Button like = new Button
            {
                Text = "Like",
                BackgroundColor = Color.FromHex("77D065")
            };
            Button dislike = new Button
            {
                Text = "Dislike",
                BackgroundColor = Color.FromHex("77D065")
            };
            Button openRecipe = new Button
            {
                Text = "More Info",
                BackgroundColor = Color.FromHex("77D065")
            };
            like.Clicked += (sender, e) =>
            {
                if (count == 30)
                {
                    response = client.GetStringAsync(new Uri(url));
                    if (response == null)
                    {
                        return;
                    }
                    recipes = parseRecipes(response.Result);
                    count = 0;
                }
                var likeClient = new HttpClient();
                string likeUrl = "http://api.kitchen.support/likes";
                string data = "{\n    \"api_token\" : \"" + DependencyService.Get<localDataInterface>().load("token") + "\",\n    \"recipe_id\" : \"" + recipes[count].id + "\"\n}";
                var httpContent = new StringContent(data);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var likeResponse = client.PostAsync(new Uri(likeUrl), httpContent);
                var ye = likeResponse.Result.StatusCode.ToString();
                if (likeResponse.Result.StatusCode.ToString() != "OK")
                {

                }
                count++;
                recipePic.Source = ImageSource.FromUri(new Uri(recipes[count].smallImageUrls[0].Substring(0,recipes[count].smallImageUrls[0].Length - 4)));
                recipeName.Text = recipes[count].recipeName;
                
            };
            dislike.Clicked += (sender, e) =>
            {
                if (count == 30)
                {
                    response = client.GetStringAsync(new Uri(url));
                    if (response == null)
                    {
                        return;
                    }
                    recipes = parseRecipes(response.Result);
                    count = 0;
                }
                var likeClient = new HttpClient();
                string likeUrl = "http://api.kitchen.support/likes";
                string data = "{\n    \"api_token\" : \"" + DependencyService.Get<localDataInterface>().load("token") + "\",\n    \"recipe_id\" : \"" + recipes[count].id + "\"\n}";
                var dislikeRequest = (HttpWebRequest)WebRequest.Create(likeUrl);
                dislikeRequest.Method = "DELETE";
                dislikeRequest.ContentType = "application/json";
                dislikeRequest.Accept = "application/json";
                var dislikeResponse = dislikeRequest.GetResponseAsync();
                if (dislikeResponse.Status.ToString() != "OK")
                {

                }
                count++;
                recipePic.Source = ImageSource.FromUri(new Uri(recipes[count].smallImageUrls[0].Substring(0, recipes[count].smallImageUrls[0].Length - 4)));
                recipeName.Text = recipes[count].recipeName;
                
            };
            back.Clicked += (sender, e) =>
            {
                Navigation.PopModalAsync();
            };
            var browser = new WebView();
            openRecipe.Clicked += async (sender, e) =>
            {
                await Navigation.PushModalAsync(new RecipeDetails(recipes[count]));
            };
            tapImage.Tapped += (sender, e) =>
            {
                Device.OpenUri(new Uri("http://www.yummly.com/recipe/" + recipes[count].yummly_id));
            };
            recipePic.GestureRecognizers.Add(tapImage);

            Content = new StackLayout
            {
                Spacing = 20,
                Padding = 50,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    back,
                    header,
                    recipeName,
                    recipePic,
                    like,
                    dislike,
                    openRecipe
                }
            };
        }
    }
}
