using cleanplus.Controls;
using cleanplus.Views.Admin;
using cleanplus.Views.Register;
using cleanplus.Views.User.Help;
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
	public partial class AdminShell : CustomShell
	{
		public AdminShell()
		{
			InitializeComponent();

			Routing.RegisterRoute("CheckDetailBillPaymentPage", typeof(CheckDetailBillPaymentPage));
			Routing.RegisterRoute("ChatPage", typeof(ChatPage));
			Routing.RegisterRoute("AdminCheckOrderPage", typeof(AdminCheckOrderPage));
			Routing.RegisterRoute("HelpPage", typeof(HelpPage));
			Routing.RegisterRoute("LoginPage", typeof(LoginPage));
		}
	}
}