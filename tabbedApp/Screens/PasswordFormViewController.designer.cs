// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace tabbedApp
{
	[Register ("PasswordFormViewController")]
	partial class PasswordFormViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtUserName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (txtPassword != null) {
				txtPassword.Dispose ();
				txtPassword = null;
			}
			if (txtUserName != null) {
				txtUserName.Dispose ();
				txtUserName = null;
			}
		}
	}
}
