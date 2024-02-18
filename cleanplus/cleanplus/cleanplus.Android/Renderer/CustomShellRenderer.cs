using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using cleanplus.Droid;
using cleanplus.Controls;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using cleanplus.Droid.Renderer;

[assembly: ExportRenderer(typeof(CustomShell), typeof(CustomShellRenderer))]
namespace cleanplus.Droid.Renderer
{
    public class CustomShellRenderer : ShellRenderer
    {
        public CustomShellRenderer(Context context) : base(context)
        {
        }

        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new MyBottomNavigationView(this);
        }

        protected override IShellToolbarAppearanceTracker CreateToolbarAppearanceTracker()
        {
            return new CustomToolbarAppearanceTracker();
        }
    }
}

public class CustomToolbarAppearanceTracker : IShellToolbarAppearanceTracker
{
    public void Dispose()
    {
    }

	public void ResetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker)
	{
	}

	public void SetAppearance(AndroidX.AppCompat.Widget.Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
	{
        toolbar.SetTitleTextColor(Android.Graphics.Color.ParseColor("#1793A8"));

        var bgc = appearance.BackgroundColor;

        if(bgc == Xamarin.Forms.Color.FromHex("#178da4"))
		{
            LinearGradientBrush linearbg = new LinearGradientBrush
            {
                StartPoint = new Xamarin.Forms.Point(0, 0),
                EndPoint = new Xamarin.Forms.Point(0, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Xamarin.Forms.Color.FromHex("#178da4"), Offset = 0 },
                    new GradientStop { Color = Xamarin.Forms.Color.FromHex("#18b7be"), Offset = 1 }
                }
            };

            GradientDrawable bg = new GradientDrawable();
            bg.UpdateBackground(linearbg, 0, 0);

            toolbar.SetBackground(bg);
		}
		else
		{
            toolbar.SetBackgroundColor(bgc.ToAndroid());
		}
    }
}

internal class MyBottomNavigationView : IShellBottomNavViewAppearanceTracker
{
    private CustomShellRenderer androidShell;
    public MyBottomNavigationView(CustomShellRenderer androidShell)
    {
        this.androidShell = androidShell;
    }

    public void Dispose()
    {
    }

    public void ResetAppearance(BottomNavigationView bottomView)
    {
        var parameters = bottomView.LayoutParameters;
        parameters.Height = 50;

        bottomView.LayoutParameters = parameters;
    }

    public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
    {
        // Change colors on different states
        int[][] states = new int[][]{
            new int[]{-Android.Resource.Attribute.StateChecked},  // unchecked
            new int[]{Android.Resource.Attribute.StateChecked},   // checked
            new int[]{}                                // default
    };

        int[] colors = new int[]{
            Android.Graphics.Color.ParseColor("#FFFFFF"),
            Android.Graphics.Color.ParseColor("#FFFFFF"),
            Android.Graphics.Color.ParseColor("#FFFFFF"),
    };
        ColorStateList navigationViewColorStateList = new ColorStateList(states, colors);
        bottomView.ItemIconTintList = navigationViewColorStateList;
        bottomView.ItemTextColor = navigationViewColorStateList;

        LinearGradientBrush linearbg = new LinearGradientBrush
        {
            StartPoint = new Xamarin.Forms.Point(0, 0),
            EndPoint = new Xamarin.Forms.Point(0, 1),
            GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Xamarin.Forms.Color.FromHex("#178CA4"), Offset = 0 },
                    new GradientStop { Color = Xamarin.Forms.Color.FromHex("#18B7BE"), Offset = 1 }
                }
        };

        GradientDrawable bg = new GradientDrawable();
        bg.UpdateBackground(linearbg, 0, 0);
        bottomView.SetBackground(bg);
        bottomView.ItemIconSize = 80;

        IMenu menu = bottomView.Menu;
        for (int i = 0; i < bottomView.Menu.Size(); i++)
        {
            IMenuItem menuItem = menu.GetItem(i);
            var title = menuItem.TitleFormatted;
            Typeface typeface = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, "Sarabun-Light.ttf");
            SpannableStringBuilder sb = new SpannableStringBuilder(title);
            sb.SetSpan(new CustomTypefaceSpan("", typeface), 0, sb.Length(), SpanTypes.InclusiveInclusive);
            menuItem.SetTitle(sb);
        }
    }
}

class CustomTypefaceSpan : TypefaceSpan
{
    private Typeface newType;

    public CustomTypefaceSpan(String family, Typeface type) : base(family)
    {
        newType = type;
    }
    public override void UpdateDrawState(TextPaint ds)
    {
        applyCustomTypeFace(ds, newType);

    }
    public override void UpdateMeasureState(TextPaint paint)
    {
        applyCustomTypeFace(paint, newType);
    }
    private static void applyCustomTypeFace(Paint paint, Typeface tf)
    {
        TypefaceStyle oldStyle;
        Typeface old = paint.Typeface;
        if (old == null)
        {
            oldStyle = 0;
        }
        else
        {
            oldStyle = old.Style;
        }

        TypefaceStyle fake = oldStyle & ~tf.Style;
        if ((fake & TypefaceStyle.Bold) != 0)
        {
            paint.FakeBoldText = true;
        }

        if ((fake & TypefaceStyle.Italic) != 0)
        {
            paint.TextSkewX = -0.25f;
        }
        
        paint.TextSize = 24;
        paint.SetTypeface(tf);
    }
}