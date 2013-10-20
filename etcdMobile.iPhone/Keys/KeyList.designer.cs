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
		MonoTouch.UIKit.UITableView tbl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tbl != null) {
				tbl.Dispose ();
				tbl = null;
			}
		}
	}
}
