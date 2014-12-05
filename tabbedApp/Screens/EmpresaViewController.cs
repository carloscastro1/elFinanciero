using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace tabbedApp
{
	public partial class EmpresaViewController : UIViewController
	{
		EmpresasModel empresa;

		public EmpresaViewController (EmpresasModel muestraEmpresa)
		{
			empresa = muestraEmpresa;
		}



		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

		}

		LoadingOverlay _loadPop;

		UIWebView webView;

		UIScrollView scrollView;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			// iOS7: Keep content from hiding under navigation bar.
			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0)) {
				EdgesForExtendedLayout = UIRectEdge.None;
			}

			Title = empresa.NombreEmpresa;
			View.BackgroundColor = UIColor.White;

			base.ViewDidLoad ();

			// Determine the correct size to start the overlay (depending on device orientation)
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
				bounds.Size = new SizeF (bounds.Size.Height, bounds.Size.Width);
			}

			// show the loading overlay on the UI thread using the correct orientation sizing
			this._loadPop = new LoadingOverlay (bounds);


			scrollView = new UIScrollView (
				new RectangleF (0, 0, View.Frame.Width
					, View.Frame.Height - NavigationController.NavigationBar.Frame.Height));


			scrollView.BackgroundColor = UIColor.White;
			string url = empresa.maquetaHtmlUrl;

			webView = new UIWebView (new RectangleF (0, 0, View.Frame.Width
				, 410));
			webView.LoadRequest (new NSUrlRequest (new NSUrl (url)));

			webView.ScalesPageToFit = true;
			webView.UserInteractionEnabled = true;
			webView.SizeToFit ();

			scrollView.AddSubview (webView);

			View.AddSubviews (new UIView[] { scrollView }); // multiple add


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

