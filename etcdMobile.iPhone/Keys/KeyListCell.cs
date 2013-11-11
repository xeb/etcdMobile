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
			get { return lblKey.Text; }
			set { lblKey.Text = value; }
		}

		public KeyListCell (IntPtr handle) : base (handle)
		{
		}

		[Export("initWithCoder:")]
		public KeyListCell (NSCoder coder) : base(coder)
		{

		}

		public static KeyListCell Create (UINavigationController nav, EtcdElement key)
		{
			var item = (KeyListCell)Nib.Instantiate (null, null) [0];
			
			item._nav = nav;
			item._key = key;
			item.KeyName = key.KeyName;

			if (key.Dir)
			{
				item.lblKey.TextColor = UIColor.DarkGray;
			}
			else
			{
				item.lblKey.Font = UIFont.BoldSystemFontOfSize (14f); 
			}

			return item;
		}
	}
}

