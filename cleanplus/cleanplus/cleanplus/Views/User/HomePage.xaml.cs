using cleanplus.Models;
using cleanplus.Controls;
using cleanplus.Views.User.Help;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using cleanplus.Views.User.Shop;
using cleanplus.Views.User.Service;
using cleanplus.Views.User.Empoyee;
using cleanplus.Views.User.Notify;

namespace cleanplus.Views.User
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public ObservableCollection<BgIntro> advert = new ObservableCollection<BgIntro>()
		{
			new BgIntro(){ image="page1.png" },
			new BgIntro(){ image="page2.png" }
		};
		public HomePage()
		{
			InitializeComponent();
			Advert.ItemsSource = advert;
		}
		void OnAlertClick(object sender, EventArgs e)
		{
			//Application.Current.MainPage = new TransitionNavigationPage(new AlertPage()) { BarBackgroundColor = Color.FromHex("#FFF") };
			Navigation.PushAsync(new AlertPage());
		}
		void EmpoyeeClick(object sender, EventArgs e)
		{
			Navigation.PushAsync(new EmpoyeePage());
			//Application.Current.MainPage = new TransitionNavigationPage(new EmpoyeePage()) { BarBackgroundColor = Color.FromHex("#FFF") };
		}

		void ServiceClick(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ServicePage());
			//Application.Current.MainPage = new TransitionNavigationPage(new ServicePage()) { BarBackgroundColor = Color.FromHex("#FFF") };
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			var statusbar = DependencyService.Get<IStatusBarPlatformSpecific>();
			statusbar.SetStatusBarColor(Color.FromHex("#178da4"));
		}

		void OnHelpClick(object sender, EventArgs e)
		{
			Navigation.PushAsync(new HelpPage());
			//Application.Current.MainPage = new TransitionNavigationPage(new HelpPage()) { BarBackgroundColor=Color.FromHex("#FFF")};
		}


		async void GoToShop(object sender, EventArgs e)
		{
			//await Shell.Current.GoToAsync("ShopPage");
			await Navigation.PushAsync(new ShopPage());
		}
	}
}