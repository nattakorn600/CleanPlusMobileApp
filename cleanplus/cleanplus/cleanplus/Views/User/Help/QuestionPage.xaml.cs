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
	public partial class QuestionPage : ContentPage
	{
		public QuestionPage()
		{
			InitializeComponent();

			//((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.White;
		}
		void BackButtonClick(object sender, EventArgs e)
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