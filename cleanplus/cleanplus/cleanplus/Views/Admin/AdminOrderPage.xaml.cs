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
    public partial class AdminOrderPage : ContentPage
    {
        public ObservableCollection<EmpoyeeCon> ItemList;
        public AdminOrderPage()
        {
            InitializeComponent();
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
            ReadDataAsync();
        }
        async void SendWork(object sender, EventArgs e)
        {
            var item = (Button)sender;
            EmpoyeeCon listitem = (from itm in ItemList
                                   where itm.Id == (int)(item.CommandParameter)
                                   select itm).FirstOrDefault<EmpoyeeCon>();
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("b_id",listitem.Id.ToString())
                });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/cleanplus/admin/sendwork.php?", formcontent);

                request.EnsureSuccessStatusCode();

                var response = await request.Content.ReadAsStringAsync();

                var res = JsonConvert.DeserializeObject<UserAccount>(response);

                if (res.Status != "fail")
                {
                    ItemList.Remove(listitem);
                }
            }
        }
        public async void ReadDataAsync()
        {
            var uri = new Uri(Application.Current.Properties["domain"] + "/cleanplus/admin/work.php");
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<EmpoyeeCon>>(content);
                ItemList = new ObservableCollection<EmpoyeeCon>(Items);

                for (int i = 0; i < ItemList.Count; i++)
                {
                    string str = ItemList[i].Price.ToString("N");
                    string[] pri = str.Split(".".ToCharArray());
                    ItemList[i].HomePrice = "โอนงาน " + pri[0] + "฿";
                    ItemList[i].DetailPrice = "฿ " + pri[0];
                    if (ItemList[i].Note == "")
                    {
                        ItemList[i].Note = "ไม่มีหมายเหตุ";
                    }
                }
                AdminOrder.ItemsSource = ItemList;
            }
        }

        async void OrderToDetail(object sender, EventArgs e)
        {
            var item = (Button)sender;
            EmpoyeeCon listitem = (from itm in ItemList
                                   where itm.Id == (int)(item.CommandParameter)
                                   select itm).FirstOrDefault<EmpoyeeCon>();
            Navigation.PushAsync(new AdminCheckOrderPage(listitem));
            //await Shell.Current.GoToAsync("AdminCheckOrderPage");
        }

        private void Help(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("HelpPage");
        }
    }
}