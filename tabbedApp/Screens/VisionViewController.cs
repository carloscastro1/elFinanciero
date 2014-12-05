using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;


namespace tabbedApp
{
	public class VisionViewController: UIViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Extras";
			//View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("img/fondos/bg-mision-vision@2x.png"));
			this.View.InsertSubview (new UIImageView (UIImage.FromBundle ("img/fondos/bg-mision-vision@2x.png")), 0);

		}
	}
}

