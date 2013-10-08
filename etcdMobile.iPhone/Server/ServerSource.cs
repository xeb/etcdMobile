using System;
using MonoTouch.Foundation;
using System.Drawing;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace etcdMobile.iPhone.Common
{
	public class ServerSource : UITableViewSource
	{
		private UINavigationController _nav;
		private UITableView _tbl;
		private List<Server> _servers;
		private SqlCache _sqlCache;
		
		public ServerSource(SqlCache sqlCache, UINavigationController nav, UITableView tbl)
		{
			_nav = nav;
			_tbl = tbl;
			_sqlCache = sqlCache;
			
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
			if (editingStyle == UITableViewCellEditingStyle.Delete) {
				var server = _servers[indexPath.Row];
//				tableView.CellAt(indexPath).Dispose();
				_sqlCache.DeleteServer(server);
				Refresh();
				
				
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
            
            tblCell.BackgroundColor = UIColor.Clear;
            tblCell.TextLabel.Text = cell.Name;
            tblCell.TextLabel.Font = UIFont.BoldSystemFontOfSize(14f);
            
            tblCell.Frame = new RectangleF(tblCell.Frame.X, tblCell.Frame.Y, tblCell.Frame.Width, tblCell.Frame.Height * 2);
            
            var f = tblCell.TextLabel.Frame;
            tblCell.TextLabel.Frame = new RectangleF(f.X , f.Y - 20, f.Width, f.Height);
            
            var lblUrl = new UILabel();
            lblUrl.Font = UIFont.SystemFontOfSize(12);
            lblUrl.Text = cell.BaseUrl;
            lblUrl.Frame = new RectangleF(f.X, f.Y + 15, f.Width, f.Height);
           	tblCell.Add(lblUrl);
            
            return tblCell;
        }
	}
}

