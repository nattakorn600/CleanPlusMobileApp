using cleanplus.Controls;
using cleanplus.Views.User.Help;
using cleanplus.Views.User.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppShell : CustomShell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute("PointPage", typeof(PointPage));
			Routing.RegisterRoute("ShopPage", typeof(ShopPage));
			Routing.RegisterRoute("CartPage", typeof(CartPage));
			Routing.RegisterRoute("PaymentPage", typeof(PaymentPage));
			Routing.RegisterRoute("AgreementPage", typeof(AgreementPage));
			Routing.RegisterRoute("ChatPage", typeof(ChatPage));
			Routing.RegisterRoute("HelpPage", typeof(HelpPage));
			Routing.RegisterRoute("PrivatePage", typeof(PrivatePage));
			Routing.RegisterRoute("QuestionPage", typeof(QuestionPage));
			Routing.RegisterRoute("StaffRulePage", typeof(StaffRulePage));

			var statusbar = DependencyService.Get<IStatusBarPlatformSpecific>();
			statusbar.SetStatusBarColor(Color.FromHex("#178da4"));
		}
	}
}