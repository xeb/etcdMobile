using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MonoTouch.UIKit;

namespace etcdMobile.iPhone.Common
{
	public class BackgroundViewController : UIViewController
	{		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			View.BackgroundColor = UIColor.Black;
			var background = UIImage.FromResource(GetType().Assembly, "background.png");
			var backgroundView = new UIImageView(background);
			backgroundView.Frame = this.View.Bounds;
			this.View.AddSubview(backgroundView);
		}
	}
}

