using cleanplus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.User.Empoyee
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmpCerPage : ContentPage
	{
		public EmpCerPage(Empoyees item)
		{
			InitializeComponent();
			CerImage.Source = item.CerImage;
		}
		void BackButtonClick(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
	}
}