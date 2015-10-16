using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IngredientView;

namespace CompleteApp
{
    [Activity(Label = "Kitchen.Support")]
    public class hub : Activity
    {
        private Button ingredientBtn;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.hub);
            ingredientBtn = FindViewById<Button>(Resource.Id.button1);
            ingredientBtn.Click += toIngredientView;
        }

        void toIngredientView(object sender, EventArgs e)
        {
            StartActivity(typeof(Ingredient_View_MainActivity));
        }
    }
}