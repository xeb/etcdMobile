// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace etcdMobile.iPhone
{
	[Register ("ServerList")]
	partial class ServerList
	{
		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem btnAdd { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lbl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView tbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAdd != null) {
				btnAdd.Dispose ();
				btnAdd = null;
			}

			if (tbl != null) {
				tbl.Dispose ();
				tbl = null;
			}

			if (lbl != null) {
				lbl.Dispose ();
				lbl = null;
			}
		}
	}
}
