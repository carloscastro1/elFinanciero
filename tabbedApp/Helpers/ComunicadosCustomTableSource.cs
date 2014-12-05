// ComunicadosCustomTableSource.cs
//
//  Author:
//       Carlos Jonathan Castro Méndez <ccastro@elfinanciero.com.mx>
//
//  Copyright (c) 2014 Grupo Lauman
//
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace tabbedApp
{
	public class ComunicadosCustomTableSource  : UITableViewSource
	{
		static readonly string sessionCellId = "SessionCell";
		List<ComunicadosAnterioresModel> data;
		IGrouping<int, ComunicadosAnterioresModel>[] grouping;
		// sub-group of speakers in each index
		UITableViewController controller;

		public ComunicadosCustomTableSource (List<ComunicadosAnterioresModel> sessions, UITableViewController tvc)
		{
			data = sessions;
			grouping = GetSessionsGroupedByDate ();
			controller = tvc;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return grouping [section].Count ();
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return grouping.Count ();
		}

		// TODO: Step 3b: uncomment to add a title to the header over each section
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return grouping [section].ElementAt (0).fechaDeEmision.Date.ToString ("dd MMM yyyy");
		}


		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var sessionGroup = grouping [indexPath.Section];
			var session = sessionGroup.ElementAt (indexPath.Row);

			controller.NavigationController.PushViewController (new ComnunicadoDetalleHistorial (session), true);


			tableView.DeselectRow (indexPath, true);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (sessionCellId);

			var sessionGroup = grouping [indexPath.Section];
			var session = sessionGroup.ElementAt (indexPath.Row);

			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, sessionCellId);	

			cell.TextLabel.Text = session.TituloComunicado;
			cell.DetailTextLabel.Text = session.origenComunicado;

			cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;

			return cell;
		}


		// This method groups the Sessions by date
		IGrouping<int, ComunicadosAnterioresModel>[] GetSessionsGroupedByDate ()
		{
			var sessionsGrouped = (from s in data
			                       group s by s.fechaDeEmision.Day into g
			                       select g).ToArray ();

			return sessionsGrouped;
		}


	}
}

