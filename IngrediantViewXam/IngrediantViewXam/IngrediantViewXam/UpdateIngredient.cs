using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace IngrediantViewXam
{
    public class UpdateIngredient : ContentPage
    {
        public static int amount;
        public static string unit;
        public UpdateIngredient(int newAmount, string newUnit)
        {
            
            Label header = new Label
            {
                Text = "Update Ingredient",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center

            };
            Entry e1 = new Entry
            {
                Placeholder = "Enter New Amount"
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
                    e1,
                    e2,
                    button
                }
            };
            button.Clicked += (sender, e) =>
            {
                amount = Int32.Parse(e1.Text);
                unit = e2.Text;
                newAmount = amount;
                newUnit = unit;
                Navigation.PopModalAsync();
            };

        }

        public int getAmount()
        {
            return amount;
        }
        public string getUnit()
        {
            return unit;
        }
        static void setAmnt(ref int amnt)
        {
            amnt = amount;
        }


    }
}

