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

namespace cleanplus.Views.Empoyee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmpoyeeHomePage : ContentPage
    {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "session.json");

        ObservableCollection<EmpoyeeCon> ItemList = new ObservableCollection<EmpoyeeCon>();
        public EmpoyeeHomePage()
        {
            InitializeComponent();
            string nme = Application.Current.Properties["emp_name"].ToString();
            string[] nmearr = nme.Split(" ".ToCharArray());
            HeadName.Text = "คุณ " + nmearr[1] + " " + nmearr[2];
        }
		protected override void OnAppearing()
		{
			base.OnAppearing();
            ReadDataAsync();
        }

		async void GetWork(object sender, EventArgs e)
		{
            var item = (Button)sender;
            EmpoyeeCon listitem = (from itm in ItemList
                                   where itm.Id == (int)(item.CommandParameter)
                                   select itm).FirstOrDefault<EmpoyeeCon>();

            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string,string>("id",Application.Current.Properties["user_id"].ToString()),
                        new KeyValuePair<string, string>("b_id",listitem.Id.ToString())
                    });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/cleanplus/empoyee/getwork.php?", formcontent);

                request.EnsureSuccessStatusCode();

                var response = await request.Content.ReadAsStringAsync();

                var res = JsonConvert.DeserializeObject<UserAccount>(response);

                if (res.Status != "fail")
                {
                    listitem.VisableWork = false;
                    for (int i = 0; i < ItemList.Count; i++)
                    {
                        if (ItemList[i].Id == listitem.Id)
                        {
                            ItemList[i] = listitem;

                        }
                    }
                }
            }
        }

        async void WaitWork(object sender, EventArgs e)
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
                    "/cleanplus/empoyee/finish.php?", formcontent);

                request.EnsureSuccessStatusCode();

                var response = await request.Content.ReadAsStringAsync();

                var res = JsonConvert.DeserializeObject<UserAccount>(response);

                if (res.Status != "fail")
                {
                    listitem.VisableWait = false;
                    for (int i = 0; i < ItemList.Count; i++)
                    {
                        if (ItemList[i].Id == listitem.Id)
                        {
                            ItemList[i] = listitem;
                        }
                    }
                }
            }
        }

        async void SuccessWork(object sender, EventArgs e)
        {
           
        }

        public async void ReadDataAsync()
        {
            var uri = new Uri(Application.Current.Properties["domain"] + "/cleanplus/empoyee/home.php?id=" + Application.Current.Properties["user_id"]);
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<EmpoyeeCon>>(content);
                ItemList = new ObservableCollection<EmpoyeeCon>(Items);

                for(int i=0; i<ItemList.Count; i++)
				{
                    string str = (ItemList[i].Price * 0.4 / ItemList[i].Empoyee).ToString("N");
                    string[] pri = str.Split(".".ToCharArray());
                    ItemList[i].HomePrice = "รับงาน " + pri[0] + "฿";
                    ItemList[i].DetailPrice = "฿ " + pri[0];

                    if (ItemList[i].Finish == 0)
					{
                        ItemList[i].VisableWork = true;
                    }
                    else if (ItemList[i].Finish == 1)
					{
                        ItemList[i].VisableWork = false;
                        ItemList[i].VisableWait = true;
                    }else
					{
                        ItemList[i].VisableWait = false;
                    }

                    if (ItemList[i].Note == "")
					{
                        ItemList[i].Note = "ไม่มีหมายเหตุ";
                    }
                }
                Empoyee.ItemsSource = ItemList;
            }
        }

        private void ToDetail(object sender, EventArgs e)
        {
            var item = (Button)sender;
            EmpoyeeCon listitem = (from itm in ItemList
                                       where itm.Id == (int)(item.CommandParameter)
                                       select itm).FirstOrDefault<EmpoyeeCon>();
            Navigation.PushAsync(new EmpoyeeDetailPage(listitem));
            //Shell.Current.GoToAsync("EmpoyeeDetailPage");
        }

        private void Alert(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("EmpoyeeAlertPage");
        }

        private void Help(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("HelpPage");
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
    }
}