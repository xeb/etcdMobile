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

			PopulatePreferences ();

			swtReadOnly.ValueChanged += (sender, e) => { SavePreferences(); };
			swtHideEtcdDir.ValueChanged += (sender, e) => { SavePreferences(); };
			swtRefresh.ValueChanged += (sender, e) => { SavePreferences(); };
			swtSmartVal.ValueChanged += (sender, e) => { SavePreferences(); };

			SetReadOnlyStates ();

			swtReadOnly.ValueChanged += (object sender, EventArgs e) =>
			{
				SetReadOnlyStates();
			};

			btnReset.TouchUpInside += (sender, e) => 
			{
				_prefs.SetToDefault();

				PopulatePreferences();
			};

			var alert = new UIAlertView ("Smart Values", "Smart Values use the value of a given key to display a user-friendly control.  For example, a key value of 'true' will enable a Switch so you can toggle the key's value between 'true' and 'false'.  You will still be able to edit the value directly as a string.", null, "OK");

			btnSmartValHelp.TouchUpInside += (sender, e) => 
			{
				alert.Show();
			};
		}

		private void SavePreferences()
		{
			_prefs.ReadOnly = swtReadOnly.On;
			_prefs.HideEtcdDir = swtHideEtcdDir.On;
			_prefs.RefreshOnSave = swtRefresh.On;
			_prefs.UseSmartValues = swtSmartVal.On;

			_prefs.SaveToCache ();
		}

		private void PopulatePreferences()
		{
			swtReadOnly.On = _prefs.ReadOnly;
			swtHideEtcdDir.On = _prefs.HideEtcdDir;
			swtRefresh.On = _prefs.RefreshOnSave;
			swtSmartVal.On = _prefs.UseSmartValues;

			SetReadOnlyStates ();
		}

		private void SetReadOnlyStates()
		{
			if(swtReadOnly.On)
			{
				swtSmartVal.On = false;
				swtSmartVal.Enabled = false;
			}
			else
			{
				swtSmartVal.Enabled = true;
			}
		}
	}
}

