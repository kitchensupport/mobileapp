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
                            markComplete

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
