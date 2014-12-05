using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace tabbedApp
{
	public class EmpresasTableSource  : UITableViewSource
	{
		static readonly string EmpresaCellId = "EmpresaCell";

		List<EmpresasModel> data;
		UITableViewController controller;


		public EmpresasTableSource (List<EmpresasModel> empresas, UITableViewController tvc)
		{
			data = empresas;
			controller = tvc;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return data.Count;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var empresa = data [indexPath.Row];

			controller.NavigationController.PushViewController (new EmpresaViewController (empresa), true);

			tableView.DeselectRow (indexPath, true);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (EmpresaCellId);

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, EmpresaCellId);
			}
			var EmpresaDescription = data [indexPath.Row];

			cell.TextLabel.Text = EmpresaDescription.NombreEmpresa;
			cell.DetailTextLabel.Text = EmpresaDescription.Titulo;
			cell.ImageView.Image = UIImage.FromBundle (EmpresaDescription.imagenLogoEmpresa);

			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			if (indexPath.Row % 2 == 0) {
				//Console.WriteLine ("Color " + indexPath.Section % 2);
				cell.BackgroundColor = UIColor.FromRGB (49, 114, 181);
				cell.TextLabel.TextColor = UIColor.White;
			}

			return cell;
		}
	}
}

