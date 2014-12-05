using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Net.Http;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;


namespace tabbedApp
{
	public partial class tabbedAppViewController : UIViewController
	{

		LoadingOverlay _loadPop;

		public tabbedAppViewController (IntPtr handle) : base (handle)
		{
		}

		public tabbedAppViewController ()
		{
		}

		public override void DidReceiveMemoryWarning ()
		{

			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle





		public override void ViewDidLoad ()
		{


			base.ViewDidLoad ();

			// Determine the correct size to start the overlay (depending on device orientation)

		
			View.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("img/fondos/bg-login@2x.png"));

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

			this.txtUserName.KeyboardType = UIKeyboardType.EmailAddress;

			this.txtUserName.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder (); 
				return true;
			};

			this.txtPassword.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder (); 
				return true;
			};


			SignInButton.TouchUpInside += async (object sender, EventArgs e) => {


				var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
				if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) {
					bounds.Size = new SizeF (bounds.Size.Height, bounds.Size.Width);
				}

				// show the loading overlay on the UI thread using the correct orientation sizing
				this._loadPop = new LoadingOverlay (bounds, "Buscando Usuario...");

				View.AddSubview (this._loadPop);

				// Load XML as an array

				//string[] arr = XDocument.Load ("http://10.1.4.154/ef/rest/configuracion.xml").Descendants ("configuracion")
				string[] arr = XDocument.Load ("http://10.10.28.38/LaumanAppRest/configuracion.xml").Descendants ("configuracion")
				.Select (element => element.Value).ToArray ();

				string url = arr [0].ToString ();


				//10.10.29.75
				url += "info.php";


				try {

					var usuarioInfo = await GetUsuarioInfo (txtUserName.Text, txtPassword.Text, url); 

					UIAlertView alert = new UIAlertView ();
					alert.Title = "Error";
					alert.AddButton ("OK");        

					switch (usuarioInfo.status) {
					case 1: 
						this._loadPop.Hide ();
						CreateDirectory ();
						CreateUserDataFile (usuarioInfo);
						this.PerformSegue ("home", this);  
						break;
					case 0: 
						this._loadPop.Hide ();
						alert.Message = "Usuario o contraseña incorrecta";
						alert.Show ();
						break;
					case 3: 
						this._loadPop.Hide ();
						alert.Message = "Usuario o contraseña incorrecta";
						alert.Show ();
						break;   
					}
				} catch (Exception ex) {
					UIAlertView alert = new UIAlertView ();
					alert.Title = "Error";
					alert.AddButton ("OK");
					alert.Message = ex.Message.ToString ();
					alert.Show ();
					this._loadPop.Hide ();
				}

			};

			ForgetPasswordButon.TouchUpInside += (object sender, EventArgs e) => {
				this.NavigationController.PushViewController (new PasswordFormViewController (), true);
				//this.PresentViewController(new PasswordFormViewController(), true,  null);
			};

			#region
			View.AddSubviews (new UIView[] { SignInButton, ForgetPasswordButon }); // multiple add
			#endregion
		}

		public static void CreateDirectory ()
		{
			var documents =
				Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var directoryname = Path.Combine (documents, "UserData");

			if (!File.Exists (directoryname)) {
				Directory.CreateDirectory (directoryname);

				// TODO: remove console outputs
				// Output to app console
				Console.WriteLine (
					"A directory was created."
					+ directoryname);
			}
		}

		public static void CreateUserDataFile (RootObject userInfo)
		{
			var documents =
				Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine (documents, "UserData/user.txt");  
			File.WriteAllText (filename, userInfo.info [0].idGerencia.ToString ());
		}

		public static async Task<RootObject> GetUsuarioInfo (string idUser, string password, string url)
		{

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



		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}




		#endregion
	}
}

