using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using etcdMobile.Core;

namespace Switchboard.iPhone.Common
{
	public class ServerSource : UITableViewSource
	{
		protected UINavigationController NavigationController;
		protected SqlCache SqlCache;
		private List<Server> _servers;
		
		public ServerSource(SqlCache sql, UINavigationController nav)
		{
			NavigationController = nav;
			SqlCache = sql;
			
			Refresh();
		}
		
		public virtual void Refresh()
		{
			_servers = SqlCache.GetServers();
		}
		
        public override int RowsInSection (UITableView tableview, int section)
        {
        	return _servers == null ? 0 : _servers.Count;
        }
        
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {   	
    		var cell = _servers[indexPath.Row];
    	}
        
        public override int NumberOfSections (UITableView tableView)
        {
        	return 1;
        }
        
        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            Server cell = _servers[indexPath.Row];
            
			var tblCell = tableView.DequeueReusableCell("TblCell");
			if (tblCell == null)
        	{
        		tblCell = new UITableViewCell(UITableViewCellStyle.Default, "TblCell");
        	}
            
            tblCell.TextLabel.Text = cell.Name;
            
            return tblCell;
        }
	}
}

