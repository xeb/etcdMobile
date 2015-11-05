using System;
using System.Threading.Tasks;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using etcetera;
using etcdMobile.iPhone.Common;

namespace etcdMobile.iPhone
{
	public partial class ServerAdd : UIViewController
	{
		private SqlCache _sqlCache;
		private Server _serverForEdit;

		public ServerAdd (Server server) : base ("ServerAdd", null)
		{
			_serverForEdit = server;
		}

		public ServerAdd() : this(null) 
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			if (_serverForEdit == null)
			{
				txtAddress.Text = string.Empty;
				txtName.Text = string.Empty;
			}
			else
			{
				txtName.Text = _serverForEdit.Name;
				txtAddress.Text = _serverForEdit.BaseUrl;
			}

			imgResponse.Hidden = true;
			lblResponse.Hidden = true;
			NavigationController.NavigationBarHidden = false;

			base.ViewWillAppear (animated);
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			if (_serverForEdit == null)
			{
				Title = "Add New Server";
			}
			else
			{
				Title = "Edit " + _serverForEdit.Name;
			}

			base.ViewDidLoad ();
			
			_sqlCache = new SqlCache();

			txtAddress.ResignFirstResponder();
			txtAddress.ShouldReturn = DoReturn;

			txtName.ResignFirstResponder ();
			txtName.ShouldReturn = DoReturn;

			btnSave.Enabled = false;
			
			btnSave.Clicked += (sender, e) => 
			{
				var server = new Server { Name = txtName.Text, BaseUrl = txtAddress.Text };
				server.Id = _serverForEdit != null ? _serverForEdit.Id : 0;

				_sqlCache.SaveServer(server);

				NavigationController.PopToRootViewController(true);
			};

			btnVerify.Clicked += (sender, e) => 
			{
				txtAddressEditingDidEnd(null);
			};
		}

		bool DoReturn (UITextField tf)
		{
			tf.ResignFirstResponder ();
			return true;
		}
		
		partial void txtAddressEditingDidEnd (MonoTouch.Foundation.NSObject sender)
		{
			txtAddress.EndEditing(true);
			
			imgResponse.Hidden = true;
			lblResponse.Hidden = true;
			txtError.Hidden = true;
			
			activityView.Hidden = false;
			activityView.StartAnimating();
			
			var address = txtAddress.Text;
			InvokeInBackground(() =>
			{
				bool isValid = false;

				Exception ex;
				try
				{
					var sc = new EtcdClient(new Uri(address));
					isValid = sc.IsValidEndpoint(out ex);
				}
				catch(Exception ex2)
				{
					ex = ex2;
				}

				if(isValid)
				{
					InvokeOnMainThread(() => {
						imgResponse.Image = UIImage.FromBundle("Images/check.png");
						lblResponse.Text = "Valid endpoint";
						lblResponse.TextColor = UIColor.FromRGB(0, 100, 0);
					});
				}
				else
				{
					InvokeOnMainThread(() => {
						imgResponse.Image = UIImage.FromBundle("Images/xmark.png");
						lblResponse.Text = "Invalid endpoint";
						lblResponse.TextColor = UIColor.FromRGB(100, 0, 0);
						txtError.Hidden = false;
						txtError.Text = ex.Message;
					});
				}
				
				InvokeOnMainThread(() => {
					activityView.StopAnimating();
					imgResponse.Hidden = false;
					lblResponse.Hidden = false;
					btnSave.Enabled = isValid;
				});
			});
		}
	}
}

