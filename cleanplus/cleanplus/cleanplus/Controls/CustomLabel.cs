using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace cleanplus.Controls
{
     public class CustomLabel : Label
    {
        private static int _defaultLineSetting = -1;

        public static readonly BindableProperty LinesProperty = BindableProperty.Create(nameof(Lines), typeof(int), typeof(CustomLabel), _defaultLineSetting);
        public int Lines
        {
            get { return (int)GetValue(LinesProperty); }
            set { SetValue(LinesProperty, value); }
        }
    }
}
