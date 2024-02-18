using cleanplus.Models;
using cleanplus.Views;
using cleanplus.Views.Register;
using cleanplus.Views.User;
using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus
{
	public partial class App : Application
	{
		string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");
		public App()
		{
			InitializeComponent();
			Application.Current.Properties["domain"] = "https://www.mahamhorr.com";

			/*if (File.Exists(_fileName))
			{
				File.Delete(_fileName);
			}*/
			//MainPage = new AppShell();
		}

		protected override void OnStart()
		{
			if (!(File.Exists(_fileName)))
            {
                MainPage = new SlidePage();
                //File.Create(session).Dispose();
            }
            else
            {
                UserAccount data = new UserAccount();
                string jsondata = File.ReadAllText(_fileName);
                data = JsonConvert.DeserializeObject<UserAccount>(jsondata);

				Application.Current.Properties["user_id"] = data.Id;
				Application.Current.Properties["user_email"] = data.Email;
				Application.Current.Properties["user_name"] = data.Name;
				Application.Current.Properties["user_pass"] = data.Password;
				Application.Current.Properties["user_phone"] = data.Phone;
				Application.Current.Properties["user_address"] = data.Address;
				Application.Current.Properties["emp_id"] = data.Emp_Id;
				Application.Current.Properties["emp_name"] = data.Emp_Name;
				Application.Current.Properties["user_status"] = data.Status;

				if (data.Status == "empoyee")
				{
					MainPage = new EmpoyeeShell();
				}
				else if (data.Status == "admin")
				{
					MainPage = new AdminShell();
				}
				else
				{
					MainPage = new AppShell();
				}
				//MainPage = new Controls.TransitionNavigationPage(new Views.User.Help.HelpPage()) { BarBackgroundColor = Color.FromHex("#FFF") };
				//MainPage = new AppShell();
			}
		}


		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
