using System;
using System.Drawing;
using System.Collections.Generic;
using etcdMobile.iPhone;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using etcdMobile.iPhone.Common;

namespace etcdMobile.iPhone
{
	public partial class ServerList : UIViewController
	{
		private ServerSource _source;
		private ISqlCache _sqlCache;
		private ServerAdd _serverAddView;
		private PreferenceList _prefs;
		
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
			NavigationController.NavigationBarHidden = false;
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
			
			Title = "etcd Mobile Admin";
			lbl.Text = "No Servers Added";
			lbl.Font = UIFont.BoldSystemFontOfSize(16f);
			lbl.Frame = new RectangleF(0, View.Bounds.Height / 2 - 50, View.Bounds.Width, 30);
			lbl.Hidden = true;

			_sqlCache = Globals.SqlCache;
			_prefs = new PreferenceList();
			
			var btnAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add);
			btnAdd.Clicked += (sender, e) => 
			{
				if(_serverAddView == null)
					_serverAddView = new ServerAdd();
					
				NavigationController.PushViewController(_serverAddView, true);
			};
			
			NavigationItem.RightBarButtonItems = new[] { btnAdd }.ToArray();
			
			
			tbl.BackgroundColor = UIColor.Clear;
			tbl.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			
			_source = new ServerSource(NavigationController, _sqlCache, tbl);
			_source.ItemDeleted += (sender, e) => { Refresh(); };

			var refresh = new UIRefreshControl();
			refresh.ValueChanged += (sender, e) => 
			{
				Refresh();
				refresh.EndRefreshing();
			};
			
			tbl.Add(refresh);

			btnSettings.Clicked += (sender, e) => 
			{
				NavigationController.PushViewController(_prefs, true);
			};
		}
	}
}

