using Acr.UserDialogs;
using cleanplus.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.User.Shop
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : ContentPage
    {
		public ObservableCollection<Product> SelectItem = new ObservableCollection<Product>();
		int SumPrice = 0;

		private MediaFile _mediafile;
		private bool OnPromptDrop = false;
		private bool OnBankDrop = false;
		public PaymentPage(ObservableCollection<Product> Data)
        {
            InitializeComponent();

			Name.Text = Application.Current.Properties["user_name"].ToString();
			Phone.Text = Application.Current.Properties["user_phone"].ToString();
			Address.Text = Application.Current.Properties["user_address"].ToString();

			SelectItem = Data;
			for (int i = 0; i < SelectItem.Count; i++)
			{
				SumPrice += int.Parse(SelectItem[i].Price);
			}
			SumPrice += 50;
			SumPriceButton.Text = "ซื้อสินค้า " + SumPrice + " ฿";
			Bill.ItemsSource = SelectItem;
        }
		void UploadClick(object sender, EventArgs e)
		{
			Navigation.PopToRootAsync();
		}

		void BackButtonClick(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PopAsync();
        }

		void KrungCopy(object sender, EventArgs e)
		{
			string CopyMessage = "857-0-61242-7";
			Clipboard.SetTextAsync(CopyMessage);
			ToastConfig.DefaultPosition = ToastPosition.Top;
			UserDialogs.Instance.Toast("Copy : " + CopyMessage);
		}
		void KasiCopy(object sender, EventArgs e)
		{
			string CopyMessage = "044-1-90076-7";
			Clipboard.SetTextAsync(CopyMessage);
			ToastConfig.DefaultPosition = ToastPosition.Top;
			UserDialogs.Instance.Toast("Copy : " + CopyMessage);
		}
		void PromptCopy(object sender, EventArgs e)
		{
			string CopyMessage = "0895043640";
			Clipboard.SetTextAsync(CopyMessage);
			ToastConfig.DefaultPosition = ToastPosition.Top;
			UserDialogs.Instance.Toast("Copy : " + CopyMessage);
		}
		void BankArrowClick(object sender, EventArgs e)
		{
			Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
			{
				if (OnBankDrop == false)
				{
					BankNumber.IsVisible = true;
					if (BankArrow.Rotation <= 165)
					{
						BankArrow.Rotation += 15;
						if (BankNumber.HeightRequest < 184)
						{
							BankNumber.HeightRequest += 16;
						}
						else
						{
							BankNumber.HeightRequest = 190;
						}
						return true;
					}
					else
					{
						BankArrow.Rotation = 180;
						OnBankDrop = true;
						return false;
					}
				}
				else
				{

					if (BankArrow.Rotation <= 345)
					{
						BankArrow.Rotation += 15;
						if (BankNumber.HeightRequest > 16)
						{
							BankNumber.HeightRequest -= 16;
						}
						else
						{
							BankNumber.HeightRequest = 0;
							BankNumber.IsVisible = false;
						}
						return true;
					}
					else
					{
						BankArrow.Rotation = 0;
						OnBankDrop = false;
						return false;
					}
				}
			});
		}

		void PromptArrowClick(object sender, EventArgs e)
		{
			Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
			{
				if (OnPromptDrop == false)
				{
					PromptNumber.IsVisible = true;
					if (PromptArrow.Rotation <= 165)
					{
						PromptArrow.Rotation += 15;
						if (PromptNumber.HeightRequest < 45)
						{
							PromptNumber.HeightRequest += 5;
						}
						else
						{
							PromptNumber.HeightRequest = 50;
						}
						return true;
					}
					else
					{
						PromptArrow.Rotation = 180;
						OnPromptDrop = true;
						return false;
					}
				}
				else
				{

					if (PromptArrow.Rotation <= 345)
					{
						PromptArrow.Rotation += 15;
						if (PromptNumber.HeightRequest > 5)
						{
							PromptNumber.HeightRequest -= 5;
						}
						else
						{
							PromptNumber.HeightRequest = 0;
							PromptNumber.IsVisible = false;
						}
						return true;
					}
					else
					{
						PromptArrow.Rotation = 0;
						OnPromptDrop = false;
						return false;
					}
				}
			});
		}

		async void SelectImage(object sender, EventArgs e)
		{
			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsTakePhotoSupported)
			{
				await DisplayAlert("No Pick Photo", ":(No Pick Photo available.", "ok");
				return;
			}

			_mediafile = await CrossMedia.Current.PickPhotoAsync();

			if (_mediafile == null)
				return;

			ImageUp.Source = ImageSource.FromStream(() =>
			{
				return _mediafile.GetStream();
			});

			if (ImageUp.Source != null)
			{
				FrameUpImage.IsVisible = false;
			}

		}
	}
}