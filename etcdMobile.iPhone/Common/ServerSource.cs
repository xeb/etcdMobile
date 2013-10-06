using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace etcdMobile.iPhone.Common
{
	public class ServerSource : UITableViewSource
	{
		protected UINavigationController NavigationController;
		private List<Server> _servers;
		
		public ServerSource(List<Server> servers, UINavigationController nav)
		{
			NavigationController = nav;
			
			Refresh(servers);
		}
		
		public virtual void Refresh(List<Server> servers)
		{
			_servers = servers;
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
            
            tblCell.BackgroundColor = UIColor.FromRGBA(0,0,0,0);
            tblCell.TextLabel.Text = cell.Name;
            
            return tblCell;
        }
	}
}

