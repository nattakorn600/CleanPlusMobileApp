using cleanplus.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.User.Shop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public ObservableCollection<Product> SelectItem = new ObservableCollection<Product>();
        int SumPrice = 0;
        public CartPage(ObservableCollection<Product> Data)
        {
            InitializeComponent();
            SelectItem = Data;
            for(int i=0; i<SelectItem.Count; i++)
			{
                SumPrice += int.Parse(SelectItem[i].Price);
			}
            SumPriceButton.Text = "ซื้อสินค้า " + SumPrice + " ฿";
            Cart.ItemsSource = SelectItem;
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            //Shell.Current.Navigation.PopAsync();
            Navigation.PopAsync();
        }

        void BuyProduct(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("PaymentPage");
            Navigation.PushAsync(new PaymentPage(SelectItem));
        }
    }
}