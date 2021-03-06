using System;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using etcetera;
using etcdMobile.iPhone.Common;
using etcdMobile.iPhone.Keys;

namespace etcdMobile.iPhone
{
	public partial class KeyAdd : UIViewController
	{
		private Server _server;
		private Preferences _prefs;
		private Node _key;
		private string _parentKey;

		public KeyAdd(Server server, string parentKey) : this(server, (Node)null)
		{
			_parentKey = parentKey;

			if (!_parentKey.EndsWith ("/"))
			{
				_parentKey += "/";
			}
		}

		public KeyAdd (Server server, Node key) : base ("KeyAdd", null)
		{
			_server = server;
			_key = key;
		}

		public event EventHandler OnSave;
		public event EventHandler OnDelete;

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

			NSNotificationCenter.DefaultCenter.AddObserver ("UITextFieldTextDidEndEditingNotification", EditingEnded);

			_prefs = Globals.Preferences;

			var toolbar = new UIToolbar (new RectangleF(0.0f, 0.0f, this.View.Frame.Size.Width, 44.0f));
			toolbar.TintColor = UIColor.White;
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;

			toolbar.Items = new UIBarButtonItem[]{
				new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
				new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate {
					DoReturn(txtTTL);
				})
			};

			txtTTL.InputAccessoryView = toolbar;

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
			lblRelative.Hidden = true;

			if (_key != null)
			{
				Title = _key.KeyName();
				txtKey.Text = _key.Key;
				txtValue.Text = _key.Value;

				if (_key.Ttl.HasValue)
				{
					txtTTL.Text = _key.Ttl.Value.ToString();

					SetDates (_key.Ttl);
				}

				lblIndex.Text = _key.CreatedIndex.ToString ();
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
					var newKey = new Node ();
					newKey.Key = txtKey.Text;
					if (!newKey.Key.StartsWith ("/"))
					{
						newKey.Key = "/" + newKey.Key;
					}

					newKey.Value = txtValue.Text;

					if (!string.IsNullOrWhiteSpace (txtTTL.Text))
					{
						int ttl;
						if(int.TryParse (txtTTL.Text, out ttl))
						{
							newKey.Ttl = ttl;
						}
					}

					UIHelper.Try(() => _server.Client.Set(newKey.Key, newKey.Value, (newKey.Ttl ?? 0)));

					if (_key != null && _key.Key != txtKey.Text)
					{
						UIHelper.Try(() => _server.Client.Delete(_key.Key));
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
						var confirm = new UIAlertView ("Delete", StringValues.DeleteKeyMessage, null, "Delete", "Cancel");
						confirm.Show ();
						confirm.Clicked += (sender2, e2) => {
							if(e2.ButtonIndex == 0) {

								UIHelper.Try (() => _server.Client.Delete(_key.Key));

								if(OnDelete != null)
								{
									OnDelete(this, null);
								}

								NavigationController.PopViewControllerAnimated (true);
							}
						};

					};

					btnCancel.Clicked += (sender, e) => 
					{
						ViewDidLoad();
					};
				}
			}
			else
			{
				btnSave.Enabled = false;
				btnCancel.Enabled = false;
				btnDelete.Enabled = false;
			}
		}

		private void EditingEnded(NSNotification notification)
		{
			if (!string.IsNullOrWhiteSpace(txtTTL.Text))
			{
				var alert = new UIAlertView ("Invalid TTL", "", null, "OK");
				int ttl;
				if (int.TryParse (txtTTL.Text, out ttl))
				{
					var secondsUntilDoom = (DateTime.MaxValue - DateTime.UtcNow).TotalSeconds;
					if (ttl >= secondsUntilDoom)
					{
						alert.Message = "WAY too big.  You know you don't *have* to set a time-to-live, right?";
						alert.Show ();
						ResetTTL ();
					}
					else
					{
						SetDates (ttl);
					}
				}
				else
				{
					alert.Message = "TTL value cannot be parsed";
					alert.Show ();

					ResetTTL ();
				}
			}
		}

		private void SetDates(int? ttl)
		{
			if(ttl.HasValue && ttl.Value > 0)
			{
				var expiration = DateTime.UtcNow.AddSeconds (ttl.Value);
				SetDatesFromUtcDate (expiration);
				lblRelative.Text = NodeExtensions.GetFriendlyTtl(ttl);
				lblRelative.Hidden = false;
			}
		}

		private void ResetTTL()
		{
			txtTTL.Text = "";
			lblDateUtc.Hidden = true;
			lblDateLocal.Hidden = true;
			lblRelative.Hidden = true;
		}

		private void SetDatesFromUtcDate(DateTime utc)
		{	
			lblDateUtc.Hidden = false;
			lblDateLocal.Hidden = false;
			lblDateUtc.Text = utc.ToString ("yyyy-MM-dd HH:mm:ss") + " UTC";
			lblDateLocal.Text = utc.ToLocalTime ().ToString ("yyyy-MM-dd hh:mm:sstt") + " Local";
		}
	}
}

