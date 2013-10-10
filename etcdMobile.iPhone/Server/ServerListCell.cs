using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace etcdMobile.iPhone
{
	public partial class ServerListCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("ServerListCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("ServerListCell");

		public ServerListCell (IntPtr handle) : base (handle)
		{
		}

		public static ServerListCell Create ()
		{
			return (ServerListCell)Nib.Instantiate (null, null) [0];
		}

		public ServerListCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
		{
		}

		public string Name
		{
			get { return this.lblName.Text; }
			set { this.lblName.Text = value; }
		}

		public string Url
		{
			get { return this.lblUrl.Text; }
			set { this.lblUrl.Text = value; }
		}
	}
}

