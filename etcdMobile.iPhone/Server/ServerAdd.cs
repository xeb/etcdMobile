using System;
using System.Threading.Tasks;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using etcdMobile.Core;
using etcdMobile.iPhone.Common;

namespace etcdMobile.iPhone
{
	public partial class ServerAdd : UIViewController
	{
		private SqlCache _sqlCache;
		public ServerAdd () : base ("ServerAdd", null)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			txtAddress.Text = string.Empty;
			txtName.Text = string.Empty;

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
			Title = "Add New Server";
			
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
				{
					_sqlCache.SaveServer(server);
				}
			
				NavigationController.PopToRootViewController(true);
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
			
			activityView.Hidden = false;
			activityView.StartAnimating();
			
			var address = txtAddress.Text;
			InvokeInBackground(() =>
			{
				
				var sc = new EtcdClient(address);
				var isValid = sc.IsValidEndpoint();
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

