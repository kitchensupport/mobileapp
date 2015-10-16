using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CompleteApp;

namespace IngredientView
{
    public class OnUpdateEventArgs : EventArgs
    {
        private string NewAmnt;

        private string Unit;

        public string NewAmount
        {
            get { return NewAmnt; }
            set { NewAmnt = value; }
        }

        public string NewUnit
        {
            get { return Unit; }
            set { Unit = value; }
        }
        public OnUpdateEventArgs(string newAmount, string unit) : base()
        {
            NewAmnt = newAmount;
            Unit = unit;
        }
    }
    class dialog_ingredient : DialogFragment
    {
        private Button closeDialogBtn;
        private EditText myEditText;
        private RadioGroup myRadioGroup;

        public event EventHandler<OnUpdateEventArgs> myOnUpdateComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Ingredient_View_update_amount, container, false);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            myEditText = view.FindViewById<EditText>(Resource.Id.editText1);
            closeDialogBtn = view.FindViewById<Button>(Resource.Id.button1);
            myRadioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            closeDialogBtn.Click += closeDialogBtn_Click;
            return view;
        }

        void closeDialogBtn_Click(object sender, EventArgs e)
        {
            //User clicks the update button
            int id = myRadioGroup.CheckedRadioButtonId;
            RadioButton selectedButton = View.FindViewById<RadioButton>(id);
            myOnUpdateComplete.Invoke(this, new OnUpdateEventArgs(myEditText.Text, selectedButton.Text));
            this.Dismiss();

        }
    }
}