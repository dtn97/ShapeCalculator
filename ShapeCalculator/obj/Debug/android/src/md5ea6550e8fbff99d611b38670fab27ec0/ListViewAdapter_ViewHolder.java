package md5ea6550e8fbff99d611b38670fab27ec0;


public class ListViewAdapter_ViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ShapeCalculator.ListViewAdapter+ViewHolder, ShapeCalculator", ListViewAdapter_ViewHolder.class, __md_methods);
	}


	public ListViewAdapter_ViewHolder ()
	{
		super ();
		if (getClass () == ListViewAdapter_ViewHolder.class)
			mono.android.TypeManager.Activate ("ShapeCalculator.ListViewAdapter+ViewHolder, ShapeCalculator", "", this, new java.lang.Object[] {  });
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
