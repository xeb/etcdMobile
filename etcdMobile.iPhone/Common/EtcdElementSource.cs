using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using etcetera;

namespace etcdMobile.iPhone.Common
{
	public class EtcdElementSource : UITableViewSource
	{
		protected UINavigationController NavigationController;
		protected List<Node> Cells;
		
		public EtcdElementSource(List<Node> cells, UINavigationController nav)
		{
			NavigationController = nav;
			Cells = cells;
		}
		
        public override int RowsInSection (UITableView tableview, int section)
        {
        	return Cells == null ? 0 : Cells.Count;
        }
        
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {   	
    		var cell = Cells[indexPath.Row];
    		CellSelected(cell);
        }
        
		public virtual void CellSelected(Node cell)
        {
			Console.WriteLine("Selected Cell '{0}'", cell.KeyName());
        }
        
        public virtual System.Drawing.SizeF GetImageSize()
        {
        	return new System.Drawing.SizeF(23, 30);
        }
        
        public override int NumberOfSections (UITableView tableView)
        {
        	return 1;
        }
        
		public virtual void ModifyNewCell(UITableViewCell tableCell, Node simpleCell)
        {
        }
        
		public virtual void ModifyReusedCell(UITableViewCell tableCell, Node simpleCell)
        {
        }
       
        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
			Node cell = Cells[indexPath.Row];
            
			var tblCell = tableView.DequeueReusableCell("TblCell");
			if (tblCell == null)
        	{
        		tblCell = new UITableViewCell(UITableViewCellStyle.Default, "TblCell");
        		ModifyNewCell(tblCell, cell);
        	}
            
            tblCell.TextLabel.Text = cell.Key;
            ModifyReusedCell(tblCell, cell);
            
            return tblCell;
        }
	}
}

