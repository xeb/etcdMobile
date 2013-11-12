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
	[Register ("KeyList")]
	partial class KeyList
	{
		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem btnSearch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem btnSort { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblDirs { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblIndex { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblKeys { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView table { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblDirs != null) {
				lblDirs.Dispose ();
				lblDirs = null;
			}

			if (lblIndex != null) {
				lblIndex.Dispose ();
				lblIndex = null;
			}

			if (lblKeys != null) {
				lblKeys.Dispose ();
				lblKeys = null;
			}

			if (table != null) {
				table.Dispose ();
				table = null;
			}

			if (btnSort != null) {
				btnSort.Dispose ();
				btnSort = null;
			}

			if (btnSearch != null) {
				btnSearch.Dispose ();
				btnSearch = null;
			}
		}
	}
}
