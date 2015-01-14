using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace tabbedApp
{
	partial class EmpresasTabController : UITabBarController
	{
		public EmpresasTabController (IntPtr handle) : base (handle)
		{
		}

		public EmpresasTabController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("img/fondos/bg-mision-vision@2x.png"));

			var lectorComunicados = new UINavigationController (new HomeViewController ());
			lectorComunicados.TabBarItem =	new UITabBarItem ("Home", UIImage.FromBundle ("img/tabAssets/icon-home.png"), 0);
			//lectorComunicados.TabBarItem.BadgeValue = "2";

			var listadoEmpresas = new UINavigationController (new MisionViewController ());
			// Customize how this controller is represented when in a tab.
			listadoEmpresas.TabBarItem = new UITabBarItem ("Lauman", UIImage.FromBundle ("img/tabAssets/icon-empresasb.png"), 1);


			var Empresas = new UINavigationController (new  EmpresasViewController ());
			// Customize how this controller is represented when in a tab.
			Empresas.TabBarItem = new UITabBarItem ("Empresas", UIImage.FromBundle ("img/tabAssets/icon-empresas.png.png"), 2);

			var ComunicadosAnteriores = new UINavigationController (new ComunicadoHistorialViewController ());
			ComunicadosAnteriores.TabBarItem = new UITabBarItem ("Comunicados", UIImage.FromBundle ("img/tabAssets/icon-docs.png"), 3);

			//var Extras = new UINavigationController (new VisionViewController ());
			// Customize how this controller is represented when in a tab.
			//Extras.TabBarItem = new UITabBarItem ("Extras", UIImage.FromBundle ("img/tabAssets/icon-opciones.png.png"), 4);




			ViewControllers = new UIViewController[] {
				lectorComunicados,
				listadoEmpresas,
				Empresas,
				ComunicadosAnteriores,
				//Extras

			};
			// Select the first tab.
			SelectedIndex = 0;
		}
	}
}
