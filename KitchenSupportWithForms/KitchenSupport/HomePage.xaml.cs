using IngrediantViewXam;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KitchenSupport
{
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

		private void findRecipes(object sender, EventArgs args){
			Navigation.PushModalAsync (new FindRecipesPage());
		}

		private void viewSavedRecipes(object sender, EventArgs args){
			Navigation.PushModalAsync (new SavedRecipesPage());
		}

		private void navigateToIngredientsView(object sender, EventArgs args){
			Navigation.PushModalAsync (new Ingredients());
		}
	}
}

