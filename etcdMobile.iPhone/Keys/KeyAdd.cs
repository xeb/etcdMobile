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

		public KeyAdd(Server server) : this(server, null)
		{
		}

		public KeyAdd (Server server, EtcdElement key) : base ("KeyAdd", null)
		{
			_server = server;
			_key = key;
		}
		

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

			if (_key != null)
			{
				Title = _key.KeyName;
				txtKey.Text = _key.Key;
				txtValue.Text = _key.Value;

				lblExpiration.Hidden = !_key.Ttl.HasValue;
				lblExpires.Hidden = !_key.Ttl.HasValue;
				btnClearTTL.Hidden = !_key.Ttl.HasValue;

				txtTTL.Text = _key.Ttl.HasValue ? _key.Ttl.Value.ToString () : "";

				stpValue.Hidden = true;
				swtch.Hidden = true;
			}
			else
			{
				Title = "New Key";
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

				NavigationController.PopViewControllerAnimated(true);
			};
		}
	}
}

