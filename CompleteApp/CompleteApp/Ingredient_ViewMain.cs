using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using CompleteApp;

namespace IngredientView
{
    [Activity(Label = "Ingredient_View", MainLauncher = true, Icon = "@drawable/icon")]
    public class Ingredient_View_MainActivity : Activity, ListView.IOnItemClickListener
    {
        private List<string> ingredients;
        private ListView listView;
        private List<int> ingredientQuantity;
        private List<string> units;
        private List<string> food;
        private int newQuantity;
        private string newUnit;
        private int selectedItem;
        private Button addIngredient;
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            base.Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Ingredient_View_Main);
            listView = FindViewById<ListView>(Resource.Id.myListView);
            addIngredient = FindViewById<Button>(Resource.Id.addIngredient);
            ingredientQuantity = new List<int>();
            ingredientQuantity.Add(2);
            ingredientQuantity.Add(3);
            ingredientQuantity.Add(4);
            units = new List<string>();
            units.Add("Pounds");
            units.Add("Ounces");
            units.Add("Cups");
            food = new List<string>();
            food.Add("Ground Beef");
            food.Add("Cheese");
            food.Add("Milk");
            ingredients = new List<string>();
            ingredients.Add(food[0] + "\n" + ingredientQuantity[0] + " " + units[0]);
            ingredients.Add(food[1] + "\n" + ingredientQuantity[1] + " " + units[1]);
            ingredients.Add(food[2] + "\n" + ingredientQuantity[2] + " " + units[2]);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredients);
            listView.Adapter = adapter;

            listView.OnItemClickListener = this;
            addIngredient.Click += addNewIngredient;
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            selectedItem = position;
            FragmentTransaction t = FragmentManager.BeginTransaction();
            dialog_ingredient dialog = new dialog_ingredient();
            dialog.Show(t, "dialog fragment");
            dialog.myOnUpdateComplete += Dialog_myOnUpdateComplete;

        }

        private void Dialog_myOnUpdateComplete(object sender, OnUpdateEventArgs e)
        {
            newQuantity = Int32.Parse(e.NewAmount);
            newUnit = e.NewUnit;
            ingredientQuantity[selectedItem] = newQuantity;
            units[selectedItem] = newUnit;
            ingredients[selectedItem] = food[selectedItem] + "\n" + newQuantity + " " + newUnit;
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredients);
            listView.Adapter = adapter;
        }
        void addNewIngredient(object sender, EventArgs e)
        {
            //User clicks the update button
            FragmentTransaction t = FragmentManager.BeginTransaction();
            AddIngredient dialog = new AddIngredient();
            dialog.Show(t, "dialog fragment");
            dialog.myOnAddComplete += Dialog_myOnAddComplete;

        }
        private void Dialog_myOnAddComplete(object sender, OnAddEventArgs e)
        {
            newQuantity = Int32.Parse(e.Amount);
            newUnit = e.NewUnit;
            string newFood = e.NewFood;
            food.Add(newFood);
            ingredientQuantity.Add(newQuantity);
            units.Add(newUnit);
            ingredients.Add(newFood + "\n" + newQuantity + " " + newUnit);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, ingredients);
            listView.Adapter = adapter;
        }
    }
}



