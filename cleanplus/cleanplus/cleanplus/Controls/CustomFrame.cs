using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace cleanplus.Controls
{
	public class CustomFrame : Frame
	{
        //Elevation = 8.0f;
        //  TranslationZ = 10.0f;

        public static readonly BindableProperty elevationProperty =
        BindableProperty.Create(nameof(elevation),
            typeof(float), typeof(CustomFrame), 8.0f);
  
        public float elevation
        {
            get => (float)GetValue(elevationProperty);
            set => SetValue(elevationProperty, value);
        }

        public static readonly BindableProperty translationzProperty =
        BindableProperty.Create(nameof(translationz),
            typeof(float), typeof(CustomFrame), 10.0f);

        public float translationz
        {
            get => (float)GetValue(translationzProperty);
            set => SetValue(translationzProperty, value);
        }
    }
}
