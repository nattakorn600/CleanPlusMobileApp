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
    public partial class AdminChatPage : ContentPage
    {
        ObservableCollection<UserChat> ItemList;
        ObservableCollection<UserChat> UserList;
        public AdminChatPage()
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
            var uri = new Uri(Application.Current.Properties["domain"] + "/cleanplus/chat/showalluser.php");
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<UserChat>>(content);
                ItemList = new ObservableCollection<UserChat>(Items);
                UserList = new ObservableCollection<UserChat>();
                for (int i=0; i<ItemList.Count; i++)
				{
                    if(ItemList[i].Count > 0)
					{
                        UserList.Add(ItemList[i]);
					}
				}
                ChatAdmin.ItemsSource = UserList;
            }
        }

        async void Chat(object sender, EventArgs e)
        {
            var item = (Button)sender;
            UserChat listitem = (from itm in UserList
                                 where itm.User_Id == (int)(item.CommandParameter)
                                  select itm).FirstOrDefault<UserChat>();
            await Navigation.PushAsync(new ChatAdminPage(listitem),false);
            //await Shell.Current.GoToAsync("ChatPage");
        }

        private void Help(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("HelpPage");
        }
    }
}