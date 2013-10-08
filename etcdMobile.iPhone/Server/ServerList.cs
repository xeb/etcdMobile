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
		private SqlCache _sqlCache;
		private ServerAdd _serverAddView;
		
		public ServerList () : base ("ServerList", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
		
		public override void ViewDidAppear (bool animated)
		{
			Refresh();
			base.ViewDidAppear (animated);
			NavigationController.NavigationBarHidden = true;
		}
		
		private void Refresh()
		{
			_source.Refresh();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			_sqlCache = new SqlCache();
			
			tbl.BackgroundColor = UIColor.Clear;
			tbl.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			
			_source = new ServerSource(_sqlCache, NavigationController, tbl);
			
			var refresh = new UIRefreshControl();
			refresh.ValueChanged += (sender, e) => 
			{
				Refresh();
				refresh.EndRefreshing();
			};
			
			tbl.Add(refresh);
			
			if(_source.Count == 0)
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

