using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.AVFoundation;

namespace tabbedApp
{
	public class MisionViewController  : UIViewController
	{
		//AVPlayer _player;
		//AVPlayerLayer _playerLayer;
		//AVAsset _asset;
		//AVPlayerItem _playerItem;

		UIWebView webView;

		LoadingOverlay _loadPop;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavigationController.SetNavigationBarHidden (true, true);
			Title = "Lauman";

			/*
			//View.BackgroundColor = UIColor.White;
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("img/fondos/bg-mision-vision@2x.png"));
			View.SizeToFit ();
			var _titulo = new UILabel ();
			_titulo.Text = "Este será el titulo de la mision de la empresa";
			_titulo.Frame = new RectangleF (20, 20, 200, 300);
			//_titulo.AdjustsFontSizeToFitWidth = true;

			*/
			/*
			_asset = AVAsset.FromUrl (NSUrl.FromFilename ("video/Home/sample.m4v"));
			_playerItem = new AVPlayerItem (_asset);   

			_player = new AVPlayer (_playerItem);  
			_playerLayer = AVPlayerLayer.FromPlayer (_player);
			_playerLayer.Frame = View.Frame;

			View.Layer.AddSublayer (_playerLayer);

			_player.Play ();
			*/

			//string url = comunicado.urlComunicado;
			//documentLoader.LoadRequest (new NSUrlRequest (new NSUrl (url)));
			/*
			webView = new UIWebView (View.Bounds);
			webView.Frame = new RectangleF (0, 180, 300, 300);
			//webView.LoadRequest (new NSUrlRequest (new NSUrl (url)));
	
			webView.ScalesPageToFit = true;
			webView.UserInteractionEnabled = true;


			string test = @"<!DOCTYPE html>
							<html>
							<body>
								<iframe width=""400"" height=""400"" src=""http://www.youtube.com/embed/EzgGTTtR0kc""></iframe>
							</body>
							</html>";
			webView.LoadHtmlString (test, null);

			View.AddSubviews (new UIView[] { 
				_titulo,
				webView
			});
			*/
			// Determine the correct size to start the overlay (depending on device orientation)
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
				bounds.Size = new SizeF (bounds.Size.Height, bounds.Size.Width);
			}

			// show the loading overlay on the UI thread using the correct orientation sizing
			this._loadPop = new LoadingOverlay (bounds);
			//this.View.Add ( this._loadPop );

			Title = "Home";

			string url = "http://graficos.elfinanciero.com.mx/test/app-lauman/lauman.html";
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

