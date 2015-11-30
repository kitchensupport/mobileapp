using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace IngrediantViewXam
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
                    button
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

            }





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
