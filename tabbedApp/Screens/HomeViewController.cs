
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace tabbedApp
{
	public partial class HomeViewController : UIViewController
	{
		public HomeViewController () : base ("HomeViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		UIWebView webView;

		LoadingOverlay _loadPop;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationController.SetNavigationBarHidden (true, true);
			// Determine the correct size to start the overlay (depending on device orientation)
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
				bounds.Size = new SizeF (bounds.Size.Height, bounds.Size.Width);
			}

			// show the loading overlay on the UI thread using the correct orientation sizing
			this._loadPop = new LoadingOverlay (bounds);
			//this.View.Add ( this._loadPop );

			Title = "Home";

			string url = "http://graficos.elfinanciero.com.mx/test/app-lauman/presentacion.html";
			//documentLoader.LoadRequest (new NSUrlRequest (new NSUrl (url)));

			webView = new UIWebView (View.Bounds);
			//webView.Frame = new RectangleF (0,80,320,520);
			webView.LoadRequest (new NSUrlRequest (new NSUrl (url)));

			webView.ScalesPageToFit = true;
			webView.UserInteractionEnabled = true;

			View.AddSubviews (new UIView[] { webView  }); // multiple add

			bool t = webView.IsLoading;

			if (!t) {
				View.AddSubview (this._loadPop);
			}

			webView.LoadFinished += (object sender, EventArgs e) => {
				this._loadPop.Hide ();
			};
		}
	}
}

