using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KitchenSupport
{
    public class IngredientView : ContentPage
    {
        public static ListView listview;
        public static List<ingredient> ingredients;

        public List<ingredient> parseIngredients(string request)
        {
            var result = JsonConvert.DeserializeObject<ingredientResponse>(request);
            return result.items;
        }
        public IngredientView()
        {
            var client = new HttpClient();
            string url = "http://api.kitchen.support/pantry?offset=0&limit=30&api_token=";
            string token = DependencyService.Get<localDataInterface>().load("token");
            url += token;
            var response = client.GetStringAsync(new Uri(url));
            List<ingredient> ingredients = parseIngredients(response.Result);
            Label header = new Label
            {
                Text = "Your Ingredients",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };
            listview = new ListView
            {
                RowHeight = 30
            };

            listview.ItemsSource = ingredients;

            listview.ItemTemplate = new DataTemplate(typeof(TextCell));
            listview.ItemTemplate.SetBinding(TextCell.TextProperty, ".term");
            Button button = new Button();
            button.Text = "Add Ingredient";



            button.Clicked += async (sender, e) =>
            {
                ingredient newIngredient = new ingredient();
                await Navigation.PushModalAsync(new AddIngredient(newIngredient));
            };

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    listview,
                    button
                }
            };

        }
        
        public class ingredientResponse
        {
            public List<ingredient> items { get; set; }
        }

        public class ingredient
        {
            public int id { get; set; }
            public string searchValue { get; set; }
            public string term { get; set; }

        }



        public class AddIngredient : ContentPage
        {

            public AddIngredient(ingredient i)
            {

                Label header = new Label
                {
                    Text = "Add Ingredient",
                    Font = Font.BoldSystemFontOfSize(50),
                    HorizontalOptions = LayoutOptions.Center

                };
                Entry e1 = new Entry
                {
                    Placeholder = "Enter New Ingredient"
                };
                Button button = new Button
                {
                    Text = "Enter",
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex("77D065")
                };
                Button button2 = new Button
                {
                    Text = "Scan Ingredient Barcode",
                    TextColor = Color.White,
                    BackgroundColor = Color.FromHex("77D065")
                };
                this.Content = new StackLayout
                {
                    Spacing = 20,
                    Padding = 50,
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                {
                    header,
                    e1,
                    button,
                    button2
                }
                };
                button.Clicked += async (sender, e) =>
                {
                    i.searchValue = e1.Text;
                    i.term = e1.Text;
                    ingredients.Add(i);
                    listview.ItemsSource = null;
                    listview.ItemsSource = ingredients;
                    await Navigation.PopModalAsync();
                };
                button2.Clicked += async (sender, e) =>
                {
                    var data1 = await DependencyService.Get<IScanner>().Scan();
                    if (data1 == "null")
                    {
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        var client = new System.Net.Http.HttpClient();
                        string url = "http://api.walmartlabs.com/v1/items?apiKey=h5yjnkgcrmtzn3rjaduapusv&upc=" + data1;
                        var response = client.GetAsync(new Uri(url));
                        if (response.Result.StatusCode.ToString() == "OK")
                        {
                            var message = response.Result.Content.ReadAsStringAsync().Result;
                            //var result = JsonConvert.DeserializeObject<Items>(message);
                            //List<Item> information = result.data;
                            //var item = information.ToList().First();
                            //var name = item.name;
                            string word = "";
                            string name = "";
                            for (int location = 0; location < message.Length - 4; location++)
                            {
                                word = (message.Substring(location, 4));
                                if (word == "name")
                                {
                                    int j = location + 7;
                                    char c = message[j];
                                    while (c != '"')
                                    {
                                        name += message[j];
                                        j++;
                                        c = message[j];
                                    }
                                    break;
                                }
                            }
                            await DisplayAlert("Item Found", name, "OK");
                            //var json = JObject.Parse (message);
                            //var token = json ["name"];
                            //await DisplayAlert ("Scanner", token.ToString(), "OK");
                            //await storeToken(token.ToString());
                            e1.Text = name;
                            //await Navigation.PushModalAsync (new NavigationPage(new HomePage()));
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Could not find item from UPC code.", "OK");
                            await DisplayAlert("Scanner", data1, "OK");
                            await DisplayAlert("Response", response.Result.StatusCode.ToString(), "OK");
                        }
                    }

                };

            }
        }

        public class Items
        {
            public List<Item> data { get; set; }
        }

        public class Item
        {
            public string itemId { get; set; }
            public string parentItemId { get; set; }
            public string name { get; set; }
            public string salePrice { get; set; }
            public string upc { get; set; }
            public string categoryPath { get; set; }
            public string shortDescription { get; set; }
            public string longDescription { get; set; }
            public string brandName { get; set; }
            public string thumbnailImage { get; set; }
            public string category { get; set; }
            public string mediumImage { get; set; }
            public string largeImage { get; set; }
            public string productTrackingUrl { get; set; }
            public string ninetySevenCentShipping { get; set; }
            public string standardShipRate { get; set; }
            public string marketplace { get; set; }
            public string shipToStore { get; set; }
            public string freeShipToStore { get; set; }
            public string modelNumber { get; set; }
            public string productUrl { get; set; }
            public string bundle { get; set; }
            public string clearance { get; set; }
            public string preOrder { get; set; }
            public string stock { get; set; }
            public string addToCartUrl { get; set; }
            public string affiliateAddToCartUrl { get; set; }
            public string freeShippingOver50Dollars { get; set; }
            public string maxItemsInOrder { get; set; }
            public List<Options> giftOptions { get; set; }
            public string availableOnline { get; set; }
        }

        public class Options
        {
            public string allowGiftWrap { get; set; }
            public string allowGiftMessage { get; set; }
            public string allowGiftReceipt { get; set; }
        }



    }


}