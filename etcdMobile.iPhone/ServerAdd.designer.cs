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
	[Register ("ServerAdd")]
	partial class ServerAdd
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView activityView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem btnSave { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgResponse { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblResponse { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtAddress { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtName { get; set; }

		[Action ("txtAddressEditingDidEnd:")]
		partial void txtAddressEditingDidEnd (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (txtName != null) {
				txtName.Dispose ();
				txtName = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (imgResponse != null) {
				imgResponse.Dispose ();
				imgResponse = null;
			}

			if (lblResponse != null) {
				lblResponse.Dispose ();
				lblResponse = null;
			}

			if (txtAddress != null) {
				txtAddress.Dispose ();
				txtAddress = null;
			}

			if (activityView != null) {
				activityView.Dispose ();
				activityView = null;
			}
		}
	}
}
