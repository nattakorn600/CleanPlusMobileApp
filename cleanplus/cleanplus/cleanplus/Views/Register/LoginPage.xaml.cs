using cleanplus.Models;
using cleanplus.Controls;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.Register
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");
		UserAccount UserLogin = new UserAccount();
		public LoginPage()
		{
			InitializeComponent();
			var statusbar = DependencyService.Get<IStatusBarPlatformSpecific>();
			statusbar.SetStatusBarColor(Color.FromHex("#00000000"));
			this.BindingContext = UserLogin;
		}
		async void OnLoginClick(object sender, EventArgs e)
		{
			if ((UserLogin.Name == null) || (UserLogin.Password == null) || (UserLogin.Name == "") || (UserLogin.Password == ""))
			{
				await DisplayAlert("","กรุณาระบุข้อมูลให้ครบถ้วน","ยืนยัน");
			}
			else
			{
				using (var cl = new HttpClient())
				{
					var formcontent = new FormUrlEncodedContent(new[]
					{
						new KeyValuePair<string,string>("name",UserLogin.Name),
						new KeyValuePair<string, string>("pass",UserLogin.Password)
					});

					var request = await cl.PostAsync(Application.Current.Properties["domain"] +
						"/cleanplus/register/login.php?", formcontent);

					request.EnsureSuccessStatusCode();

					var response = await request.Content.ReadAsStringAsync();

					var res = JsonConvert.DeserializeObject<UserAccount>(response);

					if (res.Status != "fail")
					{
						string json = JsonConvert.SerializeObject(res, Formatting.Indented);
						File.WriteAllText(_fileName, json);

						Application.Current.Properties["user_id"] = res.Id;
						Application.Current.Properties["user_email"] = res.Email;
						Application.Current.Properties["user_name"] = res.Name;
						Application.Current.Properties["user_pass"] = res.Password;
						Application.Current.Properties["user_phone"] = res.Phone;
						Application.Current.Properties["user_address"] = res.Address;
						Application.Current.Properties["emp_id"] = res.Emp_Id;
						Application.Current.Properties["emp_name"] = res.Emp_Name;
						Application.Current.Properties["user_status"] = res.Status;

						if (res.Status == "empoyee")
						{
							App.Current.MainPage = new EmpoyeeShell();
						}
						else if(res.Status == "admin")
						{
							App.Current.MainPage = new AdminShell();
						}
						else
						{
							App.Current.MainPage = new AppShell();
						}
						//await Navigation.PushAsync(new HomePage());
					}
					else
					{
						await DisplayAlert("", "ชื่อหรือรหัสผ่านไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง", "ยืนยัน");
					}
				}
			}
		}
		void ToRegister(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new RegisterPage());
		}
		void ForgotPassword(object sender, EventArgs e)
		{
			PopupNavigation.Instance.PushAsync(new GotPassPop());
		}
	}
}