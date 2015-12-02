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

namespace KitchenSupport
{
    public class RecipeStream : ContentPage
    {
        
        public class Recipes
        {
            public List<recipe> recipes { get; set; }
        }
        public class recipe
        {
            public string recipeName { get; set; }
            public string[] smallImageUrls { get; set; }
            public int id { get; set; }
            public string yummly_id { get; set; }
            public int rating { get; set; }
            public string[] ingredients { get; set; }

        }
        public List<recipe> parseRecipes(string request)
        {
            var result = JsonConvert.DeserializeObject<Recipes>(request);
            return result.recipes;
        }

        public class RecipeDetails : ContentPage
        {
            public RecipeDetails(recipe r)
            {
                var tapImage = new TapGestureRecognizer();

                string hourOrHours = "hours";
                string ingredientsString = "";
                for (int i = 0; i < r.ingredients.Length; i++)
                {
                    ingredientsString += r.ingredients[i] + "\n";
                }
                int time = 0;
                if (r != null)
                {
                    time = 3600;
                }
                else
                {
                    return;
                }
                
                double timeInhours = (double)(time) / 3600;
                double minutes = 0;
                if (timeInhours > 1)
                {
                    minutes = timeInhours - 1;
                    minutes *= 60;
                }
                if (timeInhours < 1)
                {
                    minutes = 60*timeInhours;
                    timeInhours = 0;
                }
                if (timeInhours == 1 && minutes == 0)
                {
                    hourOrHours = "hour";
                }
                Button back = new Button
                {
                    Text = "Back",
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };
                Label header = new Label
                {
                    Text = r.recipeName,
                    Font = Font.BoldSystemFontOfSize(40),
                    HorizontalOptions = LayoutOptions.Center
                };
                var recipePic = new Image
                {
                    Aspect = Aspect.AspectFill,
                    HeightRequest = 200,
                    WidthRequest = 200
                };
                string sMinutes = "";
                if (minutes < 10)
                {
                    sMinutes = "0" + minutes.ToString();
                }
                else
                {
                    sMinutes = minutes.ToString();
                }
                recipePic.Source = ImageSource.FromUri(new Uri(r.smallImageUrls[0].Substring(0, r.smallImageUrls[0].Length - 4)));
                Label details = new Label
                {
                    Text = "Rating: " + r.rating.ToString() + "/5\n\nTime to make: " + String.Format("{0:0}", timeInhours) + ":" + sMinutes + " " + hourOrHours + "\n\nIngredients:\n" + ingredientsString
                };
                tapImage.Tapped += (sender, e) =>
                {
                    Device.OpenUri(new Uri("http://www.yummly.com/recipe/" + r.yummly_id));
                };
                recipePic.GestureRecognizers.Add(tapImage);
                var scroll = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Spacing = 20,
                        Padding = 50,
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                        {
                            back,
                            header,
                            recipePic,
                            details
                        }
                    },
                };
                this.Content = scroll;
                /*{
                    Spacing = 20,
                    Padding = 50,
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        header,
                        recipePic,
                        details
                    }
                };*/
                back.Clicked += (sender, e) =>
                {
                    Navigation.PopModalAsync();
                };
            }
        }

        public RecipeStream()
        {
            var tapImage = new TapGestureRecognizer();
            int count = 1;
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
            recipePic.Source = ImageSource.FromUri(new Uri(recipes[0].smallImageUrls[0].Substring(0, recipes[0].smallImageUrls[0].Length - 4)));

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
                    count = 0;
                }
                recipePic.Source = ImageSource.FromUri(new Uri(recipes[count].smallImageUrls[0].Substring(0,recipes[count].smallImageUrls[0].Length - 4)));
                recipeName.Text = recipes[count].recipeName;
                count++;
            };
            dislike.Clicked += (sender, e) =>
            {
                if (count == 30)
                {
                    count = 0;
                }
                recipePic.Source = ImageSource.FromUri(new Uri(recipes[count].smallImageUrls[0].Substring(0, recipes[count].smallImageUrls[0].Length - 4)));
                recipeName.Text = recipes[count].recipeName;
                count++;
            };
            back.Clicked += (sender, e) =>
            {
                Navigation.PopModalAsync();
            };
            var browser = new WebView();
            openRecipe.Clicked += async (sender, e) =>
            {
                await Navigation.PushModalAsync(new RecipeDetails(recipes[count-1]));
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
