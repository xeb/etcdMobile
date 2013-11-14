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
		MonoTouch.UIKit.UIBarButtonItem btnDelete { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem btnSave { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblDateLocal { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblDateUtc { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblIndex { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblReadOnly { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch swtBoolVal { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtKey { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtTTL { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtValue { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnDelete != null) {
				btnDelete.Dispose ();
				btnDelete = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (lblDateLocal != null) {
				lblDateLocal.Dispose ();
				lblDateLocal = null;
			}

			if (lblDateUtc != null) {
				lblDateUtc.Dispose ();
				lblDateUtc = null;
			}

			if (lblIndex != null) {
				lblIndex.Dispose ();
				lblIndex = null;
			}

			if (lblReadOnly != null) {
				lblReadOnly.Dispose ();
				lblReadOnly = null;
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

			if (swtBoolVal != null) {
				swtBoolVal.Dispose ();
				swtBoolVal = null;
			}
		}
	}
}
