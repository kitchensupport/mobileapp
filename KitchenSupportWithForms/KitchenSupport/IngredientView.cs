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
    public class Ingredients : ContentPage
    {
        public static int newAmount = 0;
        public static string newUnit = "";
        public static ingredient ing;
        public static ListView listview;
        public static List<ingredient> ingredients;
        public Ingredients()
        {

            Label header = new Label
            {
                Text = "Your Ingredients",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };
            listview = new ListView
            {
                RowHeight = 50
            };

            ingredients = new List<ingredient>();
            ingredients.Add(new ingredient { name = "Orange", quantity = 1, unit = " " });
            ingredients.Add(new ingredient { name = "Beef", quantity = 2, unit = "Pounds" });
            ingredients.Add(new ingredient { name = "Milk", quantity = 10, unit = "Cups" });
            listview.ItemsSource = ingredients;

            listview.ItemTemplate = new DataTemplate(typeof(TextCell));
            listview.ItemTemplate.SetBinding(TextCell.TextProperty, ".name");
            listview.ItemTemplate.SetBinding(TextCell.DetailProperty, ".unitAndQuantity");
            Button button = new Button();
            button.Text = "Add Ingredient";



            button.Clicked += async (sender, e) =>
            {
                ingredient newIngredient = new ingredient();
                await Navigation.PushModalAsync(new AddIngredient(newIngredient));
            };

            // Build the page.

            listview.ItemSelected += async (sender, e) => {

                //UpdateIngredient u = new UpdateIngredient(newAmount, newUnit);
                if (e.SelectedItem == null)
                    return;
                listview.SelectedItem = null; // deselect row
                ing = (ingredient)e.SelectedItem;
                await Navigation.PushModalAsync(new UpdateIngredient());

                //((ingredient)e.SelectedItem).quantity = Ingredients.newAmount;
                //((ingredient)e.SelectedItem).unit = Ingredients.newUnit;

            };
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

        public class ingredient
        {
            public string name { get; set; }
            public int quantity { get; set; }
            public string unit { get; set; }

            public string unitAndQuantity
            {
                get
                {
                    return quantity + " " + unit;
                }
            }
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

                Entry e2 = new Entry
                {
                    Placeholder = "Enter New Amount",
                    Keyboard = Keyboard.Numeric

                };
                Entry e3 = new Entry
                {
                    Placeholder = "Enter New Unit"
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
                    e2,
                    e3,
                    button,
                    button2
                }
                };
                button.Clicked += async (sender, e) =>
                {
                    i.name = e1.Text;
                    i.quantity = Int32.Parse(e2.Text); ;
                    i.unit = e3.Text;
                    ingredients.Add(i);
                    listview.ItemsSource = null;
                    listview.ItemsSource = ingredients;
                    await Navigation.PopModalAsync();
                };
                button2.Clicked += async (sender, e) =>
                {
                    var data1 = await DependencyService.Get<IScanner>().Scan();

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


        public class UpdateIngredient : ContentPage
        {

            public UpdateIngredient()
            {

                Label header = new Label
                {
                    Text = "Update Ingredient",
                    Font = Font.BoldSystemFontOfSize(50),
                    HorizontalOptions = LayoutOptions.Center

                };
                Entry e1 = new Entry
                {
                    Placeholder = "Enter New Amount",
                    Keyboard = Keyboard.Numeric

                };
                Entry e2 = new Entry
                {
                    Placeholder = "Enter New Unit"
                };
                Button button = new Button
                {
                    Text = "Enter",
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
                    e2,
                    button
                }
                };
                button.Clicked += async (sender, e) =>
                {
                    Ingredients.newAmount = Int32.Parse(e1.Text); ;
                    Ingredients.newUnit = e2.Text;
                    ing.quantity = Ingredients.newAmount;
                    ing.unit = Ingredients.newUnit;
                    listview.ItemsSource = null;
                    listview.ItemsSource = ingredients;
                    await Navigation.PopModalAsync();
                };

            }





        }

    }


}