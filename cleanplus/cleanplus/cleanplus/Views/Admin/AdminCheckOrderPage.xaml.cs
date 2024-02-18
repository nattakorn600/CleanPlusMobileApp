using cleanplus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminCheckOrderPage : ContentPage
    {
        EmpoyeeCon ItemList = new EmpoyeeCon();
        public AdminCheckOrderPage(EmpoyeeCon Data)
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
            VatText.Text = (Data.Price * 0.07).ToString("N");
            SumText.Text = ((Data.Price * 0.07) + Data.Price).ToString("N");
            ButtonText.Text = Data.HomePrice;
            ItemList = Data;
        }
        async void SendWork(object sender, EventArgs e)
        {
            using (var cl = new HttpClient())
            {
                var formcontent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("b_id",ItemList.Id.ToString())
                });

                var request = await cl.PostAsync(Application.Current.Properties["domain"] +
                    "/cleanplus/admin/sendwork.php?", formcontent);

                request.EnsureSuccessStatusCode();

                var response = await request.Content.ReadAsStringAsync();

                var res = JsonConvert.DeserializeObject<UserAccount>(response);

                if (res.Status != "fail")
                {
                    await Navigation.PopAsync();
                }
            }
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopAsync();
        }
    }
}