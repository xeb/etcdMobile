using System;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace etcdMobile.iPhone.Common
{
	public class ServerSource : UITableViewSource
	{
		private UITableView _tbl;
		private List<Server> _servers;
		private SqlCache _sqlCache;
		private UINavigationController _nav;
		public EventHandler ItemDeleted;
		
		public ServerSource(UINavigationController nav, SqlCache sqlCache, UITableView tbl)
		{
			_tbl = tbl;
			_sqlCache = sqlCache;
			_nav = nav;
			
			_tbl.Source = this;

			Refresh();
		}
		
		public virtual void Refresh()
		{
			_servers = _sqlCache.GetServers();
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
				var server = _servers[indexPath.Row];
				_sqlCache.DeleteServer(server);
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
//				cell.BackgroundColor = UIColor.Green;
			}
		}
		
		public override void DidEndEditing (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.CellAt(indexPath) as UITableViewCell;
			if(cell != null)
			{
//				cell.BackgroundColor = UIColor.Green;
			}
		}

		
		public int Count
		{
			get { return _servers == null ? 0 :_servers.Count;}
		}
		
        public override int RowsInSection (UITableView tableview, int section)
        {
        	return _servers == null ? 0 : _servers.Count;
        }
        
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {   	
    		var cell = _servers[indexPath.Row] as Server;
			var keyList = new KeyList (cell);
			_nav.PushViewController (keyList, true);
    	}
        
        public override int NumberOfSections (UITableView tableView)
        {
        	return 1;
        }

		private readonly string _cellName = "ServerCell";
        
        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            Server cell = _servers[indexPath.Row];
            
			var tblCell = tableView.DequeueReusableCell(_cellName) as ServerListCell;
			if (tblCell == null)
        	{
				tblCell = ServerListCell.Create (_nav, cell);
        	}
            
			tblCell.Name = cell.Name;
			tblCell.Url = cell.BaseUrl;
			tblCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return tblCell;
        }
	}
}

