using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace tabbedApp
{
	public class ComunicadoHistorialViewController : UITableViewController
	{
		List<ComunicadosAnterioresModel> comunicados;

		public ComunicadoHistorialViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Comunicados ";

			TableView = new UITableView (Rectangle.Empty, UITableViewStyle.Grouped);
			comunicados = ComunicadosData.historialComunicados;
			TableView.Source = new ComunicadosCustomTableSource (comunicados, this);


		}

		// Adjust for overlap of first row by status bar for for iOS 7
		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

			TableView.ContentInset = new UIEdgeInsets (this.TopLayoutGuide.Length, 0, 0, 0);
		}


	}
}

