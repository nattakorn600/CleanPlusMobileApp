﻿using cleanplus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cleanplus.Views.Admin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatAdminPage : ContentPage
	{
		private bool OnLoad = true;
		private int LastId = 0;
		private string ChatPosition = "";
		public ObservableCollection<ChatMessage> Message;
		UserChat Item = new UserChat();
		string ImageLabel = "";
		public ChatAdminPage(UserChat Data)
		{
			InitializeComponent();
			Item = Data;
			ImageLabel = Data.User_Name[0].ToString();
		}

		async void ShowMessage()
		{
			try
			{
				using (var cl = new HttpClient())
				{
					var formcontent = new FormUrlEncodedContent(new[]
					{
					new KeyValuePair<string,string>("lastid", LastId.ToString()),
					new KeyValuePair<string, string>("usid", Item.User_Id.ToString())
				});
					var request = await cl.PostAsync(Application.Current.Properties["domain"] +
						"/cleanplus/chat/usershowmessage.php?", formcontent);
					request.EnsureSuccessStatusCode();
					var response = await request.Content.ReadAsStringAsync();
					var res = JsonConvert.DeserializeObject<List<ChatMessage>>(response);
					Message = new ObservableCollection<ChatMessage>(res);
				}

				if (Message.Count > 0)
				{
					int count = Message.Count;
					LastId = Message[count - 1].Id;
					for (int i = 0; i < count; i++)
					{
						if (Message[i].User == Item.User_Id.ToString())
						{
							bool ImgVisible = true;
							if (ChatPosition == "left")
							{
								ImgVisible = false;
							}
							Grid leftchat = new Grid()
							{
								ColumnDefinitions =
							{
								new ColumnDefinition { Width = new GridLength(50)}
							},
							};

							leftchat.Children.Add(new Frame
							{
								IsVisible = ImgVisible,
								Margin = new Thickness(0, 0, 0, 6),
								Padding = 5,
								BackgroundColor = Color.FromHex("#17b3bc"),
								CornerRadius = 100,
								HeightRequest = 25,
								WidthRequest = 25,
								HorizontalOptions = LayoutOptions.CenterAndExpand,
								VerticalOptions = LayoutOptions.StartAndExpand,
								Content = new Label
								{
									HorizontalOptions = LayoutOptions.Center,
									VerticalOptions = LayoutOptions.Center,
									Text = ImageLabel,
									FontSize = 12
								}
							}, 0, 0);

							leftchat.Children.Add(new Frame
							{
								Margin = new Thickness(0, 0, 0, 6),
								Padding = 10,
								BackgroundColor = Color.FromHex("#FFF"),
								CornerRadius = 10,
								HorizontalOptions = LayoutOptions.StartAndExpand,
								VerticalOptions = LayoutOptions.StartAndExpand,
								Content = new Label
								{
									HorizontalOptions = LayoutOptions.Start,
									VerticalOptions = LayoutOptions.Center,
									FontSize = 12,
									Text = Message[i].Message,
									TextColor = Color.Black
								}
							}, 1, 0);
							MessItem.Children.Add(leftchat);
							ChatPosition = "left";
						}
						else
						{
							var rightchat = new Grid()
							{
								ColumnDefinitions = {
								new ColumnDefinition { Width = new GridLength(50)}
							},
							};

							rightchat.Children.Add(new Frame
							{
								Margin = new Thickness(0, 0, 3, 6),
								Padding = 10,
								BackgroundColor = Color.FromHex("#e1f2f2"),
								CornerRadius = 10,
								HorizontalOptions = LayoutOptions.EndAndExpand,
								VerticalOptions = LayoutOptions.Center,
								Content = new Label
								{
									FontSize = 12,
									HorizontalOptions = LayoutOptions.Start,
									VerticalOptions = LayoutOptions.Center,
									TextColor = Color.Black,
									Text = Message[i].Message
								}
							}, 1, 0);
							MessItem.Children.Add(rightchat);
							ChatPosition = "right";
						}
						Scroll.ScrollToAsync(MessItem.Children.LastOrDefault(), ScrollToPosition.MakeVisible, false);
					}
				}
			}catch
			{

			}
		}
		async void SendMessage(object sender, EventArgs e)
		{
			if (textMessage.Text != null && textMessage.Text != "")
			{
				using (var cl = new HttpClient())
				{
					var formcontent = new FormUrlEncodedContent(new[]
					{
					new KeyValuePair<string,string>("user1","CleanPlus"),
					new KeyValuePair<string, string>("user2",Item.User_Id.ToString()),
					new KeyValuePair<string, string>("msg",textMessage.Text)
				});
					var request = await cl.PostAsync(Application.Current.Properties["domain"] +
						"/cleanplus/chat/usermessage.php?", formcontent);
					request.EnsureSuccessStatusCode();
					var response = await request.Content.ReadAsStringAsync();
					var res = JsonConvert.DeserializeObject<ChatMessage>(response);

					if (res.Status == "success")
					{
						textMessage.Text = null;
					}
				}
			}
		}

		protected override void OnAppearing()
		{
			base.OnDisappearing();
			Device.StartTimer(TimeSpan.FromMilliseconds(600), () =>
			{
				if (OnLoad == true || LastId > 0)
				{
					OnLoad = false;
					ShowMessage();
				}
				return true;
			});
		}
		/*protected override void OnDisappearing()
		{
			base.OnDisappearing();

			MessagingCenter.Unsubscribe<ChatPage, TransitionType>(this, TransitionSetting.TransitionMessage);
		}

		void ChangePage(Page next, TransitionType animation)
		{
			MessagingCenter.Subscribe<ChatPage, TransitionType>(this, TransitionSetting.TransitionMessage, (s, arg) =>
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
		
		void BackButtonClick(object sender, EventArgs e)
		{
			Navigation.PopAsync(false);
		}

		protected override bool OnBackButtonPressed()
		{
			Navigation.PopAsync(false);
			return true;
		}
	}
}