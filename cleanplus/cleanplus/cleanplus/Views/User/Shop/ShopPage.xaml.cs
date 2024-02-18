using cleanplus.Controls;
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
    public partial class ShopPage : ContentPage
    {
        public ObservableCollection<Product> SelectItem = new ObservableCollection<Product>();
        public ObservableCollection<Product> Data = new ObservableCollection<Product>()
        {
            new Product (){ Id=1, Image="Product1.png", Title="3 เอ็ม ผลิตภัณฑ์ทำความสะอาดพื้นและฆ่าเชื้อโรค กลิ่นโรแมนติกโรส ขนาด 3.8 ลิตร", Price="219"},
            new Product (){ Id=2, Image="Product2.png", Title="3 เอ็ม ผลิตภัณฑ์ล้างห้องน้ำฆ่าเชื้อโรค กลิ่นพฤกษา ขนาด 3.8 ลิตร", Price="211"},
            new Product (){ Id=3, Image="Product3.png", Title="3 เอ็ม ผลิตภัณฑ์ทำความสะอาดกระจก ขนาด 3.8 ลิตร", Price="143"},
            new Product (){ Id=4, Image="Product4.png", Title="3เอ็ม ผลิตภัณฑ์ดันฝุ่นขนาด 3.8 ลิตร", Price="531"},
            new Product (){ Id=5, Image="Product5.png", Title="3เอ็ม ผลิตภัณฑ์ทำความสะอาดพื้น (รุ่นฉลากเขียว)  ขนาด 3.8 ลิตร", Price="246"},
            new Product (){ Id=6, Image="Product6.png", Title="3เอ็ม ผลิตภัณฑ์ขจัดคราบไขมันฝังแน่นขนาด 600 กรัม", Price="246"},
        };

        public ShopPage()
        {
            InitializeComponent();
            CountItem.IsVisible = false;
            Shop.ItemsSource = Data;
        }
        void SelectItemClick(object sender, EventArgs e)
		{
            var item = (Button)sender;
            Product listitem = (from itm in Data
                                       where itm.Id == (int)(item.CommandParameter)
                                       select itm).FirstOrDefault<Product>();
            SelectItem.Add(listitem);
            CountItem.IsVisible = true;
            CountItemNumber.Text = SelectItem.Count.ToString();
        }
        void BackButton(object sender, EventArgs e)
        {
            //Shell.Current.Navigation.PopAsync();
            Navigation.PopAsync();
        }

        async void ShoppingCart(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("CartPage");
            await Navigation.PushAsync(new CartPage(SelectItem));
        }

        private void SearchBut(object sender, EventArgs e)
        {
            Shop.ItemsSource = Data.Where(name => name.Title.ToLower().Contains(SearchText.Text.ToLower()));
        }
    }
}