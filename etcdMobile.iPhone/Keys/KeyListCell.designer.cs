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
	[Register ("KeyListCell")]
	partial class KeyListCell
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView img { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lbl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch swtch { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lbl != null) {
				lbl.Dispose ();
				lbl = null;
			}

			if (swtch != null) {
				swtch.Dispose ();
				swtch = null;
			}

			if (img != null) {
				img.Dispose ();
				img = null;
			}
		}
	}
}
