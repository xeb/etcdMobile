using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Switchboard.iPhone.Common;
using etcdMobile.Core;
using System.Drawing;

namespace Switchboard.iPhone
{
	public class KeyList : UIViewController
	{
		private UILabel _title;
		private UIBarButtonItem _add;
		private EtcdElement _previousCell;
		private List<EtcdElement> _cells;
		
		public KeyList(List<EtcdElement> cells, EtcdElement previousCell)
		{
			_cells = cells;
			_previousCell = previousCell;
		}
		
		public KeyList()
		{
		}
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}
		
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			NavigationController.NavigationBarHidden = _previousCell == null;
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			if(_previousCell != null)
			{
				Title = _previousCell.Key;
				_add = new UIBarButtonItem(UIBarButtonSystemItem.Add);
				_add.Clicked += (sender, e) => 
				{
					var alert = new UIAlertView("", "Adding...", null, "OK");
					alert.Show();
				};
				NavigationItem.RightBarButtonItem = _add;
			}
			
			View.BackgroundColor = UIColor.FromRGB(16,16,16);
			
			_title = new UILabel(new RectangleF(0, 25, View.Bounds.Width, 15));
			_title.Text = "etcd Switchboard";
			_title.Font = UIFont.FromName("Futura", 20f);
			_title.TextAlignment = UITextAlignment.Center;
			_title.TextColor = UIColor.White;
			
			View.AddSubview(_title);
			
			if(_cells == null)
			{
				_cells = new List<EtcdElement>();
				_cells.Add(new EtcdElement { Key = "Test 1" });
				_cells.Add(new EtcdElement { Key = "Test 2" });
				_cells.Add(new EtcdElement { Key = "Test 3" });
				_cells.Add(new EtcdElement { Key = "Test 4" });
			}
			
			var keySource = new KeySource(_cells, NavigationController);
			
			var tbl = new UITableView();
			tbl.Source = keySource;
			tbl.ReloadData();
			tbl.Frame = new RectangleF(0, 64, View.Bounds.Width, View.Bounds.Height);
			tbl.BackgroundColor = UIColor.FromRGBA(0,0,0,0);
			tbl.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			
			this.View.AddSubview(tbl);
			
		}
		
		public class KeySource : EtcdElementSource
		{
			private UINavigationController _nav;
			public KeySource(List<EtcdElement> cells, UINavigationController nav) : base(cells, nav)
			{
				_nav = nav;
			}
			
			public override void ModifyNewCell (UITableViewCell tableCell, EtcdElement cell)
			{
				tableCell.BackgroundColor = UIColor.FromRGBA(0,0,0,0);
				tableCell.TextLabel.TextColor = UIColor.FromRGB(190,190,190);
				tableCell.SelectedBackgroundView = new UIView 
				{ 
					BackgroundColor =  UIColor.FromRGB(50,50,50),
				};
				
				if(!string.IsNullOrWhiteSpace(cell.Value) && new[]{ "true", "false" }.Contains(cell.Value.ToLower()))
				{
					var switchctl = new UISwitch(new RectangleF(tableCell.Bounds.Width - 60, 5, 40, 30));
					switchctl.On = cell.Value == "true";
					tableCell.AddSubview(switchctl);
				}
			}
			
			public override void CellSelected (EtcdElement cell)
			{
				base.CellSelected (cell);
				
				var newCells = new List<EtcdElement>();
				newCells.Add(new EtcdElement { Key = "Test A" });
				newCells.Add(new EtcdElement { Key = "Test B", Value = "true" });
				newCells.Add(new EtcdElement { Key = "Test C" });
				newCells.Add(new EtcdElement { Key = "Test D", Value = "false" });
				newCells.Add(new EtcdElement { Key = "Very Long Name" });
				
				var keylist = new KeyList(newCells, cell);
				_nav.PushViewController(keylist, true);
			}
		}
	}
}
