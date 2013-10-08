using System;
using System.Drawing;
using System.Collections.Generic;
using etcdMobile.iPhone;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using etcdMobile.iPhone.Common;

namespace etcdMobile.iPhone
{
	public partial class ServerList : UIViewController
	{
		private ServerSource _source;
		private ServerAdd _serverAddView;
		
		public ServerList () : base ("ServerList", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			NavigationController.NavigationBarHidden = true;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			tbl.BackgroundColor = UIColor.Clear;
			tbl.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			
			var servers = new List<Server>();
//			servers.Add(new Server { Name = "Test" });
			
			_source = new ServerSource(servers, NavigationController);
			tbl.Source = _source;
			
			if(servers.Count == 0)
			{
				tbl.Hidden = true;
				lbl.Text = "No Servers Added";
				lbl.Font = UIFont.BoldSystemFontOfSize(16f);
				lbl.Frame = new RectangleF(0, View.Bounds.Height / 2 - 50, View.Bounds.Width, 30);
			}
			
			btnAdd.Clicked += (sender, e) => 
			{
				if(_serverAddView == null)
					_serverAddView = new ServerAdd();
					
				NavigationController.PushViewController(_serverAddView, true);
			};
		}
	}
}

