using cleanplus.Controls;
using cleanplus.Views.Empoyee;
using cleanplus.Views.Register;
using cleanplus.Views.User;
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
	public partial class EmpoyeeShell : CustomShell
	{
		public EmpoyeeShell()
		{
			InitializeComponent();
			Routing.RegisterRoute("EmpoyeeDetailPage", typeof(EmpoyeeDetailPage));
			Routing.RegisterRoute("EmpoyeeAlertPage", typeof(EmpoyeeAlertPage));
			Routing.RegisterRoute("HelpPage", typeof(HelpPage));
			Routing.RegisterRoute("LoginPage", typeof(LoginPage));
		}

        private void ToDetail(object sender, EventArgs e)
        {

        }
    }
}