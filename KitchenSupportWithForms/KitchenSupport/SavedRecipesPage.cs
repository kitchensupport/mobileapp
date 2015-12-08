using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace KitchenSupport
{
    public class SavedRecipesPage : ContentPage
    {
        public class Recipes
        {
            public List<recipe> recipes { get; set; }
        }
        public static ListView lv, lv2, lv3;
        public static List<recipe> parseRecipes(string request)
        {
            var result = JsonConvert.DeserializeObject<Recipes>(request);
            return result.recipes;
        }
        public SavedRecipesPage()
        {
            var client = new HttpClient();
            string url = "http://api.kitchen.support/likes?offset=0&limit=30&api_token=";
            string token = DependencyService.Get<localDataInterface>().load("token");
            url += token;
            var response = client.GetStringAsync(new Uri(url));
            if (response == null)
            {
                return;
            }
            var recipes = parseRecipes(response.Result);
            lv = new ListView
            {
                RowHeight = 45
            };
            lv.ItemsSource = recipes;
            lv.ItemTemplate = new DataTemplate(typeof(TextCell));
            lv.ItemTemplate.SetBinding(TextCell.TextProperty, ".recipeName");
            Button back = new Button
            {
                Text = "Back",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            Label label = new Label
            {
                Text = "Your Liked Recipes",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };
            lv.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                lv.SelectedItem = null;
                //await Navigation.PushModalAsync(new LikedRecipeDetails((recipe)e.SelectedItem));
                Navigation.PushModalAsync(new RecipeDetails((recipe)e.SelectedItem));
            };
            url = "http://api.kitchen.support/favorites?offset=0&limit=30&api_token=";
            url += token;
            response = client.GetStringAsync(new Uri(url));
            if (response == null)
            {
                return;
            }
            Label label2 = new Label
            {
                Text = "Your Favorited Recipes",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };
            recipes = parseRecipes(response.Result);
            lv2 = new ListView
            {
                RowHeight = 45
            };
            lv2.ItemsSource = recipes;
            lv2.ItemTemplate = new DataTemplate(typeof(TextCell));
            lv2.ItemTemplate.SetBinding(TextCell.TextProperty, ".recipeName");
            lv2.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                lv2.SelectedItem = null;
                await Navigation.PushModalAsync(new RecipeDetails((recipe)e.SelectedItem));
            };
            url = "http://api.kitchen.support/completed?offset=0&limit=30&api_token=";
            url += token;
            response = client.GetStringAsync(new Uri(url));
            if (response == null)
            {
                return;
            }
            Label label3 = new Label
            {
                Text = "Your Completed Recipes",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };
            recipes = parseRecipes(response.Result);
            lv3 = new ListView
            {
                RowHeight = 45
            };
            lv3.ItemsSource = recipes;
            lv3.ItemTemplate = new DataTemplate(typeof(TextCell));
            lv3.ItemTemplate.SetBinding(TextCell.TextProperty, ".recipeName");
            lv3.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                lv3.SelectedItem = null;
                await Navigation.PushModalAsync(new RecipeDetails((recipe)e.SelectedItem));
            };
            var scroll = new ScrollView
            {


                Content = new StackLayout
                {
                    Children = {
                    back,
                    label,
                    lv,
                    label2,
                    lv2,
                    label3,
                    lv3
                    }
                }
            };
            this.Content = scroll;
            back.Clicked += (sender, e) =>
            {
                Navigation.PopModalAsync();
            };
            
        }
    }

    public class Ingredient
    {
        public string name { get; set; }
    }
    public class LikedRecipeDetails : ContentPage
    {
        public LikedRecipeDetails(recipe r)
        {
            var tapImage = new TapGestureRecognizer();

            string[] ratingImageLinks = new string[] { "http://i.imgur.com/7qq8zdR.png", "http://i.imgur.com/BRwowMP.png", "http://i.imgur.com/dNUdKiO.png", "http://i.imgur.com/zK4JmCG.png", "http://i.imgur.com/61WSiZf.png", "http://i.imgur.com/7J7BYuv.png" };
            var ratingImage = new Image { Aspect = Aspect.AspectFit };
            ratingImage.Source = ImageSource.FromUri(new Uri(ratingImageLinks[r.rating]));


            string hourOrHours = "hours";
            string minuteOrMinutes = "minutes";

            int time = 0;
            if (r == null)
            {
                return;
            }

            Label cookingTimeLabel = new Label();

            if (r.totalTimeInSeconds == null)
            {
                cookingTimeLabel.Text = "";
                cookingTimeLabel.Font = Font.BoldSystemFontOfSize(1);
            }
            else
            {
                time = (int)r.totalTimeInSeconds;

                int hours = (int)(time / 3600);
                int leftOverSeconds = time - (hours * 3600);
                int minutes = (int)(leftOverSeconds / 60);


                if (hours == 1)
                {
                    hourOrHours = "hour";
                }

                if (minutes == 1)
                {
                    minuteOrMinutes = "minute";
                }

                String cookingTime = "";

                if (hours != 0)
                {
                    cookingTime += hours.ToString() + " " + hourOrHours;
                }

                if (minutes != 0)
                {
                    if (hours != 0)
                        cookingTime += ", ";

                    cookingTime += minutes.ToString() + " " + minuteOrMinutes;
                }

                cookingTimeLabel.Text = "Time to make: " + cookingTime;
                cookingTimeLabel.Font = Font.BoldSystemFontOfSize(25);
            }

            Label ingredientLabel = new Label
            {
                Text = "Ingredients:",
                Font = Font.BoldSystemFontOfSize(25)
            };
            ListView listview = new ListView();
            listview.RowHeight = 40;
            listview.ItemSelected += (sender, e) =>
            {
                listview.SelectedItem = null;
            };

            List<Ingredient> ingredients = new List<Ingredient>();
            for (int i = 0; i < r.ingredients.Length; i++)
            {
                ingredients.Add(new Ingredient { name = r.ingredients[i] });
            }
            listview.ItemsSource = ingredients;

            listview.ItemTemplate = new DataTemplate(typeof(TextCell));
            listview.ItemTemplate.SetBinding(TextCell.TextProperty, ".name");

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
            recipePic.Source = ImageSource.FromUri(new Uri(r.smallImageUrls[0].Substring(0, r.smallImageUrls[0].Length - 4)));
            tapImage.Tapped += (sender, e) =>
            {
                var client = new HttpClient();
                string url = "http://api.kitchen.support/favorites";
                string data = "{\n    \"api_token\" : \"" + DependencyService.Get<localDataInterface>().load("token") + "\",\n    \"recipe_id\" : \"" + r.id + "\"\n}";
                var httpContent = new StringContent(data);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(new Uri(url), httpContent);
                if (response.Result.StatusCode.ToString() != "OK")
                {

                }

            };
            recipePic.GestureRecognizers.Add(tapImage);

            Button markComplete = new Button
            {
                Text = "Mark as complete",
                BackgroundColor = Color.FromHex("77D065")
            };
            Button viewRecipeButton = new Button();
            viewRecipeButton.Text = "View Recipe";
            viewRecipeButton.BackgroundColor = Color.FromHex("77D065");

            viewRecipeButton.Clicked += delegate {
                Device.OpenUri(new Uri("http://www.yummly.com/recipe/" + r.yummly_id));
            };

            markComplete.Clicked += (sender, e) =>
            {
                var client = new HttpClient();
                string url = "http://api.kitchen.support/completed";
                string data = "{\n    \"api_token\" : \"" + DependencyService.Get<localDataInterface>().load("token") + "\",\n    \"recipe_id\" : \"" + r.id + "\"\n}";
                var httpContent = new StringContent(data);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync(new Uri(url), httpContent);
                if (response.Result.StatusCode.ToString() != "OK")
                {

                }
            };
            Button dislike = new Button
            {
                Text = "Dislike Recipe",
                BackgroundColor = Color.FromHex("77D065")
            };
            dislike.Clicked += (sender, e) => {
                /*var likeClient = new HttpClient();
                string likeUrl = "http://api.kitchen.support/likes";
                string data = "{\n    \"api_token\" : \"" + DependencyService.Get<localDataInterface>().load("token") + "\",\n    \"recipe_id\" : \"" + r.id + "\"\n}";
                var dislikeRequest = (HttpWebRequest)WebRequest.Create(likeUrl);
                dislikeRequest.Method = "DELETE";
                dislikeRequest.ContentType = "application/json";
                dislikeRequest.Accept = "application/json";
                //byte[] byt = "recipient=12345ABC";

                var dislikeResponse = dislikeRequest.GetResponseAsync();
                if (dislikeResponse.Status.ToString() != "OK")
                {

                }*/
                Navigation.PopModalAsync();
            };
            int favorites = (int)r.favorites;
            string favoritesStatement = "";

            if (favorites == 0 || r.favorites == null)
            {
                favoritesStatement = "Tap the image to be the first to like this recipe!";
            }
            else
            {
                favoritesStatement = "Tap the image to join the ";
                if (favorites == 1)
                {
                    favoritesStatement += "1 person who has favorited this recipe!";
                }
                else
                {
                    favoritesStatement += favorites.ToString() + " people who have favorited this recipe!";
                }
            }

            Label favoritesLabel = new Label
            {
                Text = favoritesStatement,
                Font = Font.BoldSystemFontOfSize(15),
                HorizontalOptions = LayoutOptions.Center
            };

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
                            favoritesLabel,
                            ratingImage,
                            cookingTimeLabel,
                            ingredientLabel,
                            listview,
                            viewRecipeButton,
                            markComplete,
                            dislike

                        }
                },
            };
            this.Content = scroll;
            back.Clicked += (sender, e) =>
            {
                Navigation.PopModalAsync();
            };
        }
    }


}
