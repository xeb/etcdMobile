using System;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.UIKit;
using etcdMobile.iPhone.Common;
using etcdMobile.Core;
using System.Collections.Generic;

namespace etcdMobile.iPhone.Keys
{
	public class KeySource : UITableViewSource
	{
		private EtcdElement _parent;
		private Server _server;
		private UITableView _tbl;
		private List<EtcdElement> _keys;
		private UINavigationController _nav;
		private SqlCache _sqlCache;
		public EventHandler ItemDeleted;

		public KeySource(UINavigationController nav, Server server, EtcdElement parent, SqlCache sqlCache, UITableView tbl)
		{
			_tbl = tbl;
			_sqlCache = sqlCache;
			_nav = nav;
			_server = server;
			_parent = parent;

			_tbl.Source = this;

			Refresh();
		}

		public virtual void Refresh() // TODO: include parent
		{
			if (_parent != null)
			{
				_keys = _server.Client.GetChildKeys (_parent);
			}
			else
			{
				_keys = _server.Client.GetKeys ();
			}

			_tbl.ReloadData();
		}

		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete) 
			{
				var key = _keys[indexPath.Row];

				// TODO: do the delete

				Refresh(); 

				if (ItemDeleted != null)
				{
					ItemDeleted (this, null);
				}
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
			var cell = _keys[indexPath.Row] as EtcdElement;
			if (cell.Dir)
			{
				var keyList = new KeyList (_server, cell);
				_nav.PushViewController (keyList, true);
			}
			else
			{
				var keyAdd = new KeyAdd (_server, cell);
				_nav.PushViewController (keyAdd, true);
			}
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		private readonly string _cellName = "KeyCell";

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			EtcdElement cell = _keys[indexPath.Row];

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

