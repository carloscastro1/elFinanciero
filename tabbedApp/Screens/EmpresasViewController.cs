using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;


namespace tabbedApp
{
	public class EmpresasViewController : UITableViewController
	{
		List<EmpresasModel> empresas;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();



			Title = "Empresas";

			empresas = EmpresasData.InformacionEmpresas;
			TableView.Source = new EmpresasCustomTableSource (empresas, this);

		}
	}
}

