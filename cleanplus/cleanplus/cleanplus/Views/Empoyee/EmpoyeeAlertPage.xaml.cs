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

namespace cleanplus.Views.Empoyee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmpoyeeAlertPage : ContentPage
    {
        ObservableCollection<ServicePayment> ItemList = new ObservableCollection<ServicePayment>();
        public EmpoyeeAlertPage()
        {
            InitializeComponent();
            ReadDataAsync();
        }
        public async void ReadDataAsync()
        {
            var uri = new Uri(Application.Current.Properties["domain"] + "/cleanplus/empoyee/notify.php?id=" + Application.Current.Properties["user_id"]);
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
                    EmpoyeeAlert.IsVisible = false;
                }
                else
				{
                    EmpoyeeAlert.IsVisible = true;
                    HideNoti.IsVisible = false;
                    EmpoyeeAlert.ItemsSource = ItemList;
                }
            }
        }
        private void CancelClick(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopAsync();
        }
    }
}