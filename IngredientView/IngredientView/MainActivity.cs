using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace IngredientView
{
	[Activity (Label = "IngredientView", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ListView.IOnItemClickListener
	{
		private List<string> ingredients;
		private ListView listView;
		private List<int> ingredientQuantity;
        private int newQuantity;
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            base.Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Main);
            listView = FindViewById<ListView>(Resource.Id.myListView);
            ingredientQuantity = new List<int>();
            ingredientQuantity.Add(2);
            ingredientQuantity.Add(3);
            ingredientQuantity.Add(4);
            ingredients = new List<string>();
            ingredients.Add("Ground Beef\n" + ingredientQuantity[0]);
            ingredients.Add("Cheese\n" + ingredientQuantity[1]);
            ingredients.Add("Milk\n" + ingredientQuantity[2]);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredients);
            listView.Adapter = adapter;

            listView.OnItemClickListener = this;
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {

            FragmentTransaction t = FragmentManager.BeginTransaction();
            dialog_ingredient dialog = new dialog_ingredient();
            dialog.Show(t, "dialog fragment");
            dialog.myOnUpdateComplete += Dialog_myOnUpdateComplete;
            
        }

        private void Dialog_myOnUpdateComplete(object sender, OnUpdateEventArgs e)
        {
            newQuantity = Int32.Parse(e.NewAmount);
            ingredientQuantity[0] = newQuantity;
            ingredients[0] = e.NewName + "\n" + newQuantity;
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredients);
            listView.Adapter = adapter;
        }
    }
}



