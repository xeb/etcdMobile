using System;
using System.Drawing;
using System.Collections.Generic;
using etcdMobile.iPhone;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using etcdMobile.iPhone.Common;

namespace etcdMobile.iPhone
{
	public partial class PreferenceList : UIViewController
	{
		private Preferences _prefs;

		public PreferenceList () : base ("PreferenceList", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			_prefs = Globals.Preferences;

			base.ViewDidLoad ();

			Title = "Preferences";

//			lblWarning.Text = string.Format (lblWarning.Text, EtcdElement.READ_ONLY_NAME);

			PopulatePreferences ();

			swtReadOnly.ValueChanged += (sender, e) => { SavePreferences(); };
			swtHideEtcdDir.ValueChanged += (sender, e) => { SavePreferences(); };
			swtRefresh.ValueChanged += (sender, e) => { SavePreferences(); };

			btnReset.TouchUpInside += (sender, e) => 
			{
				_prefs.SetToDefault();

				PopulatePreferences();
			};

		}

		private void SavePreferences()
		{
			_prefs.ReadOnly = swtReadOnly.On;
			_prefs.HideEtcdDir = swtHideEtcdDir.On;
			_prefs.RefreshOnSave = swtRefresh.On;

			_prefs.SaveToCache ();
		}

		private void PopulatePreferences()
		{
			swtReadOnly.On = _prefs.ReadOnly;
			swtHideEtcdDir.On = _prefs.HideEtcdDir;
			swtRefresh.On = _prefs.RefreshOnSave;
		}
	}
}

