using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Switchboard.iPhone.Common;
using etcdMobile.Core;
using System.Drawing;


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
			
			View.TintColor = UIColor.White;
			
			_title = new UILabel(new RectangleF(0, 30, View.Bounds.Width, 30));
			_title.Text = "etcd Mobile Admin";
			_title.Font = UIFont.FromName("Futura", 19f);
			_title.TextAlignment = UITextAlignment.Center;
			_title.TextColor = UIColor.White;
			
			View.AddSubview(_title);
			
			_sqlCache = new SqlCache();
			
			_serverSource = new ServerSource(_sqlCache, NavigationController);
			_tbl = new UITableView();
			_tbl.Source = _serverSource;
			_tbl.ReloadData();
			_tbl.Frame = new RectangleF(0, 64, View.Bounds.Width, View.Bounds.Height - 70);
			_tbl.BackgroundColor = UIColor.FromRGBA(0,0,0,0);
			_tbl.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			
			View.AddSubview(_tbl);
			
			// If no servers, display label
			
			_toolbar = new UIToolbar();
			var btn = new UIBarButtonItem(UIBarButtonSystemItem.Add);
			btn.TintColor = UIColor.White;
			btn.Clicked += (sender, e) => 
			{
				var alert = new UIAlertView("", "Add button pressed", null, "OK");
				alert.Show();
			};
//			
			_toolbar.Items = new UIBarButtonItem[] 
			{ 
				new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
				btn,
			};
			
			_toolbar.Frame = new RectangleF(0, View.Bounds.Height - 40, View.Bounds.Width, 40);
			_toolbar.BackgroundColor = UIColor.FromRGBA(0,0,0,0);
			_toolbar.BarStyle = UIBarStyle.Black;
			_toolbar.Translucent = true;
			
			View.AddSubview(_toolbar);
		}
	}
}

