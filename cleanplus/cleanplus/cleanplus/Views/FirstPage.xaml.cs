using cleanplus.Models;
using cleanplus.Views.Register;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FirstPage : ContentPage
	{
		public ObservableCollection<BgIntro> Bgpage = new ObservableCollection<BgIntro>()
		{
			new BgIntro (){ background="bg1.png", color="#0a4c4f", image="logo.png", opacity="0.95", width="150",text1="ให้บริการตามความต้องการ", text2="ที่จะทำให้คุณสะดวกสบายมากยิ่งขึ้น"},
			new BgIntro (){ background="bg2.jpg", color="#0a4c4f", image="house2.png", opacity="1", width="120",text1="ให้บริการอย่างเป็นมืออาชีพ", text2="ด้านบริการทำความสะอาด"}
		};

		public FirstPage()
		{
			InitializeComponent();
			BackgroundIntro.ItemsSource = Bgpage;
		}
	}
}