using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using cleanplus.Controls;
using cleanplus.Droid.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntryUnderline), typeof(CustomEntryUnderlineRenderer))]
namespace cleanplus.Droid.Renderer
{
	public class CustomEntryUnderlineRenderer : EntryRenderer
	{
		public CustomEntryUnderlineRenderer(Context context) : base(context)
		{
		}
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gd);
                this.Control.SetPadding(20, 0, 0, 0);

                CustomEntryUnderline customEntry = (CustomEntryUnderline)e.NewElement;
                if (customEntry.IsPasswordFlag)
                {
                    this.Control.InputType = InputTypes.TextVariationVisiblePassword;
                }

            }
        }
    }
}