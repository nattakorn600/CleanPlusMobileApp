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

namespace cleanplus.Views.User
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookingPage : ContentPage
	{
		public ObservableCollection<ServicePayment> AllList;
		public ObservableCollection<ServicePayment> BookList = new ObservableCollection<ServicePayment>();
		public ObservableCollection<ServicePayment> FinishList = new ObservableCollection<ServicePayment>();
		private int pixels = 50;
		private int pagebutton = 0;
		public BookingPage()
		{
			InitializeComponent();
			BookPage.TranslationX = 0;
			FinishPage.TranslationX = Application.Current.MainPage.Width;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			ReadDataAsync();
		}
		async void OnSelect(object sender, SelectionChangedEventArgs e)
		{
			var selectproduct = (e.CurrentSelection.FirstOrDefault() as ServicePayment);
			//Products.SelectedItems.Clear(); 
			//DisplayAlert("test", selectproduct.id, "OK");
			//await Navigation.PushAsync(new ProductDetail(selectproduct));
		}
		async void SendComment(object sender, EventArgs e)
		{
			var item = (Button)sender;
			ServicePayment listitem = (from itm in FinishList
									   where itm.Id == (int)(item.CommandParameter)
									   select itm).FirstOrDefault<ServicePayment>();

			string json = JsonConvert.SerializeObject(listitem.Emp, Formatting.Indented);

			using (var cl = new HttpClient())
			{
				var formcontent = new FormUrlEncodedContent(new[]
				{
						new KeyValuePair<string,string>("star",listitem.CountStar),
						new KeyValuePair<string, string>("comment",listitem.Comment),
						new KeyValuePair<string, string>("b_id",listitem.Id.ToString()),
						new KeyValuePair<string, string>("c_id",json)
					});

				var request = await cl.PostAsync(Application.Current.Properties["domain"] +
					"/cleanplus/booking/addreview.php?", formcontent);

				request.EnsureSuccessStatusCode();

				var response = await request.Content.ReadAsStringAsync();

				var res = JsonConvert.DeserializeObject<UserAccount>(response);
				if(res.Status == "success")
				{
					listitem.VisableComment = false;
					for (int i = 0; i < FinishList.Count; i++)
					{
						if (FinishList[i].Id == listitem.Id)
						{
							FinishList[i] = listitem;
						}
					}
				}
			}
		}
	
		public async void ReadDataAsync()
		{
			var uri = new Uri(Application.Current.Properties["domain"] + "/cleanplus/booking/getbooking.php?user_id=" + Application.Current.Properties["user_id"]);
			HttpClient myClient = new HttpClient();

			var response = await myClient.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var Items = JsonConvert.DeserializeObject<List<ServicePayment>>(content);
				AllList = new ObservableCollection<ServicePayment>(Items);
				FinishList.Clear();
				BookList.Clear();
				for (int i = 0; i < AllList.Count; i++)
				{
					if (AllList[i].Finish == 2)
					{
						string[] timelist = AllList[i].Time.Split("-".ToCharArray());
						AllList[i].Time = "เวลา " + timelist[1];
						AllList[i].Star1 = "star.png";
						AllList[i].Star2 = "star.png";
						AllList[i].Star3 = "star.png";
						AllList[i].Star4 = "star.png";
						AllList[i].Star5 = "star.png";
						AllList[i].VisableComment = false;

						if (AllList[i].CountStar == null && AllList[i].Comment == null)
						{
							AllList[i].VisableComment = true;
						}

						if (AllList[i].CountStar == null)
						{
							AllList[i].CountStar = "0";
						}
		
						if(int.Parse(AllList[i].CountStar) > 0)
						{
							StarManage(AllList[i], int.Parse(AllList[i].CountStar));
						}
						
						FinishList.Add(AllList[i]);
					}
					else
					{
						BookList.Add(AllList[i]);
					}
				}
				if (BookList.Count <= 0)
				{
					HideBooking.IsVisible = false;
					HideNotiBookPage.IsVisible = true;
				}
				else
				{
					HideBooking.IsVisible = true;
					HideNotiBookPage.IsVisible = false;
					BookColection.ItemsSource = BookList;
				}
				if (FinishList.Count <= 0)
				{
					HideFinish.IsVisible = false;
					HideNotiFinishPage.IsVisible = true;
				}
				else
				{
					HideFinish.IsVisible = true;
					HideNotiFinishPage.IsVisible = false;
					FinishShowList.ItemsSource = FinishList;
				}
			}
		} 
		void OnBooking(object sender, EventArgs e)
		{
			if (pagebutton == 1)
			{
				BookPage.TranslationX = -Application.Current.MainPage.Width;
				FinishPage.TranslationX = 0;
				Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
				{
					if (FinishPage.TranslationX <= Application.Current.MainPage.Width - pixels)
					{
						FinishPage.TranslationX += pixels;
						BookPage.TranslationX += pixels;
						return true;
					}
					else
					{
						FinishPage.TranslationX = Application.Current.MainPage.Width;
						BookPage.TranslationX = 0;
						return false;
					}
				});

				BookingIndtcator.IsVisible = true;
				FinishIndicator.IsVisible = false;
				pagebutton = 0;
			}
		}
		void OnFinish(object sender, EventArgs e)
		{
			if (pagebutton == 0)
			{
				FinishPage.TranslationX = Application.Current.MainPage.Width;
				BookPage.TranslationX = 0;
				Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
				{
					if (FinishPage.TranslationX >= pixels)
					{
						FinishPage.TranslationX -= pixels;
						BookPage.TranslationX -= pixels;
						return true;
					}
					else
					{
						FinishPage.TranslationX = 0;
						BookPage.TranslationX = -Application.Current.MainPage.Width;
						return false;
					}
				});

				BookingIndtcator.IsVisible = false;
				FinishIndicator.IsVisible = true;
				pagebutton = 1;
			}
		}
		private void StarManage(ServicePayment listitem, int number)
		{
			if(number >= 1)
			{
				listitem.Star1 = "stared.png";
				listitem.CountStar = "1";
			}
			else
			{
				listitem.Star1 = "star.png";
			}
			if(number >= 2)
			{
				listitem.Star2 = "stared.png";
				listitem.CountStar = "2";
			}
			else
			{
				listitem.Star2 = "star.png";
			}
			if (number >= 3)
			{
				listitem.Star3 = "stared.png";
				listitem.CountStar = "3";
			}
			else
			{
				listitem.Star3 = "star.png";
			}
			if (number >= 4)
			{
				listitem.Star4 = "stared.png";
				listitem.CountStar = "4";
			}
			else
			{
				listitem.Star4 = "star.png";
			}
			if (number >= 5)
			{
				listitem.Star5 = "stared.png";
				listitem.CountStar = "5";
			}
			else
			{
				listitem.Star5 = "star.png";
			}

			for (int i = 0; i < FinishList.Count; i++)
			{
				if (FinishList[i].Id == listitem.Id)
				{
					FinishList[i] = listitem;
				}
			}
		}
		private void Click_Star1(object sender, EventArgs e)
		{
			var item = (ImageButton)sender;
			ServicePayment listitem = (from itm in FinishList
									   where itm.Id == (int)(item.CommandParameter)
									   select itm).FirstOrDefault<ServicePayment>();
			StarManage(listitem, 1);
		}

		private void Click_Star2(object sender, EventArgs e)
		{
			var item = (ImageButton)sender;
			ServicePayment listitem = (from itm in FinishList
									   where itm.Id == (int)(item.CommandParameter)
									   select itm).FirstOrDefault<ServicePayment>();
			StarManage(listitem, 2);
		}

		private void Click_Star3(object sender, EventArgs e)
		{
			var item = (ImageButton)sender;
			ServicePayment listitem = (from itm in FinishList
									   where itm.Id == (int)(item.CommandParameter)
									   select itm).FirstOrDefault<ServicePayment>();
			StarManage(listitem, 3);
		}

		private void Click_Star4(object sender, EventArgs e)
		{
			var item = (ImageButton)sender;
			ServicePayment listitem = (from itm in FinishList
									   where itm.Id == (int)(item.CommandParameter)
									   select itm).FirstOrDefault<ServicePayment>();
			StarManage(listitem, 4);
		}

		private void Click_Star5(object sender, EventArgs e)
		{
			var item = (ImageButton)sender;
			ServicePayment listitem = (from itm in FinishList
									   where itm.Id == (int)(item.CommandParameter)
									   select itm).FirstOrDefault<ServicePayment>();
			StarManage(listitem, 5);
		}
	}
}