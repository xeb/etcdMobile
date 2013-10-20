using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;
using etcdMobile.iPhone.Common;
using etcdMobile.Core;
using etcdMobile.iPhone.Keys;

namespace etcdMobile.iPhone
{
	public partial class KeyList : UIViewController
	{
		private Server _server;
		private EtcdElement _parentKey;
		private KeySource _source;
		private SqlCache _sqlCache;
		
		public KeyList (Server server) : base ("KeyList", null)
		{
			_server = server;
		}

		public KeyList(Server server, EtcdElement parent) : this(server)
		{
			_parentKey = parent;
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
				// Shouldn't happen, go to ValueEdit  
			}
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = _parentKey == null ? _server.Name : _parentKey.KeyName;

			_sqlCache = new SqlCache();

			var btnAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add);
			btnAdd.Clicked += (sender, e) => 
			{
				// TODO: Add Key
			};

			NavigationItem.RightBarButtonItems = new[] { btnAdd }.ToArray();

			tbl.BackgroundColor = UIColor.Clear;
			tbl.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			_source = new KeySource(NavigationController, _server, _parentKey, _sqlCache, tbl);
			_source.ItemDeleted += (sender, e) => { Refresh(); };

			var refresh = new UIRefreshControl();
			refresh.ValueChanged += (sender, e) => 
			{
				Refresh();
				refresh.EndRefreshing();
			};

			tbl.Add(refresh);
		}
	}
}

