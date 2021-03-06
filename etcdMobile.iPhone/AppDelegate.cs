using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace etcdMobile.iPhone
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		private UIWindow _window;
		private UINavigationController _nav;
		private UIViewController _root;
		
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			_window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			_root = new ServerList();
			_nav = new UINavigationController();
			_nav.PushViewController(_root, true);
			
			_window.RootViewController = _nav;
			
			_window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

