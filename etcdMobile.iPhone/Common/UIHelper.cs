using System;
using System.Threading;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace etcdMobile.iPhone
{
	public static class UIHelper
	{
		public static void ShowError(string title, string message, params object[] parameters)
		{
			var ui = new UIAlertView(title, string.Format(message, parameters), null, "OK");
			ui.Show();
		}

		public static void Try(Action action)
		{
			try
			{
				action();
			}
			catch(System.Net.WebException ex)
			{
				var response = ex.Response as System.Net.HttpWebResponse;
				ShowError("ERROR", "Error from the server. \n ({0}) {1}", Convert.ToString((int)response.StatusCode), response.StatusCode);
			}
		} 
	}
}

