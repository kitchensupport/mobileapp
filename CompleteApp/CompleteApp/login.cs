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
using RestSharp;

namespace CompleteApp
{

    public class OnLogInEventArgs : EventArgs
    {
      

    }
    class login : DialogFragment
    {
        private Button logInBtn;
        private EditText emailField;
        private EditText passwordField;
        private Button registerBtn;
        private Button forgotBtn;
        private string email;
        private string password;

        public event EventHandler<OnLogInEventArgs> myOnLogInComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.login, container, false);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            emailField = view.FindViewById<EditText>(Resource.Id.editText1);
            passwordField = view.FindViewById<EditText>(Resource.Id.editText2);
            logInBtn = view.FindViewById<Button>(Resource.Id.button2);
            registerBtn = view.FindViewById<Button>(Resource.Id.button4);

            logInBtn.Click += logInBtn_Click;
            registerBtn.Click += regBtn_Click;
            return view;
        }

        void logInBtn_Click(object sender, EventArgs e)
        {
            var client = new RestClient("http://api.kitchen.support/accounts/login");
            var request = new RestRequest(Method.POST);
            email = emailField.Text;
            password = passwordField.Text;
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n    \"email\": \"" + email + "\",\n    \"password\": \"" + password + "\"\n}", ParameterType.RequestBody);
            client.ExecuteAsync(request, response =>
            {
                if (response.Content == "")
                {
                    //failCallback(new LoginScreenFaultDetails { PasswordErrorMessage = "Unable to login to account." });
                    throw new Exception("Unable to register account.");
                }
                else
                {
                    Console.WriteLine(response.Content);
                    //successCallback();
                }
            });
            myOnLogInComplete.Invoke(this, new OnLogInEventArgs());
            this.Dismiss();
        }
        void regBtn_Click(object sender, EventArgs e)
        {
            email = emailField.Text;
            password = passwordField.Text;
            //should get rid of username on this page.
            if (password.Length < 4)
            { //arbitrary
                //failCallback(new LoginScreenFaultDetails { PasswordErrorMessage = "Password must be at least 4 chars." });
            }
            else
            {
                var client = new RestClient("http://api.kitchen.support/accounts/create");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\n    \"email\": \"" + email + "\",\n    \"password\": \"" + password + "\"\n}", ParameterType.RequestBody);
                client.ExecuteAsync(request, response =>
                {
                    if (response.Content == "")
                    {
                        // failCallback(new LoginScreenFaultDetails { PasswordErrorMessage = "Unable to create account." });
                        throw new Exception("Unable to register account.");
                    }
                    else
                    {
                        Console.WriteLine(response.Content);
                        //successCallback();
                    }
                });
            }
            myOnLogInComplete.Invoke(this, new OnLogInEventArgs());
            this.Dismiss();
        }
    }
}