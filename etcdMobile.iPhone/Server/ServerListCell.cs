using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using etcdMobile.iPhone;
using etcdMobile.iPhone.Common;

namespace etcdMobile.iPhone
{
	public partial class ServerListCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("ServerListCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("ServerListCell");

		public ServerListCell (IntPtr handle) : base (handle)
		{
		}

		public static ServerListCell Create (UINavigationController nav, Server server)
		{
			var cell = (ServerListCell)Nib.Instantiate (null, null) [0];
			cell.btnEdit.TouchUpInside += (sender, e) => 
			{
				var edit = new ServerAdd(server);
				nav.PushViewController(edit, true);
			};

			return cell;
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

