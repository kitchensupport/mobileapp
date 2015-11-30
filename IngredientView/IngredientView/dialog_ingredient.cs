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

namespace IngredientView
{
    public class OnUpdateEventArgs : EventArgs
    {
        private string _NewAmount;

        public string NewAmount
        {
			get { return _NewAmount; }
			set { _NewAmount = value; }
        }

		private string _NewName;
		public string NewName {
			get { return _NewName; }
			set { _NewName = value; }
		}

		public OnUpdateEventArgs(string newName, string newAmount) : base()
        {
            _NewAmount = newAmount;
			_NewName = newName;
        }
    }
    class dialog_ingredient : DialogFragment
    {
        private Button closeDialogBtn;
        private EditText amountText;
		private EditText ingredientText;


        public event EventHandler<OnUpdateEventArgs> myOnUpdateComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.update_amount, container, false);

			amountText = view.FindViewById<EditText>(Resource.Id.amountText);
			ingredientText = view.FindViewById<EditText> (Resource.Id.ingredientText);
            closeDialogBtn = view.FindViewById<Button>(Resource.Id.button1);


            closeDialogBtn.Click += closeDialogBtn_Click;
            return view;
        }

        void closeDialogBtn_Click(object sender, EventArgs e)
        {
            //User clicks the update button
			myOnUpdateComplete.Invoke(this, new OnUpdateEventArgs(ingredientText.Text, amountText.Text));
            this.Dismiss();

        }
    }
}