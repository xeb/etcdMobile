using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using etcdMobile.iPhone.Keys;
using etcdMobile.Core;

namespace etcdMobile.iPhone
{
	public partial class KeyListCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("KeyListCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("KeyListCell");

		public UINavigationController _nav;
		public EtcdElement _key;

		public string KeyName 
		{
			get { return lbl.Text; }
			set { lbl.Text = value; }
		}



		public KeyListCell (IntPtr handle) : base (handle)
		{
		}

		public static KeyListCell Create (UINavigationController nav, EtcdElement key)
		{
			var item = (KeyListCell)Nib.Instantiate (null, null) [0];
			
			item._nav = nav;
			item._key = key;
			item.swtch.Hidden = true;
			item.KeyName = key.KeyName;

			if (key.Dir)
			{
				item.lbl.TextColor = UIColor.DarkGray;
			}
			else
			{
				item.lbl.Font = UIFont.BoldSystemFontOfSize (14f); 
			}

			return item;
		}
	}
}

