package crc6434ef4d78207ddfb1;


public class CustomRatioRenderer
	extends crc643f46942d9dd1fff9.RadioButtonRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("cleanplus.Droid.Renderer.CustomRatioRenderer, cleanplus.Android", CustomRatioRenderer.class, __md_methods);
	}


	public CustomRatioRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomRatioRenderer.class)
			mono.android.TypeManager.Activate ("cleanplus.Droid.Renderer.CustomRatioRenderer, cleanplus.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public CustomRatioRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomRatioRenderer.class)
			mono.android.TypeManager.Activate ("cleanplus.Droid.Renderer.CustomRatioRenderer, cleanplus.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public CustomRatioRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomRatioRenderer.class)
			mono.android.TypeManager.Activate ("cleanplus.Droid.Renderer.CustomRatioRenderer, cleanplus.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
