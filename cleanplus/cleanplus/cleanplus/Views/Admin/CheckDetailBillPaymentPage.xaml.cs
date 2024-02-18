using cleanplus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckDetailBillPaymentPage : ContentPage
    {
        public AdminData DataAdmin = new AdminData();
        public CheckDetailBillPaymentPage(AdminData Data)
        {
            InitializeComponent();
            BindingContext = Data;
            DataAdmin = Data;
        }

        async void CheckFail(object sender, EventArgs e)
        {
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("id",DataAdmin.Id.ToString()),
                    new KeyValuePair<string,string>("user_id",DataAdmin.User_Id.ToString()),
                    new KeyValuePair<string, string>("payment","2")
                });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/cleanplus/admin/payment.php?", formcontent);

                request.EnsureSuccessStatusCode();

                var response = await request.Content.ReadAsStringAsync();

                var res = JsonConvert.DeserializeObject<UserAccount>(response);
                if(res.Status == "success")
				{
                    await Navigation.PopAsync();
				}
            }
        }

        async void CheckPass(object sender, EventArgs e)
		{
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("id",DataAdmin.Id.ToString()),
                    new KeyValuePair<string,string>("user_id",DataAdmin.User_Id.ToString()),
                    new KeyValuePair<string, string>("payment","1")
                });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/cleanplus/admin/payment.php?", formcontent);

                request.EnsureSuccessStatusCode();

                var response = await request.Content.ReadAsStringAsync();

                var res = JsonConvert.DeserializeObject<UserAccount>(response);
                if (res.Status == "success")
                {
                    await Navigation.PopAsync();
                }
            }
        }

         void BackButtonClick(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopAsync();
        }
    }
}