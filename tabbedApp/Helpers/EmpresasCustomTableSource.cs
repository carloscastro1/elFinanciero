// EmpresasCustomTableSource.cs
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
	public class EmpresasCustomTableSource   : UITableViewSource
	{

		static readonly string sessionCellId = "SessionCell";
		List<EmpresasModel> data;
		IGrouping<string, EmpresasModel>[] grouping;
		// sub-group of speakers in each index
		UITableViewController controller;

		public EmpresasCustomTableSource (List<EmpresasModel> sessions, UITableViewController tvc)
		{
			data = sessions;
			grouping = GetSessionsGroupedByUrl ();
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
			return grouping [section].ElementAt (0).Titulo;
		}


		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var sessionGroup = grouping [indexPath.Section];
			var session = sessionGroup.ElementAt (indexPath.Row);
			controller.NavigationController.PushViewController (new EmpresaViewController (session), true);
			tableView.DeselectRow (indexPath, true);
			//tableView.SeparatorColor = UIColor.Blue;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (sessionCellId);
			var sessionGroup = grouping [indexPath.Section];
			var session = sessionGroup.ElementAt (indexPath.Row);
			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, sessionCellId);	

			cell.TextLabel.Text = session.NombreEmpresa;
			//cell.DetailTextLabel.Text = session.origenComunicado;
			cell.ImageView.Image = UIImage.FromBundle (session.imagenLogoEmpresa);
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			tableView.SeparatorInset.InsetRect (new RectangleF (4, 4, 150, 2));

			/*
			if (indexPath.Section % 2 == 0) {
				//Console.WriteLine ("Color " + indexPath.Section % 2);
				cell.BackgroundColor = UIColor.FromRGB (49, 114, 181);
				cell.TextLabel.TextColor = UIColor.White;
			}
			*/
			return cell;
		}




		// This method groups the Sessions by date
		IGrouping<string, EmpresasModel>[] GetSessionsGroupedByUrl ()
		{
			var sessionsGrouped = (from s in data
			                       group s by s.NombreEmpresa into g
			                       select g).ToArray ();

			return sessionsGrouped;
		}


	}
}

