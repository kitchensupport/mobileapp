using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

using Xamarin.Forms;

namespace KitchenSupport
{
	public partial class LoginPage : TabbedPage //, INotifyPropertyChanged
	{
		public LoginPage ()
		{
			/*this.LoginCommand = new Command<string>((key) =>
				{
					//load the main page
				});*/
			InitializeComponent ();
		}

		/*public void OnButtonClicked(object sender, EventArgs args)
		{
			count++;

			((Button)sender).Text = 
				String.Format("{0} click{1}!", count, count == 1 ? "" : "s");
		}
		*/
	
		//public ICommand LoginCommand { protected set; get; }
	}
}

