using cleanplus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.User.Empoyee
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmpoyeePage : ContentPage
	{
		ObservableCollection<Empoyees> emp = new ObservableCollection<Empoyees>();
		/*ObservableCollection<Empoyees> emp = new ObservableCollection<Empoyees>() {
			new Empoyees{ Id=1, Name="นางสาว ปาวีณา ยีภู่", Position="หัวหน้าพนักงาน", Star=5, CommentIsVisible=false, Image="https://mahamhorr.com/cleanplus/images/empoyee/user1.png", Rotate=0, CerImage="cer3.png"},
			new Empoyees{ Id=2, Name="นางสาว ปริชญา สาเสียง", Position="พนักงาน", Star=5, CommentIsVisible=false, Image="https://mahamhorr.com/cleanplus/images/empoyee/user2.png", Rotate=0, CerImage="cer1.png"},
			new Empoyees{ Id=3, Name="นางสาว วรรณาพร โสมาสา", Position="พนักงาน", Star=5, CommentIsVisible=false, Image="https://mahamhorr.com/cleanplus/images/empoyee/user3.png", Rotate=0, CerImage="cer2.png"},
		};*/
		public EmpoyeePage()
		{
			InitializeComponent();
			ReadDataAsync();
		}
		public async void ReadDataAsync()
		{
			var uri = new Uri(Application.Current.Properties["domain"] + "/cleanplus/empoyee/empoyee.php");
			HttpClient myClient = new HttpClient();

			var response = await myClient.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var Items = JsonConvert.DeserializeObject<List<Empoyees>>(content);
				emp = new ObservableCollection<Empoyees>(Items);
				EmpoList.ItemsSource = emp;
			}
		}
		void BackButtonClick(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
		void ClickMore(object sender, EventArgs e)
		{
			var item = (Button)sender;
			Empoyees listitem = (from itm in emp
								 where itm.Id == (int)(item.CommandParameter)
								 select itm).FirstOrDefault<Empoyees>();
			Navigation.PushAsync(new EmpCerPage(listitem));
		}
		void dropdownClicked(object sender, EventArgs e)
		{
			var item = (ImageButton)sender;
			Empoyees listitem = (from itm in emp
					 where itm.Id == (int)(item.CommandParameter)
					 select itm).FirstOrDefault<Empoyees>();
			//emp.Remove(listitem);
			for(int i=0; i<emp.Count; i++)
			{
				if(emp[i].Id == listitem.Id)
				{
					if(listitem.CommentIsVisible == true)
					{
						listitem.Rotate = 0;
						listitem.CommentIsVisible = false;
					}
					else
					{
						listitem.Rotate = 180;
						listitem.CommentIsVisible = true;
					}
					emp[i] = listitem;
				}
			}
		}
		protected override bool OnBackButtonPressed()
		{
			Navigation.PopAsync();
			return true;
		}
	}
}