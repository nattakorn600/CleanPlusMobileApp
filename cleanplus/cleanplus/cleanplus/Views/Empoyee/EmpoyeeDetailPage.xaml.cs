using cleanplus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.Empoyee
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmpoyeeDetailPage : ContentPage
    {
        EmpoyeeCon ItemList = new EmpoyeeCon();
        public EmpoyeeDetailPage(EmpoyeeCon Data)
        {
            InitializeComponent();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Data.Latitude, Data.Longitude), Distance.FromMiles(0.1)));
            Pin pin = new Pin
            {
                Label = Data.User.Name,
                Address = Data.Address,
                Type = PinType.Place,
                Position = new Position(Data.Latitude, Data.Longitude)
            };
            map.Pins.Add(pin);
            BindingContext = Data;
            ItemList = Data;
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            //Shell.Current.Navigation.PopAsync();
        }

        private void OnCheckRadioMap(object sender, CheckedChangedEventArgs e)
        {

        }

        private void OnMapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {

        }

        async void Confirm(object sender, EventArgs e)
        {
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string,string>("id",Application.Current.Properties["user_id"].ToString()),
                        new KeyValuePair<string, string>("b_id",ItemList.Id.ToString())
                    });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/cleanplus/empoyee/getwork.php?", formcontent);

                request.EnsureSuccessStatusCode();

                var response = await request.Content.ReadAsStringAsync();

                var res = JsonConvert.DeserializeObject<UserAccount>(response);

                if (res.Status != "fail")
                {
                    await Navigation.PopAsync();
                    //Shell.Current.GoToAsync("///EmpoyeeHomePage");
                }
            }
        }
    }
}