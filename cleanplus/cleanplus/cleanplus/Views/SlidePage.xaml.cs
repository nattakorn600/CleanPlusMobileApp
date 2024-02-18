using cleanplus.Models;
using cleanplus.Controls;
using cleanplus.Views.Register;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SlidePage
	{
		public SlidePage()
		{
			InitializeComponent();
			//var statusbar = DependencyService.Get<IStatusBarPlatformSpecific>();
			//statusbar.SetStatusBarColor(Color.FromHex("#00000000"));
		}
	}
}