
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace tabbedApp
{
	public partial class LoginViewController : UIViewController
	{
		public LoginViewController () : base ("LoginViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("img/fondos/bg-login@2x.png"));

			/*
			var mainView = new UIImageView (new RectangleF (70, 80, 180, 180)){
				Image = UIImage.FromFile("img/LaumanGroup.jpg")
			};
			*/
			float w = View.Bounds.Width; //medir el total del tamaño de la ventana
			var SignInButton = UIButton.FromType (UIButtonType.RoundedRect);//tipo de boton.
			SignInButton.BackgroundColor = UIColor.FromRGB (106, 207, 250);
			SignInButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			SignInButton.Frame = new RectangleF (71, 370, 187, 30); // con estas coordenadas el boton aparecera siempre centrado
			SignInButton.SetTitle ("Entrar", UIControlState.Normal); //se asigna el titulo del boton y el estado en el que aparecerá en la pantalla
			SignInButton.Layer.CornerRadius = 5f; // darle un apecto redondeado al boton



			var ForgetPasswordButon = UIButton.FromType (UIButtonType.RoundedRect);
			//ForgetPasswordButon.BackgroundColor = UIColor.FromRGB(106,207,250);
			ForgetPasswordButon.SetTitleColor (UIColor.White, UIControlState.Disabled);
			ForgetPasswordButon.SetTitle ("Olvidé mi contraseña", UIControlState.Normal);
			ForgetPasswordButon.Frame = new RectangleF (71, 520, 187, 30);
			ForgetPasswordButon.Layer.CornerRadius = 5f;

			/*
			this.txtUserName.KeyboardType = UIKeyboardType.EmailAddress;

			this.txtUserName.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder(); 
				return true;
			};

			this.txtPassword.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder(); 
				return true;
			};

			*/
			SignInButton.TouchUpInside += (object sender, EventArgs e) => {

				//UIAlertView alert = new UIAlertView ("Bienvenido", txtUserName.Text, null, "OK", null);
				//alert.Show();
				//this.PerformSegue ("home", this);
				this.NavigationController.PushViewController (new EmpresasTabController (), true);

			};

			ForgetPasswordButon.TouchUpInside += (object sender, EventArgs e) => {
				this.NavigationController.PushViewController (new PasswordFormViewController (), true);
				//this.PresentViewController(new PasswordFormViewController(), true,  null);
			};

			#region
			View.AddSubviews (new UIView[] { SignInButton, ForgetPasswordButon }); // multiple add



			#endregion
		}
	}
}

