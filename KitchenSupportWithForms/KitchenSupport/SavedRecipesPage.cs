using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public class recipe
        {
            public string recipeName { get; set; }
            public string[] smallImageUrls { get; set; }
            public int id { get; set; }
            public string yummly_id { get; set; }
            public int rating { get; set; }
            public string[] ingredients { get; set; }
            public int? totalTimeInSeconds { get; set; }

        }

        public List<recipe> parseRecipes(string request)
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
            ListView lv = new ListView
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
            lv.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }
                lv.SelectedItem = null;
                await Navigation.PushModalAsync(new RecipeStream.RecipeDetails((RecipeStream.recipe)e.SelectedItem));
            };

            Content = new StackLayout
            {
                Children = {
                    back,
                    label,
                    lv
                }
            };
            back.Clicked += (sender, e) =>
            {
                Navigation.PopModalAsync();
            };
        }
    }
}
