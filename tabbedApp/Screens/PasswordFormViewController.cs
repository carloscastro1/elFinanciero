using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Dialog;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace tabbedApp
{
	partial class PasswordFormViewController : UIViewController
	{
		LoadingOverlay _loadPop;

		public PasswordFormViewController (IntPtr handle) : base (handle)
		{
		}

		public PasswordFormViewController(){
		}



		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavigationController.SetNavigationBarHidden (false, false);

			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("img/fondos/bg-login@2x.png"));
			Title = "Recuperar Password ";

			var SignInButton = UIButton.FromType (UIButtonType.RoundedRect);//tipo de boton.
			SignInButton.BackgroundColor = UIColor.FromRGB (106, 207, 250);
			SignInButton.SetTitleColor (UIColor.White, UIControlState.Normal);
			SignInButton.Frame = new RectangleF (71, 370, 187, 30); // con estas coordenadas el boton aparecera siempre centrado
			SignInButton.SetTitle ("Recuperar Contraseña", UIControlState.Normal); //se asigna el titulo del boton y el estado en el que aparecerá en la pantalla
			SignInButton.Layer.CornerRadius = 5f; // darle un apecto redondeado al boton

			SignInButton.TouchUpInside += async (object sender, EventArgs e) => {

				var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
				if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
					bounds.Size = new SizeF (bounds.Size.Height, bounds.Size.Width);
				}

				// show the loading overlay on the UI thread using the correct orientation sizing
				this._loadPop = new LoadingOverlay (bounds, "Actualizando Usuario...");
				View.AddSubview (this._loadPop);

				try {

					var usuarioInfo = await GetUsuarioInfo (txtUserName.Text, txtPassword.Text, "http://187.141.3.25/LaumanAppRest/updateUser.php"); 
					if(usuarioInfo != null)
					{
						UIAlertView alert = new UIAlertView ();
						alert.Title = "Error";
						alert.AddButton ("OK");        
						this.txtUserName.Text = null;
						this.txtPassword.Text = null;
						switch (usuarioInfo.status) {
						case 1: 
							this._loadPop.Hide ();
							alert.Title = "Exito";
							alert.Message = "Contraseña actualizada \n ";


							NavigationController.PopViewControllerAnimated(true);
							NavigationController.SetNavigationBarHidden (false, false);
							alert.Show ();
							break;
						case 0: 
							this._loadPop.Hide ();
							alert.Message = "Usuario no valido, consulte con el administrador";
							alert.Show ();
							break;
						case 3: 
							this._loadPop.Hide ();
							alert.Message = "Usuario no valido, consulte con el administrador";
							alert.Show ();
							break;   
						}
					}
				} 
				catch (Exception ex) {
					UIAlertView alert = new UIAlertView ();
					alert.Title = "Error";
					alert.AddButton ("OK");
					alert.Message = ex.Message.ToString ();
					alert.Show ();
					this._loadPop.Hide ();
				}
			};




			this.txtUserName.KeyboardType = UIKeyboardType.EmailAddress;

			this.txtUserName.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder(); 
				return true;
			};

			this.txtPassword.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder(); 
				return true;
			}; 


			View.AddSubview(SignInButton); 


		}

		public static async Task<RootObject> GetUsuarioInfo (string idUser, string password, string url)
		{
			try{
			if (string.IsNullOrEmpty (idUser)) {
				throw new ArgumentException ("Por favor escribe tu email");       
			}
			if (string.IsNullOrEmpty (password)) {
				throw new ArgumentException ("Por favor escribe tu password");
			}
			string json;

			url += "?uid=" + idUser + "" + "&" + "pwd=" + password + "";

			using (var client = new HttpClient ()) {
				json = await client.GetStringAsync (url);
			}
				return JsonConvert.DeserializeObject<RootObject> (json);
			}
			catch(Exception ex) {

				UIAlertView alert = new UIAlertView ();
				alert.Title = "Error";
				alert.AddButton ("OK");
				alert.Message = ex.Message.ToString ();
				alert.Show ();
				return null;
			}
		}
	}
}
