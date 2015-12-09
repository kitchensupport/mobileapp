using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KitchenSupport
{
    public partial class HomePage : ContentPage
    {
        
        public HomePage()
        {
            this.Title = "  Kitchen.Support";
            SavedRecipesPage.lv = new ListView();
            SavedRecipesPage.lv2 = new ListView();
            SavedRecipesPage.lv3 = new ListView();
            InitializeComponent();
        }

        private void findRecipes(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new NavigationPage(new RecipeMenu()));
        }

        private void viewSavedRecipes(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new NavigationPage(new SavedRecipesPage()));
        }

        private void navigateToIngredientsView(object sender, EventArgs args)
        {
            Navigation.PushModalAsync(new NavigationPage(new IngredientView()));
        }

        private void navigateBack(object sender, EventArgs args)
        {
            //Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
            Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
           // Navigation.PopModalAsync();
        }
        private void logOut(object sender, EventArgs args)
        {
            DependencyService.Get<localDataInterface>().save("token", "");
            Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }
    }
}