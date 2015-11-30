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
        private string NewAmnt;

        public string NewAmount
        {
            get { return NewAmnt; }
            set { NewAmnt = value; }
        }
        public OnUpdateEventArgs(string newAmount) : base()
        {
            NewAmnt = newAmount;
        }
    }
    class dialog_ingredient : DialogFragment
    {
        private Button closeDialogBtn;
        private EditText myEditText;

        public event EventHandler<OnUpdateEventArgs> myOnUpdateComplete;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.update_amount, container, false);

            myEditText = view.FindViewById<EditText>(Resource.Id.editText1);
            closeDialogBtn = view.FindViewById<Button>(Resource.Id.button1);


            closeDialogBtn.Click += closeDialogBtn_Click;
            return view;
        }

        void closeDialogBtn_Click(object sender, EventArgs e)
        {
            //User clicks the update button
            myOnUpdateComplete.Invoke(this, new OnUpdateEventArgs(myEditText.Text));
            this.Dismiss();

        }
    }
}