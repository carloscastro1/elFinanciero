using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace tabbedApp
{
	public class ComnunicadoDetalleHistorial : UIViewController
	{
		Comunicado comunicado;
		UIWebView webView;
		protected LoadingOverlay _loadPop = null;

		public ComnunicadoDetalleHistorial (Comunicado comunicadoDetalle)
		{
			comunicado = comunicadoDetalle;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Determine the correct size to start the overlay (depending on device orientation)
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || 
				UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
				bounds.Size = new SizeF(bounds.Size.Height, bounds.Size.Width);
			}

			// show the loading overlay on the UI thread using the correct orientation sizing
			this._loadPop = new LoadingOverlay (bounds);
			//this.View.Add ( this._loadPop );


			Title = comunicado.TituloComunicado;

			string url = comunicado.urlComunicado;
			//documentLoader.LoadRequest (new NSUrlRequest (new NSUrl (url)));

			webView = new UIWebView (View.Bounds);
			//webView.Frame = new RectangleF (0,80,320,520);
			webView.LoadRequest (new NSUrlRequest (new NSUrl (url)));

			webView.ScalesPageToFit = true;
			webView.UserInteractionEnabled = true;

			View.AddSubviews(new UIView[] {  webView  }); // multiple add


			bool t = webView.IsLoading;

			if (!t) {
				View.AddSubview(this._loadPop);
			}

			webView.LoadFinished += (object sender, EventArgs e) => {
				this._loadPop.Hide ();
			};

		}

	}
}

