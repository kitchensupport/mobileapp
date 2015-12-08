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
            var ye = response.Result.ToString();
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
    


}
