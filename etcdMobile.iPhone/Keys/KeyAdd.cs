using System;
using System.Drawing;
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
		private EtcdElement _key;
		private string _parentKey;

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
			tf.ResignFirstResponder ();
			return true;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			foreach (var txt in new[] { txtKey, txtTTL, txtValue })
			{
				txt.ResignFirstResponder();
				txt.ShouldReturn = DoReturn;
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

			btnSave.Clicked += (sender, e) => 
			{
				var newKey = new EtcdElement();
				newKey.Key = txtKey.Text;
				if(!newKey.Key.StartsWith("/"))
				{
					newKey.Key = "/" + newKey.Key;
				}

				newKey.Value = txtValue.Text;

				if(!string.IsNullOrWhiteSpace(txtTTL.Text))
				{
					int ttl;
					int.TryParse(txtTTL.Text, out ttl);
					newKey.Ttl = ttl;
				}

				_server.Client.SaveKey(newKey);

				if(_key != null && _key.Key != txtKey.Text)
				{
					_server.Client.DeleteKey(_key);
				}

				if(OnSave != null)
				{
					OnSave(this, null);
				}

				NavigationController.PopViewControllerAnimated(true);
			};
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

