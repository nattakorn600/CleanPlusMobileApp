

using Android.Content;
using Android.Content.Res;
using cleanplus.Controls;
using cleanplus.Droid.Renderer;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomRatio), typeof(CustomRatioRenderer))]
namespace cleanplus.Droid.Renderer
{
	public class CustomRatioRenderer : RadioButtonRenderer
	{
		public CustomRatioRenderer(Context context) : base(context)
		{
		}
		protected override void OnElementChanged(ElementChangedEventArgs<RadioButton> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.ButtonTintList = ColorStateList.ValueOf(Color.FromHex("#2c96ac").ToAndroid());
			}
		}
	}
}