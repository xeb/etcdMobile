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
		
		public override void ViewWillAppear (bool animated)
		{
			Refresh ();

			base.ViewWillAppear (animated);
			NavigationController.NavigationBarHidden = true;
		}
		
		public void Refresh()
		{
			_source.Refresh ();

			if(_source.Count == 0)
			{
				tbl.Hidden = true;
				lbl.Hidden = false;
			}
			else
			{
				tbl.Hidden = false;
				lbl.Hidden = true;
			}
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			lbl.Text = "No Servers Added";
			lbl.Font = UIFont.BoldSystemFontOfSize(16f);
			lbl.Frame = new RectangleF(0, View.Bounds.Height / 2 - 50, View.Bounds.Width, 30);
			lbl.Hidden = true;

			_sqlCache = new SqlCache();
			
			tbl.BackgroundColor = UIColor.Clear;
			tbl.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			
			_source = new ServerSource(_sqlCache, tbl);
			_source.ItemDeleted += (sender, e) => { Refresh(); };

			var refresh = new UIRefreshControl();
			refresh.ValueChanged += (sender, e) => 
			{
				Refresh();
				refresh.EndRefreshing();
			};
			
			tbl.Add(refresh);

			btnAdd.Clicked += (sender, e) => 
			{
				if(_serverAddView == null)
					_serverAddView = new ServerAdd();
					
				NavigationController.PushViewController(_serverAddView, true);
			};
		}
	}
}

