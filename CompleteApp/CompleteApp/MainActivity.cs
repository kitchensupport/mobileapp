using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RestSharp;
using LoginScreen;

namespace CompleteApp
{
    [Activity(Label = "Kitchen.Support", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.signInWithEmailButton);

            button.Click += signInClick;
        }
        void signInClick(object sender, EventArgs e)
        {
            //User clicks the sign in button
            FragmentTransaction t = FragmentManager.BeginTransaction();
            login dialog = new login();
            dialog.Show(t, "dialog fragment");
            dialog.myOnLogInComplete += logInComplete;

        }
        public void logInComplete(object sender, OnLogInEventArgs e) 
        {
            StartActivity(typeof(hub));

         }




    }

}

