using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using cleanplus.Droid;
using cleanplus.Controls;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using cleanplus.Droid.Renderer;

[assembly: Dependency(typeof(CustomStatusbarRenderer))]
namespace cleanplus.Droid.Renderer
{
	public class CustomStatusbarRenderer : IStatusBarPlatformSpecific
	{
		public CustomStatusbarRenderer()
		{
		}

        public void SetStatusBarColor(Color color)
        {
            // The SetStatusBarcolor is new since API 21
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var androidColor = color.ToAndroid();
                CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(androidColor);
            }
            else
            {
                // Here you will just have to set your 
                // color in styles.xml file as shown below.
            }
        }

    }
}