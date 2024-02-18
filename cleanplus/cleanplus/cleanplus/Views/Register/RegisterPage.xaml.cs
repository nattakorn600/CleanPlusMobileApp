using cleanplus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.Register
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		UserAccount UserRegister = new UserAccount(); 
		public RegisterPage()
		{
			InitializeComponent();
			this.BindingContext = UserRegister;
		}
		async void OnRegisterClick(object sender, EventArgs e)
		{
			if ((UserRegister.Email == null) || (UserRegister.Password == null) || (UserRegister.Name == null) || (UserRegister.Phone == null)
				|| (UserRegister.Email == "") || (UserRegister.Password == "") || (UserRegister.Name == "") || (UserRegister.Phone == ""))
			{
				await DisplayAlert("", "กรุณาระบุข้อมูลให้ครบถ้วน", "ยืนยัน");
			}
			else
			{
				using (var cl = new HttpClient())
				{
					var formcontent = new FormUrlEncodedContent(new[]
					{
					new KeyValuePair<string,string>("email",UserRegister.Email),
					new KeyValuePair<string, string>("pass",UserRegister.Password),
					new KeyValuePair<string, string>("phone",UserRegister.Phone),
					new KeyValuePair<string, string>("name",UserRegister.Name)
					});

					var request = await cl.PostAsync(Application.Current.Properties["domain"] +
						"/cleanplus/register/register.php?", formcontent);

					request.EnsureSuccessStatusCode();

					var response = await request.Content.ReadAsStringAsync();

					var res = JsonConvert.DeserializeObject<UserAccount>(response);

					if (res.Status == "success")
					{
						await Navigation.PopModalAsync();
					}
					else
					{
						await DisplayAlert("",res.Status,"ยืนยัน");
					}

				}
			}
		}
		void ToLogin(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}
	}
}