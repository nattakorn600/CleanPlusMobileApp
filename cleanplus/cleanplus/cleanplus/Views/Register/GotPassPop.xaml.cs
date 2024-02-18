using cleanplus.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
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
	public partial class GotPassPop
	{
		UserAccount User = new UserAccount();
		public GotPassPop()
		{
			InitializeComponent();
			this.BindingContext = User;
		}
		async void BtnSend(object sender, EventArgs e)
		{
			using (var cl = new HttpClient())
			{
				var formcontent = new FormUrlEncodedContent(new[]
				{
						new KeyValuePair<string,string>("email",User.Email)
				});

				var request = await cl.PostAsync(Application.Current.Properties["domain"] +
					"/cleanplus/register/forgot.php?", formcontent);

				request.EnsureSuccessStatusCode();

				var response = await request.Content.ReadAsStringAsync();

				var res = JsonConvert.DeserializeObject<UserAccount>(response);

				if (res.Status == "success")
				{
					showstatus.TextColor = Color.Green;
					showstatus.Text = "ส่งรหัสผ่านไปยังอีเมลของท่านแล้ว";
				}
				else
				{
					showstatus.TextColor = Color.Red;
					showstatus.Text = res.Status;
				}
			}
		}
		void Cancel(object sender, EventArgs e)
		{
			PopupNavigation.Instance.PopAsync();
		}
	}
}