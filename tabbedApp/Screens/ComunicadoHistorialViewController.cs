using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

namespace tabbedApp
{
	public class ComunicadoHistorialViewController : UITableViewController
	{
		List<Comunicado> comunicados;
		string idCompañia = String.Empty;

		public ComunicadoHistorialViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Comunicados ";

			TableView = new UITableView (Rectangle.Empty, UITableViewStyle.Grouped);

			string outHouseURL = "http://187.141.3.25/LaumanAppRest/";
			XmlDocument doc = new XmlDocument ();

			ReadText ();
			//Console.WriteLine (idCompañia);
			doc.Load ( outHouseURL + "employeesPermissions.php?id="+idCompañia);

			Xml2Json xml2Json = new Xml2Json ();
			var LeerArchivoXMLConfiguracionListadoHistorialComunicados = xml2Json.XmlToJSON (doc);
			var ListaComunicados = JsonConvert.DeserializeObject<RootObjectComunicado> (LeerArchivoXMLConfiguracionListadoHistorialComunicados);

			comunicados = ListaComunicados.Comunicados.Comunicado;


			TableView.Source = new ComunicadosCustomTableSource (comunicados, this);


		}


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
				} 
			} catch (Exception ex) {
				//Console.WriteLine (ex.Message.ToString ());
			}
		}

		// Adjust for overlap of first row by status bar for for iOS 7
		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

			TableView.ContentInset = new UIEdgeInsets (this.TopLayoutGuide.Length, 0, 0, 0);
		}


	}
}

