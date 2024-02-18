using cleanplus.Models;
using cleanplus.Views.Register;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.User
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");
		string pass = Application.Current.Properties["user_pass"].ToString();
		public ProfilePage()
		{
			InitializeComponent();
			labName.Text = Application.Current.Properties["user_name"].ToString();
			labEmail.Text = Application.Current.Properties["user_email"].ToString();
			labPhone.Text = Application.Current.Properties["user_phone"].ToString();
			labAddress.Text = Application.Current.Properties["user_address"].ToString();

			entName.Text = Application.Current.Properties["user_name"].ToString();
			entPass.Text = Application.Current.Properties["user_pass"].ToString();
			entEmail.Text = Application.Current.Properties["user_email"].ToString();
			entPhone.Text = Application.Current.Properties["user_phone"].ToString();
			entAddress.Text = Application.Current.Properties["user_address"].ToString();
		}

		async void updateData()
		{
			using (var cl = new HttpClient())
			{
				var formcontent = new FormUrlEncodedContent(new[]
				{
						new KeyValuePair<string,string>("name", labName.Text),
						new KeyValuePair<string, string>("pass", pass),
						new KeyValuePair<string,string>("id", Application.Current.Properties["user_id"].ToString()),
						new KeyValuePair<string, string>("email", labEmail.Text),
						new KeyValuePair<string,string>("phone",labPhone.Text),
						new KeyValuePair<string, string>("address",labAddress.Text),
					});

				var request = await cl.PostAsync(Application.Current.Properties["domain"] +
					"/cleanplus/register/updateprofile.php?", formcontent);

				request.EnsureSuccessStatusCode();

				var response = await request.Content.ReadAsStringAsync();

				var res = JsonConvert.DeserializeObject<UserAccount>(response);

				if (res.Status == "success")
				{
					if (File.Exists(_fileName))
					{
						File.Delete(_fileName);
					}

					UserAccount user = new UserAccount()
					{
						Name = labName.Text,
						Password = pass,
						Id = Application.Current.Properties["user_id"].ToString(),
						Email = labEmail.Text,
						Phone = labPhone.Text,
						Address = labAddress.Text
					};

					string json = JsonConvert.SerializeObject(user, Formatting.Indented);
					File.WriteAllText(_fileName, json);

					Application.Current.Properties["user_id"] = user.Id;
					Application.Current.Properties["user_email"] = user.Email;
					Application.Current.Properties["user_name"] = user.Name;
					Application.Current.Properties["user_pass"] = user.Password;
					Application.Current.Properties["user_phone"] = user.Phone;
					Application.Current.Properties["user_address"] = user.Address;
					Application.Current.Properties["user_status"] = user.Status;
				}
			}
		}

		void OnSignoutClick(object sender, EventArgs e)
		{
			if (File.Exists(_fileName))
			{
				File.Delete(_fileName);
			}

			Application.Current.Properties["user_id"] = null;
			Application.Current.Properties["user_Email"] = null;
			Application.Current.Properties["user_name"] = null;
			Application.Current.Properties["user_pass"] = null;
			Application.Current.Properties["user_phone"] = null;
			Application.Current.Properties["user_address"] = null;
			Application.Current.Properties["user_status"] = null;


			Application.Current.MainPage = new LoginPage();
		}

		async void btnNameClick(object sender, EventArgs e)
		{
			if (labName.IsVisible)
			{
				btnName.Source = "check.png";
				labName.IsVisible = false;
				entName.IsVisible = true;

			}
			else if (entName.Text == "")
			{
				await DisplayAlert("", "กรุณาระบุข้อมูล", "ยืนยัน");
			}
			else if (entName.Text != "")
			{
				labName.Text = entName.Text;

				btnName.Source = "pen.png";
				labName.IsVisible = true;
				entName.IsVisible = false;
				updateData();
			}
		}
		async void btnPassClick(object sender, EventArgs e)
		{
			if (labPass.IsVisible)
			{
				btnPass.Source = "check.png";
				labPass.IsVisible = false;
				entPass.IsVisible = true;
			}
			else if (entPass.Text == "")
			{
				await DisplayAlert("", "กรุณาระบุข้อมูล", "ยืนยัน");
			}
			else if (entPass.Text != "")
			{
				btnPass.Source = "pen.png";
				labPass.IsVisible = true;
				entPass.IsVisible = false;
				pass = entPass.Text;
				updateData();
			}
		}
		async void btnEmailClick(object sender, EventArgs e)
		{
			if (labEmail.IsVisible)
			{
				btnEmail.Source = "check.png";
				labEmail.IsVisible = false;
				entEmail.IsVisible = true;
			}
			else if (entEmail.Text == "")
			{
				await DisplayAlert("", "กรุณาระบุข้อมูล", "ยืนยัน");
			}
			else if (entEmail.Text != "")
			{
				labEmail.Text = entEmail.Text;

				btnEmail.Source = "pen.png";
				labEmail.IsVisible = true;
				entEmail.IsVisible = false;
				updateData();
			}
		}
		async void btnPhoneClick(object sender, EventArgs e)
		{
			if (labPhone.IsVisible)
			{
				btnPhone.Source = "check.png";
				labPhone.IsVisible = false;
				entPhone.IsVisible = true;
			}
			else if (entPhone.Text == "")
			{
				await DisplayAlert("", "กรุณาระบุข้อมูล", "ยืนยัน");
			}
			else if (entPhone.Text != "")
			{
				labPhone.Text = entPhone.Text;

				btnPhone.Source = "pen.png";
				labPhone.IsVisible = true;
				entPhone.IsVisible = false;
				updateData();
			}
		}
		async void btnAddressClick(object sender, EventArgs e)
		{
			if (labAddress.IsVisible)
			{
				btnAddress.Source = "check.png";
				labAddress.IsVisible = false;
				entAddress.IsVisible = true;
			}
			else if (entAddress.Text == "")
			{
				await DisplayAlert("", "กรุณาระบุข้อมูล", "ยืนยัน");
			}
			else if (entAddress.Text != "")
			{
				labAddress.Text = entAddress.Text;

				btnAddress.Source = "pen.png";
				labAddress.IsVisible = true;
				entAddress.IsVisible = false;
				updateData();
			}
		}

		async void BtnPointClick(object sender, EventArgs e)
        {
			await Shell.Current.GoToAsync("//ProfilePage/PointPage");
        }
    }
}