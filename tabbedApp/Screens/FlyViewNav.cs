// FlyViewNav.cs
//
//  Author:
//       Carlos Jonathan Castro Méndez <ccastro@elfinanciero.com.mx>
//
//  Copyright (c) 2014 Grupo Lauman
//
using System;
using System.Linq;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;

using FlyoutNavigation;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;


namespace tabbedApp
{
	public class FlyViewNav: UIViewController
	{
		FlyoutNavigationController navigation;

		string idCompañia = String.Empty;

		string[] Tasks = {
			" ",
		};

		public static List<ComunicadosAnterioresModel> historialComunicados;

		public  void ReadText ()
		{
			try {
				var documents =
					Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
				var filename = Path.Combine (documents, "UserData/user.txt");

				//Console.WriteLine (filename);

				if (System.IO.File.Exists (filename)) {
					var text = System.IO.File.ReadAllText (filename);    
					//TODO: remove console output
					//Console.WriteLine (text);
					idCompañia = text;
				} else {
					//TODO: remove console output
					//Console.WriteLine ("No hay archivo de configuracion");
				}
			} catch (Exception ex) {
				//Console.WriteLine (ex.Message.ToString ());
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			ReadText ();

			XmlDocument doc = new XmlDocument ();

			//TODO: replace XML file name with the id for company

			//string compania = String.Empty;



			//doc.Load ("http://187.141.3.25/LaumanAppRest/reportes/comtelsat/1.xml");

			doc.Load ("http://187.141.3.25/LaumanAppRest/employeesPermissions.php?id=" + idCompañia);

			Xml2Json xml2Json = new Xml2Json ();
			var LeerArchivoXMLConfiguracionListadoHistorialComunicados = xml2Json.XmlToJSON (doc);
			var ListaComunicados = JsonConvert.DeserializeObject<RootObjectComunicado> (LeerArchivoXMLConfiguracionListadoHistorialComunicados);
			//

			//List<ComunicadosAnterioresModel> comunicados;

			List<string> urlComunidado = new List<string> ();

			// Create the flyout view controller, make it large,
			// and add it as a subview:


			//comunicados = ComunicadosData.historialComunicados;


			/*
			foreach (Comunicado com in ListaComunicados.Comunicados.Comunicado) {
				Console.WriteLine (com.urlComunicado);
			}
			*/

			navigation = new FlyoutNavigationController ();
			//navigation.Position = FlyOutNavigationPosition.Left;
			//navigation.View.Frame = UIScreen.MainScreen.Bounds;
			View.AddSubview (navigation.View);
			//this.AddChildViewController (navigation);
			this.Title = "Comunicados";

			// Create the menu:

			navigation.NavigationRoot = new RootElement ("Comunicados") {

				//HACK: this is was used becacuse the navigation controller is a liitle buggy and does not move complete the nav controller
				new Section ("") {
					new StringElement ("") as Element
				},

				new Section ("Historial de Comunicados") {
					from comunicado in ListaComunicados.Comunicados.Comunicado
					select new StringElement (comunicado.TituloComunicado) as Element
				}
			};


			foreach (Comunicado comunicadoList in ListaComunicados.Comunicados.Comunicado) {
				urlComunidado.Add (comunicadoList.urlComunicado);
				Console.WriteLine ("\n" + comunicadoList.TituloComunicado + "\n" + comunicadoList.urlComunicado + "\n");
			}

			// Create an array of UINavigationControllers that correspond to your
			// menu items:
			navigation.ViewControllers = Array.ConvertAll (urlComunidado.ToArray (), title =>
				new UINavigationController (new TaskPageController (navigation, title))
			);


			this.NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem (UIBarButtonSystemItem.Add, (sender, args) => {
					navigation.ToggleMenu ();

				})
				, true);


			View.AddSubview (navigation.View);
		}

		class TaskPageController : DialogViewController
		{
			UIWebView webView;
			protected LoadingOverlay _loadPop = null;

			public TaskPageController (FlyoutNavigationController navigation, string title) : base (null)
			{

				var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
				if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
					bounds.Size = new SizeF (bounds.Size.Height + 10, bounds.Size.Width);
				}

				// show the loading overlay on the UI thread using the correct orientation sizing
				this._loadPop = new LoadingOverlay (bounds, "Cargando reporte");
				this.View.Add ( this._loadPop );
				string url = title;
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

				this.NavigationItem.SetRightBarButtonItem (
					new UIBarButtonItem (UIBarButtonSystemItem.Add, (sender, args) => {
						navigation.ToggleMenu ();

					})
					, true);

			}
		}
	}
}
