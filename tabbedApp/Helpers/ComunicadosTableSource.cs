using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;


namespace tabbedApp
{
	public class ComunicadosTableSource  : UITableViewSource
	{
		static readonly string ComunicadoCellId = "ComunicadosCell";

		List<ComunicadosAnterioresModel> data;
		UITableViewController controller;

		public ComunicadosTableSource(List<ComunicadosAnterioresModel> comunicados, UITableViewController tvc)
		{
			data = comunicados;
			controller = tvc;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return data.Count;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var comunicado = data [indexPath.Row];

			controller.NavigationController.PushViewController (new ComnunicadoDetalleHistorial (comunicado), true);

			tableView.DeselectRow (indexPath, true);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (ComunicadoCellId);

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, ComunicadoCellId);
			}
			var documentDesc = data [indexPath.Row];

			cell.TextLabel.Text = documentDesc.TituloComunicado;
			cell.DetailTextLabel.Text = documentDesc.origenComunicado;

			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			return cell;
		}
	}
}

