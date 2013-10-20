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
	[Register ("KeyAdd")]
	partial class KeyAdd
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnClearTTL { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem btnDelete { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem btnSave { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblExpiration { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblExpires { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblWarning { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIStepper stpTTL { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIStepper stpValue { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch swtch { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtKey { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtTTL { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtValue { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnClearTTL != null) {
				btnClearTTL.Dispose ();
				btnClearTTL = null;
			}

			if (lblExpiration != null) {
				lblExpiration.Dispose ();
				lblExpiration = null;
			}

			if (lblExpires != null) {
				lblExpires.Dispose ();
				lblExpires = null;
			}

			if (stpTTL != null) {
				stpTTL.Dispose ();
				stpTTL = null;
			}

			if (stpValue != null) {
				stpValue.Dispose ();
				stpValue = null;
			}

			if (swtch != null) {
				swtch.Dispose ();
				swtch = null;
			}

			if (txtKey != null) {
				txtKey.Dispose ();
				txtKey = null;
			}

			if (txtTTL != null) {
				txtTTL.Dispose ();
				txtTTL = null;
			}

			if (txtValue != null) {
				txtValue.Dispose ();
				txtValue = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (btnDelete != null) {
				btnDelete.Dispose ();
				btnDelete = null;
			}

			if (lblWarning != null) {
				lblWarning.Dispose ();
				lblWarning = null;
			}
		}
	}
}
