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

namespace cleanplus.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHomePage : ContentPage
    {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");

        public ObservableCollection<AdminData> DataAdmin;

        public AdminHomePage()
        {
            InitializeComponent();
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
            ReadDataAsync();
        }
		public async void ReadDataAsync()
        {
            var uri = new Uri(Application.Current.Properties["domain"] + "/cleanplus/admin/checkpayment.php");
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<AdminData>>(content);
                DataAdmin = new ObservableCollection<AdminData>(Items);

                for (int i = 0; i < DataAdmin.Count; i++)
                {
                    string str = DataAdmin[i].Price.ToString("N");
                    string[] pri = str.Split(".".ToCharArray());
                    DataAdmin[i].PriceFormat = pri[0];
                }
                HomeAdmin.ItemsSource = DataAdmin;
            }
        }

        void CheckDetailBillPayment(object sender, EventArgs e)
        {
            var item = (Button)sender;
            AdminData listitem = (from itm in DataAdmin
                                  where itm.Id == (int)(item.CommandParameter)
                                   select itm).FirstOrDefault<AdminData>();

            Navigation.PushAsync(new CheckDetailBillPaymentPage(listitem));
            //await Shell.Current.GoToAsync("CheckDetailBillPaymentPage");
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

        private void Help(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("HelpPage");
        }
    }
}