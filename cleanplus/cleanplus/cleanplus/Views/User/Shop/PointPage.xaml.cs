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
    public partial class PointPage : ContentPage
    {
        public ObservableCollection<Product> Datatxt = new ObservableCollection<Product>()
        {
            new Product (){ Image="Product1.png", Title="3 เอ็ม ผลิตภัณฑ์ทำความสะอาดพื้นและฆ่าเชื้อโรค กลิ่นโรแมนติกโรส ขนาด 3.8 ลิตร", Price="200"},
            new Product (){ Image="Product2.png", Title="3 เอ็ม ผลิตภัณฑ์ล้างห้องน้ำฆ่าเชื้อโรค กลิ่นพฤกษา ขนาด 3.8 ลิตร", Price="200"},
            new Product (){ Image="Product3.png", Title="3 เอ็ม ผลิตภัณฑ์ทำความสะอาดกระจก ขนาด 3.8 ลิตร", Price="150"},
            new Product (){ Image="Product6.png", Title="3เอ็ม ผลิตภัณฑ์ขจัดคราบไขมันฝังแน่นขนาด 600 กรัม", Price="200"},
        };
        public PointPage()
        {
            InitializeComponent();
            Point.ItemsSource = Datatxt;
        }

        void BackButtonClick(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopAsync();
        }
    }
}