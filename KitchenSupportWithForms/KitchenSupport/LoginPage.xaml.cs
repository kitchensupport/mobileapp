using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace KitchenSupport
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}
		int count = 0;
		public void OnButtonClicked(object sender, EventArgs args)
		{
			count++;

			((Button)sender).Text = 
				String.Format("{0} click{1}!", count, count == 1 ? "" : "s");
		}
	}
}

