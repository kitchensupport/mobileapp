using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KitchenSupport
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void findRecipes(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new RecipeStream());
        }

        private void viewSavedRecipes(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new NavigationPage(new SavedRecipesPage()));
        }

        private void navigateToIngredientsView(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new NavigationPage(new Ingredients()));
        }

        private void navigateBack(object sender, EventArgs args)
        {
            //Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
            Navigation.PopModalAsync();
        }
    }
}