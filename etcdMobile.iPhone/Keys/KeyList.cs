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
		private SortType _sort;
		private Preferences _prefs;
		
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
			if (_prefs.RefreshOnSave)
			{
				Refresh ();
			}

			base.ViewWillAppear (animated);
			NavigationController.NavigationBarHidden = false;
		}

		public void Refresh()
		{
			_source.Refresh (_sort);

			if (_source.Count == 0)
			{
				// Shouldn't happen, go to ValueEdit  ?
			}
			else
			{
				SetStats ();
			}
		}

		public void Refresh(object sender, EventArgs e)
		{
			Refresh ();
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

			_prefs = Globals.Preferences;

			if (_prefs.ReadOnly == false)
			{
				var btnAdd = new UIBarButtonItem (UIBarButtonSystemItem.Add);
				btnAdd.Clicked += (sender, e) =>
				{
					var keyAdd = new KeyAdd (_server, _parentKey != null ? _parentKey.Key : "/");
					keyAdd.OnSave += Refresh;
					NavigationController.PushViewController (keyAdd, true);
				};
		
				NavigationItem.RightBarButtonItems = new[] { btnAdd }.ToArray ();
			}

			table.BackgroundColor = UIColor.Clear;
			table.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			_source = new KeySource(NavigationController, _server, _parentKey, new ReloadableTableWrapper(table));
			_source.ItemDeleted += Refresh;

			SetStats ();

			btnSort.Clicked += (sender, e) => 
			{
				if(_sort == SortType.NameAsc)
				{
					btnSort.Image = UIImage.FromBundle("alphabetical_sorting_za-25.png");;
					_sort = SortType.NameDesc;
				}
				else 
				{
					btnSort.Image = UIImage.FromBundle("alphabetical_sorting_az-25.png");
					_sort = SortType.NameAsc;
				}

				Refresh();
			};

			var refresh = new UIRefreshControl();
			refresh.ValueChanged += (sender, e) => 
			{
				Refresh();
				refresh.EndRefreshing();
			};

			table.Add(refresh);
		}

		private void SetStats()
		{
			lblIndex.Text = _source.Keys.First ().Index.ToString();
			lblKeys.Text = _source.Keys.Count.ToString();
			lblDirs.Text = _source.Keys.Count (k => k.Dir).ToString();
		}

		private class ReloadableTableWrapper : IReloadableTableView
		{
			private UITableView _tbl;
			public ReloadableTableWrapper(UITableView tbl)
			{
				_tbl = tbl;
			}

			public void ReloadData()
			{
				_tbl.ReloadData ();
			}

			public UITableViewSource Source
			{
				set { _tbl.Source = value; }
			}
		}
	}
}

