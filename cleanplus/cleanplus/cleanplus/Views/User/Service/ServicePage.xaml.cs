using cleanplus.Controls;
using cleanplus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.User.Service
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ServicePage : ContentPage
	{
		ServicePayment servicePay = new ServicePayment();
		Geocoder geoCoder;
		Position PinPosition;
		List<DateTimeCustom> dateFormat = new List<DateTimeCustom>()
		{
			new DateTimeCustom{ MontNumber=1, MontReviation="ม.ค"},
			new DateTimeCustom{ MontNumber=2, MontReviation="ก.พ."},
			new DateTimeCustom{ MontNumber=3, MontReviation="มี.ค."},
			new DateTimeCustom{ MontNumber=4, MontReviation="เม.ย."},
			new DateTimeCustom{ MontNumber=5, MontReviation="พ.ค."},
			new DateTimeCustom{ MontNumber=6, MontReviation="มิ.ย."},
			new DateTimeCustom{ MontNumber=7, MontReviation="ก.ค."},
			new DateTimeCustom{ MontNumber=8, MontReviation="ส.ค."},
			new DateTimeCustom{ MontNumber=9, MontReviation="ก.ย."},
			new DateTimeCustom{ MontNumber=10, MontReviation="ต.ค."},
			new DateTimeCustom{ MontNumber=11, MontReviation="พ.ย."},
			new DateTimeCustom{ MontNumber=12, MontReviation="ธ.ค."}
		};
		List<ServiceRate> servicerate = new List<ServiceRate>()
		{
			new ServiceRate{ Min=20, Max=50, Hour=2, Empoyee=1},
			new ServiceRate{ Min=51, Max=80, Hour=3, Empoyee=1},
			new ServiceRate{ Min=81, Max=120, Hour=4, Empoyee=2},
			new ServiceRate{ Min=121, Max=150, Hour=5, Empoyee=2},
			new ServiceRate{ Min=151, Max=200, Hour=6, Empoyee=3},
			new ServiceRate{ Min=201, Max=250, Hour=7, Empoyee=3}
		};
		public ServicePage()
		{
			InitializeComponent();
			textTime.Time = DateTime.Now.TimeOfDay;
			geoCoder = new Geocoder();
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(13.7543822883747, 100.500995479524), Distance.FromMiles(450)));
			ChangeDateFormat();
			PositionUser();
		}

		async void OnCheckRadioMap(object sender, EventArgs e)
		{
			map.Pins.Clear();
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(PinPosition.Latitude, PinPosition.Longitude), Distance.FromMeters(200)));
			IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(PinPosition);
			string address = possibleAddresses.FirstOrDefault();

			Pin pin = new Pin
			{
				Label = "Your location",
				Address = address,
				Type = PinType.Place,
				Position = new Position(PinPosition.Latitude, PinPosition.Longitude)
			};
			map.Pins.Add(pin);
			servicePay.Address = address;
			servicePay.Latitude = PinPosition.Latitude;
			servicePay.Longitude = PinPosition.Longitude;
		}
		async void PositionUser()
		{
			try
			{
				var request = new GeolocationRequest(GeolocationAccuracy.Best);
				var position = await Geolocation.GetLocationAsync(request);
				PinPosition = new Position(position.Latitude, position.Longitude);
				IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(PinPosition);
				string address = possibleAddresses.FirstOrDefault();
				string[] addres = address.Split(",".ToCharArray());
				string[] addres1 = addres[addres.Length - 1].Split(" ".ToCharArray());
				string adrs = "";
				try
				{
					adrs = int.Parse(addres1[addres1.Length - 2]).ToString();
					adrs = addres1[addres1.Length - 3];
				}
				catch
				{
					adrs = addres1[addres1.Length - 2];
				}
				Proven.Text = adrs;
				map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMeters(200)));
			}
			catch
			{
				await DisplayAlert("ข้อผิดพลาด!!", "กรุณาเปิดการเข้าถึงตำแหน่งของคุณ", "ตกลง");
				await Navigation.PopAsync();
			}
		}
		void calHour(object sender, EventArgs e)
		{
			int calbath = 0;
			if (squareMeter.Text != null && squareMeter.Text != "")
			{
				for (int i = 0; i < servicerate.Count; i++)
				{
					if (int.Parse(squareMeter.Text) >= servicerate[i].Min && int.Parse(squareMeter.Text) <= servicerate[i].Max)
					{
						calbath = int.Parse(squareMeter.Text) * 20;

						textBath.Text = calbath.ToString("N");
						textHour.Text = servicerate[i].Hour.ToString();
						textPerson.Text = servicerate[i].Empoyee.ToString();
						textShowCal.Text = squareMeter.Text + " ตร.ม.X ฿20";
						textSum.Text = calbath.ToString("N");

						servicePay.Price = calbath;
						servicePay.Empoyee = servicerate[i].Empoyee;
						servicePay.Hour = servicerate[i].Hour;
						servicePay.TarangMate = squareMeter.Text;
					}
				}
			}
			if (calbath == 0)
			{
				textBath.Text = "0";
				textHour.Text = "0";
				textPerson.Text = "0";
				textShowCal.Text = "0" + " ตร.ม.X ฿20";
				textSum.Text = "0";
				servicePay.Price = 0;
				servicePay.Empoyee = 0;
				servicePay.Hour = 0;
				servicePay.TarangMate = "0";
				DisplayAlert("การคำนวณผิดพลาด!!", "จำกัดขนาดความกว้างอย่างต่ำ 20 ตร.ม และไม่เกิน 250 ตร.ม", "ยืนยัน");
			}
		}

		async void OnMapClicked(object sender, MapClickedEventArgs e)
		{
			if (btnRadio.IsChecked == false)
			{
				map.Pins.Clear();
				Position PinPuk = new Position(e.Position.Latitude, e.Position.Longitude);
				IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(PinPuk);
				string address = possibleAddresses.FirstOrDefault();
				
				Pin pin = new Pin
				{
					Label = "Your location",
					Address = address,
					Type = PinType.Place,
					Position = new Position(PinPuk.Latitude, PinPuk.Longitude)
				};
				map.Pins.Add(pin);
				servicePay.Address = address;
				servicePay.Latitude = PinPuk.Latitude;
				servicePay.Longitude = PinPuk.Longitude;
				//Launcher.OpenAsync("geo:15.8700,100.9925");
			}
		}
		void BackButtonClick(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
		void ChangeDateFormat()
		{
			string[] str = DatePick.Date.ToString().Split(" ".ToCharArray());
			string[] str1 = str[0].Split("/".ToCharArray());

			for (int i = 0; i < dateFormat.Count; i++)
			{
				if (int.Parse(str1[1]) == dateFormat[i].MontNumber)
				{
					DatePickText.Text = str1[0] + " " + dateFormat[i].MontReviation + " " + str1[2];
				}
			}
		}
		void OnDateSelect(object sender, EventArgs e)
		{
			ChangeDateFormat();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			//MessagingCenter.Unsubscribe<ServicePage, TransitionType>(this, TransitionSetting.TransitionMessage);
		}
		/*void ChangePage(Page next, TransitionType animation)
		{
			MessagingCenter.Subscribe<ServicePage, TransitionType>(this, TransitionSetting.TransitionMessage, (s, arg) =>
			{
				var transitionType = (TransitionType)arg;
				var transitionNavigationPage = Parent as Controls.TransitionNavigationPage;

				if (transitionNavigationPage != null)
				{
					transitionNavigationPage.TransitionType = transitionType;
					Navigation.PushAsync(next);
				}
			});

			MessagingCenter.Send(this, TransitionSetting.TransitionMessage, animation);
		}*/
		void OnNextPage(object sender, EventArgs e)
		{
			string[] time = textTime.Time.ToString().Split(":".ToCharArray());
			servicePay.Date = DatePickText.Text;
			servicePay.Time = time[0] + "." + time[1];
			servicePay.Note = NoteNa.Text;
			if (servicePay.Hour == 0 || servicePay.Address == "" || servicePay.Address == null)
			{
				DisplayAlert("กรอกข้อมูลไม่ครบ!!", "กรุณากรอกข้อมูลให้ครบก่อนไปหน้าถัดไปค่ะ", "ยืนยัน");
			}
			else
			{
				//ChangePage(new CheckBillPagexaml(servicePay), TransitionType.SlideFromRight);
				Navigation.PushAsync(new CheckBillPagexaml(servicePay));
			}
		}
	}
}