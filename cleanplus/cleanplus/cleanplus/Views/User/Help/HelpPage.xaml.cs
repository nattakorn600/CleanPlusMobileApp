using cleanplus.Controls;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.User.Help
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HelpPage : ContentPage
	{
		public HelpPage()
		{
			InitializeComponent();
			
			var statusbar = DependencyService.Get<IStatusBarPlatformSpecific>();
			statusbar.SetStatusBarColor(Color.FromHex("#FFF"));
		}

		async void OnCancelClick(object sender, EventArgs e)
		{
			//Application.Current.MainPage = new AppShell();
			Shell.Current.Navigation.PopAsync();
		}

		
		void ToChat(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ChatPage());
		}
		void ToQuestion(object sender, EventArgs e)
		{
			Navigation.PushAsync(new QuestionPage());
		}
		void ToPrivate(object sender, EventArgs e)
		{
			Navigation.PushAsync(new PrivatePage());
		}
		void ToAgree(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AgreementPage());
		}

		private void ToRule(object sender, EventArgs e)
		{
			Navigation.PushAsync(new StaffRulePage());
		}

		protected override bool OnBackButtonPressed()
		{
			Navigation.PopAsync(false);
			//Application.Current.MainPage = new AppShell();
			return true;
		}
    }
}