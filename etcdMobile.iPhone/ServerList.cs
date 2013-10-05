using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Switchboard.iPhone.Common;
using etcdMobile.Core;
using System.Drawing;
using Switchboard.iPhone.Common;


namespace Switchboard.iPhone
{
	public class ServerList : BackgroundViewController
	{
		private UILabel _title;
		private UITableView _tbl;
		private UIToolbar _toolbar;
		private SqlCache _sqlCache;
		private ServerSource _serverSource;
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			NavigationController.NavigationBarHidden = true;
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			_title = new UILabel(new RectangleF(0, 25, View.Bounds.Width, 25));
			_title.Text = "etcd Switchboard";
			_title.Font = UIFont.FromName("Futura", 20f);
			_title.TextAlignment = UITextAlignment.Center;
			_title.TextColor = UIColor.White;
			
			View.AddSubview(_title);
			
			_sqlCache = new SqlCache();
			
			_serverSource = new ServerSource(_sqlCache, NavigationController);
			_tbl = new UITableView();
			_tbl.Source = _serverSource;
			_tbl.ReloadData();
			_tbl.Frame = new RectangleF(0, 64, View.Bounds.Width, View.Bounds.Height - 50);
			_tbl.BackgroundColor = UIColor.FromRGBA(0,0,0,0);
			_tbl.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			
			View.AddSubview(_tbl);
			
			// If no servers, display label
			
			_toolbar = new UIToolbar();
			var btn = new UIButton(UIButtonType.ContactAdd);
			btn.TouchUpInside += (sender, e) => 
			{
				var alert = new UIAlertView("", "Add button pressed", null, "OK");
				alert.Show();
			};
			_toolbar.Add(btn);
			
			View.AddSubview(_toolbar);
		}
	}
}

