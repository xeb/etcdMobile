using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Switchboard.iPhone.Common
{
	public class SimpleSource : UITableViewSource
	{
		protected UINavigationController NavigationController;
		protected List<SimpleCell> Cells;
		
		public SimpleSource(List<SimpleCell> cells, UINavigationController nav)
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
        
        public virtual void CellSelected(SimpleCell cell)
        {
        	Console.WriteLine("Selected Cell '{0}'", cell.Key);
        }
        
        public virtual System.Drawing.SizeF GetImageSize()
        {
        	return new System.Drawing.SizeF(23, 30);
        }
        
        public override int NumberOfSections (UITableView tableView)
        {
        	return 1;
        }
        
        public virtual void ModifyNewCell(UITableViewCell tableCell, SimpleCell simpleCell)
        {
        }
        
        public virtual void ModifyReusedCell(UITableViewCell tableCell, SimpleCell simpleCell)
        {
        }
       
        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            SimpleCell cell = Cells[indexPath.Row];
            
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

