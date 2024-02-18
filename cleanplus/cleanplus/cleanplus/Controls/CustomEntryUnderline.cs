using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace cleanplus.Controls
{
	public class CustomEntryUnderline : Entry
	{
        public static readonly BindableProperty IsPasswordFlagProperty =
        BindableProperty.Create("IsPasswordFlag", typeof(bool), typeof(CustomEntryUnderline), defaultBindingMode: BindingMode.OneWay);
        public bool IsPasswordFlag
        {
            get { return (bool)GetValue(IsPasswordFlagProperty); }
            set { SetValue(IsPasswordFlagProperty, value); }
        }
	}
}
