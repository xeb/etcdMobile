using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using etcdMobile.Core;
using etcdMobile.iPhone.Common;
using etcdMobile.iPhone.Keys;

namespace etcdMobile.iPhone
{
	public partial class KeyAdd : UIViewController
	{
		private Server _server;
		private Preferences _prefs;
		private EtcdElement _key;
		private string _parentKey;
		private UIButton _doneButton;

		public KeyAdd(Server server, string parentKey) : this(server, (EtcdElement)null)
		{
			_parentKey = parentKey;

			if (!_parentKey.EndsWith ("/"))
			{
				_parentKey += "/";
			}
		}

		public KeyAdd (Server server, EtcdElement key) : base ("KeyAdd", null)
		{
			_server = server;
			_key = key;
		}

		public event EventHandler OnSave;

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			NavigationController.NavigationBarHidden = false;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
		}

		bool DoReturn (UITextField tf)
		{
			View.EndEditing (true);
			return true;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_prefs = Globals.Preferences;

			NSNotificationCenter.DefaultCenter.AddObserver ("UIKeyboardWillHideNotification", KeyboardHide);
			NSNotificationCenter.DefaultCenter.AddObserver ("UIKeyboardWillShowNotification", KeyboardShow);

			_doneButton = new UIButton (UIButtonType.Custom);
			_doneButton.Frame = new RectangleF (0, 163, 106, 53);
			_doneButton.SetTitle ("DONE", UIControlState.Normal);
			_doneButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			_doneButton.SetTitleColor (UIColor.LightGray, UIControlState.Highlighted);

			_doneButton.TouchUpInside += (sender, e) => 
			{
				DoReturn(null);
			};

			foreach (var txt in new[] { txtKey, txtValue })
			{
				txt.ShouldReturn = DoReturn;
			}

			if (_prefs.ReadOnly)
			{
				lblReadOnly.Hidden = false;
			}
			else
			{
				lblReadOnly.Hidden = true;
			}

			txtTTL.Text = string.Empty;

			lblDateUtc.Hidden = true;
			lblDateLocal.Hidden = true;

			if (_key != null)
			{
				Title = _key.KeyName;
				txtKey.Text = _key.Key;
				txtValue.Text = _key.Value;

				if (_key.Ttl.HasValue)
				{
					txtTTL.Text = _key.Ttl.Value.ToString();

					if(_key.ExpirationDate.HasValue)
					{
						var utc = _key.ExpirationDate.Value.ToUniversalTime ();
						SetDatesFromUtcDate (utc);
					}
				}

				lblIndex.Text = _key.Index.ToString();
			}
			else
			{
				Title = "New Key";
				txtKey.Text = _parentKey;
				txtTTL.Text = "";
			}

			if (_prefs.ReadOnly == false)
			{
				btnSave.Clicked += (sender, e) =>
				{
					var newKey = new EtcdElement ();
					newKey.Key = txtKey.Text;
					if (!newKey.Key.StartsWith ("/"))
					{
						newKey.Key = "/" + newKey.Key;
					}

					newKey.Value = txtValue.Text;

					if (!string.IsNullOrWhiteSpace (txtTTL.Text))
					{
						int ttl;
						int.TryParse (txtTTL.Text, out ttl);
						newKey.Ttl = ttl;
					}

					UIHelper.Try(() => _server.Client.SaveKey (newKey));

					if (_key != null && _key.Key != txtKey.Text)
					{
						UIHelper.Try(() => _server.Client.DeleteKey (_key));
					}

					if (OnSave != null)
					{
						OnSave (this, null);
					}

					NavigationController.PopViewControllerAnimated (true);
				};

				if (_key != null)
				{
					btnDelete.Clicked += (sender, e) =>
					{
						UIHelper.Try (() => _server.Client.DeleteKey (_key));
						NavigationController.PopViewControllerAnimated (true);
					};
				}	
			}
			else
			{
				btnSave.Enabled = false;
				btnDelete.Enabled = false;
			}
		}

		public void KeyboardShow(NSNotification notification)
		{
			var keyboard = txtTTL.WeakInputDelegate as UIView;

			if (keyboard != null)
			{
				_doneButton.Hidden = false;
				keyboard.AddSubview (_doneButton);
			}
		}

		public void KeyboardHide(NSNotification notification)
		{
			_doneButton.Hidden = true;
		}

		private void SetDatesFromUtcDate(DateTime utc)
		{	
			lblDateUtc.Hidden = false;
			lblDateLocal.Hidden = false;
			lblDateUtc.Text = utc.ToString ("yyyy-MM-dd HH:mm:ss") + " UTC";
			lblDateLocal.Text = utc.ToLocalTime ().ToString ("yyyy-M-d hh:mm:sstt") + " Local";
		}
	}
}

