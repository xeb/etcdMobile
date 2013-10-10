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
	[Register ("ServerListCell")]
	partial class ServerListCell
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnEdit { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblUrl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblUrl != null) {
				lblUrl.Dispose ();
				lblUrl = null;
			}

			if (lblName != null) {
				lblName.Dispose ();
				lblName = null;
			}

			if (btnEdit != null) {
				btnEdit.Dispose ();
				btnEdit = null;
			}
		}
	}
}
