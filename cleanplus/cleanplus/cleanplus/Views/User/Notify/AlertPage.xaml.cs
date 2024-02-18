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

namespace cleanplus.Views.User.Notify
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertPage : ContentPage
    {
        ObservableCollection<ServicePayment> ItemList;
       /* public ObservableCollection<Notify> Datatxt = new ObservableCollection<Notify>()
        {
            new Notify (){ Title="พนักงานทำความสะอาด ชื่อ คุณปริชญา สาเสียง ได้ตอบรับการทำงานของคุณแล้ว"},
            new Notify (){ Title="คุณ ...... ทำการชำระเงินไม่สำเร็จ กรุณาชำระเงินอีกครั้ง"},
        };*/

        public AlertPage()
        {
            InitializeComponent();
            HideNoti.IsVisible = false;
            ReadDataAsync();
            //Carts.ItemsSource = Datatxt;
        }
        public async void ReadDataAsync()
        {
            var uri = new Uri(Application.Current.Properties["domain"] + "/cleanplus/booking/notify.php?id=" + Application.Current.Properties["user_id"]);
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<ServicePayment>>(content);
                ItemList = new ObservableCollection<ServicePayment>(Items);

                if(ItemList.Count <= 0)
				{
                    HideNoti.IsVisible = true;
                    HideNotify.IsVisible = false;
				}
                else
				{
                    HideNoti.IsVisible = false;
                    HideNotify.IsVisible = true;
                    for (int i = 0; i < ItemList.Count; i++)
                    {
                        if (ItemList[i].Status == "paymentfail")
                        {
                            ItemList[i].Title = "คุณ " + Application.Current.Properties["user_name"] + " ทำการชำระเงินไม่สำเร็จกรุณาชำระเงินอีกครั้ง";
                            ItemList[i].VisableComment = true;
                        }
                        else if (ItemList[i].Status == "empoyeeaccept")
                        {
                            string[] name = ItemList[i].Emp_Name.Split(" ".ToCharArray());
                            ItemList[i].Title = "พนักงานทำความสะอาด ชื่อ คุณ " + name[1] + " " + name[2] + " ได้ตอบรับการทำงานของคุณแล้ว";
                        }
                    }
                    Carts.ItemsSource = ItemList;
                }
            }
        }
        void NotiClick(object sender, EventArgs e)
		{
            var item = (Button)sender;
            ServicePayment listitem = (from itm in ItemList
                                       where itm.Noti_Id == (int)(item.CommandParameter)
                                       select itm).FirstOrDefault<ServicePayment>();
            Navigation.PushAsync(new CheckBillPage(listitem));
		}

        private void CancelClick(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync();
            return true;
        }
    }
}