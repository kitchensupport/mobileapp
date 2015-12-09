using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace KitchenSupport
{
    public class RecipeMenu : ContentPage
    {
        public RecipeMenu()
        {
            this.Title = "  Kitchen.Support";
            Button back = new Button
            {
                Text = "back",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            back.Clicked += (sender, e) => {
                Navigation.PopModalAsync();
            };
            Label header = new Label
            {
                Text = "Recipe Main Menu",
                Font = Font.BoldSystemFontOfSize(50),
            };
            var tapHead = new TapGestureRecognizer();
            tapHead.Tapped += (sender, e) =>
            {
                Navigation.PushModalAsync(new NavigationPage(new RecipeInterface()));
            };
            header.GestureRecognizers.Add(tapHead);
            Button stream = new Button
            {
                Text = "Recipe Stream",
                BackgroundColor = Color.FromHex("77D065")
            };
            stream.Clicked += (sender, e) =>
            {
                Navigation.PushModalAsync(new NavigationPage(new RecipeStream()));
            };
            Button search = new Button
            {
                Text = "Recipe Search",
                BackgroundColor = Color.FromHex("77D065")
            };
            search.Clicked += (sender, e) =>
            {
                Navigation.PushModalAsync(new NavigationPage(new RecipeSearch()));
            };
            Content = new StackLayout
            {
                Spacing = 20,
                Padding = 50,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    back,
                    header,
                    stream,
                    search
                }
            };
        }
    }
}
