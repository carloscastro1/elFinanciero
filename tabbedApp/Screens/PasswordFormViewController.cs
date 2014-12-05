using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Dialog;

namespace tabbedApp
{
	partial class PasswordFormViewController : UIViewController
	{
		public PasswordFormViewController (IntPtr handle) : base (handle)
		{
		}

		public PasswordFormViewController(){
		}

		UIWindow window;

		EntryElement login;
		//EntryElement password;
		BooleanElement bol;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			/*
			UITableView faqTable = new UITableView ();
			faqTable.Frame = new RectangleF (30, 0, 320, 531);

			List<TableItem> tableItems = new List<TableItem>();

			tableItems.Add (new TableItem ("60", 20) { SubHeading = "laengde", ImageName = "Swipe_Left2.png" });
			tableItems.Add (new TableItem ("65", 20) { SubHeading = "laengde", ImageName = "" });
			faqTable.Source = new TableSource(this, tableItems);

			faqTable.ReloadData();
			View.Add (faqTable);
			*/

			window = new UIWindow (UIScreen.MainScreen.Bounds);

			window.RootViewController = new DialogViewController (new RootElement ("Login") {
				new Section ("Credentials"){
					(login = new EntryElement ("Login", "escribe tu email", "")),
					//(password = new EntryElement ("Password", "", "", true)),
					(bol = new BooleanElement("Recibir notificacion?", false))

				},
				new Section () {
					new StringElement ("tap para recuperar Contraseña", delegate{ 
						Console.WriteLine ("User {0} log-in", login.Value); 
						Console.WriteLine ("Boolean controller: {0}", bol.Value.ToString ());
					})   
				}
			});
			window.MakeKeyAndVisible ();
			View.Add(window);  

		}
	}
}
