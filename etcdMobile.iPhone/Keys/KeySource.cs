using System;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.UIKit;
using System.Linq;
using etcdMobile.iPhone.Common;
using System.Collections.Generic;
using etcetera;

namespace etcdMobile.iPhone.Keys
{
	public class KeySource : UITableViewSource
	{
		private Node _parent;
		private Preferences _prefs;
		private Server _server;
		private IReloadableTableView _tbl;
		private List<Node> _keys;
		private UINavigationController _nav;
		public EventHandler ItemDeleted;

		public KeySource(UINavigationController nav, Server server, Node parent, IReloadableTableView tbl)
		{
			_tbl = tbl;
			_nav = nav;
			_server = server;
			_parent = parent;

			_tbl.Source = this;

			_prefs = Globals.Preferences;

			Refresh();
		}

		public virtual void Refresh()
		{
			Refresh (SortType.NameAsc); // pull from SQL Cache
		}

		public virtual void Refresh(SortType sortType) // TODO: include parent
		{
			if (_parent != null)
			{
				_keys = _server.Client.Get (_parent.Key).Node.Nodes;
			}
			else
			{
				// MARK: CRASH HERE!  OVERLOAD ALL GET METHODS
				// TODO: COULD NOT CONNECT
				try
				{
					var keys = _server.Client.Get(string.Empty);
					if(keys != null && keys.Node != null && keys.Node.Nodes != null)
					{
						_keys = keys.Node.Nodes;
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine ("Exception getting keys {0}", ex.Message);
				}
			}


			if (_keys == null || !_keys.Any ())
			{
				_keys = new List<Node> ();
			}
				
			if (_prefs.HideEtcdDir)
			{
				_keys.RemoveAll(k => k.Dir && k.KeyName() == "_etcd");
			}

			// MARK: TODO: NULL?
			switch (sortType)
			{
				case SortType.NameAsc:
					_keys = _keys.OrderBy (k => k.Key).ToList ();
					break;
				case SortType.NameDesc:
					_keys = _keys.OrderByDescending (k => k.Key).ToList ();
					break;
				case SortType.TtlAsc:
				_keys = _keys.OrderBy (k => k.Ttl.HasValue ? k.Ttl.Value : 0).ToList ();
					break;
				case SortType.TtlDesc:
				_keys = _keys.OrderByDescending (k => k.Ttl.HasValue ? k.Ttl.Value : 0).ToList ();
					break;
			}

			_tbl.ReloadData();
		}

		public void Refresh(object sender, EventArgs e)
		{
			Refresh ();
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (_prefs.ReadOnly == false && editingStyle == UITableViewCellEditingStyle.Delete) 
			{
				var key = _keys[indexPath.Row];

				var confirm = new UIAlertView ("Delete", StringValues.DeleteKeyMessage, null, "Delete", "Cancel");
				confirm.Show ();
				confirm.Clicked += (sender, e) => {
					if(e.ButtonIndex == 0) {
						UIHelper.Try(() => _server.Client.Delete (key.Key));

						Refresh(); 

						if (ItemDeleted != null)
						{
							ItemDeleted (this, null);
						}
					}
				};
			}
		}

		public override void WillBeginEditing (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.CellAt(indexPath) as UITableViewCell;
			if(cell != null)
			{

			}
		}

		public override void DidEndEditing (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.CellAt(indexPath) as UITableViewCell;
			if(cell != null)
			{

			}
		}

		public List<Node> Keys 
		{
			get { return _keys; }
		}

		public int Count
		{
			get { return _keys == null ? 0 :_keys.Count;}
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _keys == null ? 0 : _keys.Count;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{   	
			var cell = _keys[indexPath.Row] as Node;
			if (cell.Dir)
			{
				var keyList = new KeyList (_server, cell);
				_nav.PushViewController (keyList, true);
			}
			else
			{
				var keyAdd = new KeyAdd (_server, cell);
				keyAdd.OnSave += Refresh;
				keyAdd.OnDelete += Refresh;

				_nav.PushViewController (keyAdd, true);
			}
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		private readonly string _cellName = "KeyValueListCell";

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			Node cell = _keys[indexPath.Row];
			
			var tblCell = tableView.DequeueReusableCell(_cellName) as KeyListCell;
			if (tblCell == null)
			{
				tblCell = KeyListCell.Create (_nav, cell);
			}

			if (cell.Dir)
			{
				tblCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			else
			{
				tblCell.Accessory = UITableViewCellAccessory.None;
			}

			return tblCell;
		}
	}
}

