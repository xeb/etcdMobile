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
	[Register ("PreferenceList")]
	partial class PreferenceList
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnReset { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem btnSave { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblWarning { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch swtHideEtcdDir { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch swtReadOnly { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch swtRefresh { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnReset != null) {
				btnReset.Dispose ();
				btnReset = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (swtHideEtcdDir != null) {
				swtHideEtcdDir.Dispose ();
				swtHideEtcdDir = null;
			}

			if (swtReadOnly != null) {
				swtReadOnly.Dispose ();
				swtReadOnly = null;
			}

			if (swtRefresh != null) {
				swtRefresh.Dispose ();
				swtRefresh = null;
			}

			if (lblWarning != null) {
				lblWarning.Dispose ();
				lblWarning = null;
			}
		}
	}
}
