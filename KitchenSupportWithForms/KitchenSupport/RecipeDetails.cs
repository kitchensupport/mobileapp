using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace KitchenSupport
{
    public class RecipeDetails : ContentPage
    {
        public RecipeDetails(recipe r)
        {
            this.Title = "  Kitchen.Support";
            var tapImage = new TapGestureRecognizer();

            Button dislike = new Button
            {
                Text = "Dislike this recipe",
                BackgroundColor = Color.FromHex("77D065")
            };
            dislike.Clicked += (sender, e) =>
            {
               
                var likeClient = new HttpClient();
                string likeUrl = "http://api.kitchen.support/likes";
                string data = "{\n    \"api_token\" : \"" + DependencyService.Get<localDataInterface>().load("token") + "\",\n    \"recipe_id\" : \"" + r.id + "\",\n    \"value\" : " + "false" + "\n}";
                var httpContent = new StringContent(data);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var likeResponse = likeClient.PostAsync(new Uri(likeUrl), httpContent);
                var ye = likeResponse.Result.StatusCode.ToString();
                if (likeResponse.Result.StatusCode.ToString() != "OK")
                {

                }
                var client = new HttpClient();
                var url = "http://api.kitchen.support/likes?offset=0&limit=30&api_token=";
                url += DependencyService.Get<localDataInterface>().load("token");
                var response = client.GetStringAsync(new Uri(url));

                if (response == null)
                {
                    return;
                }
                var recipes = SavedRecipesPage.parseRecipes(response.Result);
                SavedRecipesPage.lv.ItemsSource = null;
                SavedRecipesPage.lv.ItemsSource = recipes;
            };
            Button unfavorite = new Button
            {
                Text = "Unfavorite this recipe",
                BackgroundColor = Color.FromHex("77D065")
            };
            unfavorite.Clicked += (sender, e) =>
            {
              
                var favClient = new HttpClient();
                string favUrl = "http://api.kitchen.support/favorites";
                string data = "{\n    \"api_token\" : \"" + DependencyService.Get<localDataInterface>().load("token") + "\",\n    \"recipe_id\" : \"" + r.id + "\",\n    \"value\" : " + "false" + "\n}";
                var httpContent = new StringContent(data);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var favResponse = favClient.PostAsync(new Uri(favUrl), httpContent);
                var ye = favResponse.Result.StatusCode.ToString();
                if (favResponse.Result.StatusCode.ToString() != "OK")
                {

                }
                var client = new HttpClient();
                var url = "http://api.kitchen.support/favorites?offset=0&limit=30&api_token=";
                url += DependencyService.Get<localDataInterface>().load("token");
                var response = client.GetStringAsync(new Uri(url));

                if (response == null)
                {
                    return;
                }
                var recipes = SavedRecipesPage.parseRecipes(response.Result);
                SavedRecipesPage.lv2.ItemsSource = null;
                SavedRecipesPage.lv2.ItemsSource = recipes;
            };
            Button uncomplete = new Button
            {
                Text = "Mark as incomplete",
                BackgroundColor = Color.FromHex("77D065")
            };
            uncomplete.Clicked += (sender, e) =>
            {
          
                var compClient = new HttpClient();
                string compUrl = "http://api.kitchen.support/completed";
                string data = "{\n    \"api_token\" : \"" + DependencyService.Get<localDataInterface>().load("token") + "\",\n    \"recipe_id\" : \"" + r.id + "\",\n    \"value\" : " + "false" + "\n}";
                var httpContent = new StringContent(data);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var favResponse = compClient.PostAsync(new Uri(compUrl), httpContent);
                var ye = favResponse.Result.StatusCode.ToString();
                if (favResponse.Result.StatusCode.ToString() != "OK")
                {

                }
                var client = new HttpClient();
                var url = "http://api.kitchen.support/completed?offset=0&limit=30&api_token=";
                url += DependencyService.Get<localDataInterface>().load("token");
                var response = client.GetStringAsync(new Uri(url));

                if (response == null)
                {
                    return;
                }
                var recipes = SavedRecipesPage.parseRecipes(response.Result);
                SavedRecipesPage.lv3.ItemsSource = null;
                SavedRecipesPage.lv3.ItemsSource = recipes;
            };
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
            int favorites = (int)r.favorites;
            string favoritesStatement = "";

            if (favorites == 0 || r.favorites == null)
            {
                favoritesStatement = "Tap the image to be the first to favorite this recipe!";
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
            if (r.favorited == true)
            {
                favoritesStatement = "You have already favorited this recipe!";
            }

            Label favoritesLabel = new Label
            {
                Text = favoritesStatement,
                Font = Font.BoldSystemFontOfSize(15),
                HorizontalOptions = LayoutOptions.Center
            };
            var stack = new StackLayout
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
                            viewRecipeButton
                            

                        }
            };
            if (r.completed == false)
            {
                stack.Children.Add(markComplete);
            }
            if (r.favorited == true)
            {
                stack.Children.Add(unfavorite);
            }
            if (r.liked == true)
            {
                stack.Children.Add(dislike);
            }
            if (r.completed == true)
            {
                stack.Children.Add(uncomplete);
            }
            var scroll = new ScrollView
            {
                Content = stack
            };
            this.Content = scroll;  
            
            
            back.Clicked += (sender, e) =>
            {

                Navigation.PopModalAsync();
                
            };
        }
    }
}
