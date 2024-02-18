using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using cleanplus.Droid;
using cleanplus.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using cleanplus.Droid.Renderer;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace cleanplus.Droid.Renderer
{
	public class CustomFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
		public CustomFrameRenderer(Context context) : base(context)
		{
		}

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            var view = (CustomFrame)Element;

            if (view.HasShadow)
            {
                Elevation = 10f;
                TranslationZ = 10f;
            }
        }
    }
}