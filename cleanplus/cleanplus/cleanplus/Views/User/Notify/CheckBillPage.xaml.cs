using Acr.UserDialogs;
using cleanplus.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.User.Notify
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckBillPage : ContentPage
	{
		private MediaFile _mediafile;
		private bool OnPromptDrop = false;
		private bool OnBankDrop = false;
		ServicePayment servicePay = new ServicePayment();
		public CheckBillPage(ServicePayment servicepayment)
		{
			InitializeComponent();
			servicePay = servicepayment;

			textName.Text = Application.Current.Properties["user_name"].ToString();
			textPhone.Text = Application.Current.Properties["user_phone"].ToString();
			textAddress.Text = Application.Current.Properties["user_address"].ToString();

			textTatang.Text = servicePay.TarangMate.ToString();
			textDate.Text = servicePay.Date;
			textTime.Text = servicePay.Time.ToString();
			textPrice.Text = "฿ " + servicePay.Price.ToString("N");
			textVat.Text = "฿ " + (servicePay.Price * 0.07).ToString("N");
			textSum.Text = "฿ " + (servicePay.Price * 1.07).ToString("N");
			btnPayment.Text = "ชำระเงินยอดรวมทั้งสิ้น " + textSum.Text;
		}
		async void UpLoadData(object sender, EventArgs e)
		{
			if (_mediafile != null)
			{
				try
				{
					await PopupNavigation.Instance.PushAsync(new LoadingPop());
					WebClient client = new WebClient();
					client.Credentials = CredentialCache.DefaultCredentials;
					client.UploadFile(Application.Current.Properties["domain"] + "/cleanplus/booking/updatepayment.php?user_id="
						+ Application.Current.Properties["user_id"].ToString() + "&b_id=" + servicePay.Booking_Id
						+ "&n_id=" + servicePay.Noti_Id, "POST", _mediafile.Path);
					client.Dispose();
					await PopupNavigation.Instance.PopAsync();
					//Application.Current.MainPage = new AppShell();
					await Navigation.PopToRootAsync();
				}
				catch (Exception err)
				{
					await PopupNavigation.Instance.PopAsync();
					await DisplayAlert("Error!", err.Message, "OK");
				}
			}
			else
			{
				await DisplayAlert("ข้อมูลไม่ครบ", "กรุณาอัพโหลดหลักฐานการจ่ายเงิน", "OK");
			}
			
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
		void BackButtonClick(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			//MessagingCenter.Unsubscribe<CheckBillPagexaml, TransitionType>(this, TransitionSetting.TransitionMessage);
		}
		/*void ChangePage(Page next, TransitionType animation)
		{
			MessagingCenter.Subscribe<CheckBillPagexaml, TransitionType>(this, TransitionSetting.TransitionMessage, (s, arg) =>
			{
				var transitionType = (TransitionType)arg;
				var transitionNavigationPage = Parent as Controls.TransitionNavigationPage;

				if (transitionNavigationPage != null)
				{
					transitionNavigationPage.TransitionType = transitionType;
					Navigation.PushAsync(next);
				}
			});

			MessagingCenter.Send(this, TransitionSetting.TransitionMessage, animation);
		}*/
	}
}