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
using CompleteApp;

namespace IngredientView
{

    public class OnAddEventArgs : EventArgs
    {
        private string Amnt;
        private string Food;
        private string Unit;

        public string Amount
        {
            get { return Amnt; }
            set { Amnt = value; }
        }

        public string NewUnit
        {
            get { return Unit; }
            set { Unit = value; }
        }

        public string NewFood
        {
            get { return Food; }
            set { Food = value; }
        }
        public OnAddEventArgs(string food, string amount, string unit) : base()
        {
            Food = food;
            Amnt = amount;
            Unit = unit;
        }
    }
    class AddIngredient : DialogFragment
    {
        private Button addButton;
        private EditText enterIngredient;
        private EditText enterAmount;
        private RadioGroup unit;
        public event EventHandler<OnAddEventArgs> myOnAddComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Ingredient_View_AddIngredient, container, false);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            addButton = view.FindViewById<Button>(Resource.Id.addButton);
            enterAmount = view.FindViewById<EditText>(Resource.Id.quantity);
            enterIngredient = view.FindViewById<EditText>(Resource.Id.foodText);
            unit = view.FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            addButton.Click += addBtn_Click;
            return view;
        }

        void addBtn_Click(object sender, EventArgs e)
        {
            // User clicks the add button
            int id = unit.CheckedRadioButtonId;
            RadioButton selectedButton = View.FindViewById<RadioButton>(id);
            string food = enterIngredient.Text;
            string amount = enterAmount.Text;
            myOnAddComplete.Invoke(this, new OnAddEventArgs(food, amount, selectedButton.Text));
            this.Dismiss();

        }
    }
}