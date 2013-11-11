using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using etcdMobile.Core;
using etcdMobile.iPhone.Common;


namespace etcdMobile.iPhone
{
	public interface IReloadableTableView
	{
		void ReloadData();
		UITableViewSource Source {set;}
	}
}

