// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AffirmSDK.Sample.OS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UITextField amountTextField { get; set; }

		[Outlet]
		UIKit.UITextField caasTextfield { get; set; }

		[Outlet]
		UIKit.UITextField promoIDTextField { get; set; }

		[Outlet]
		UIKit.UITextField publicKeyTextfield { get; set; }

		[Outlet]
		UIKit.UILabel resultLabel { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollView { get; set; }

		[Outlet]
		UIKit.UIStackView stackView { get; set; }

		[Action ("Checkout:")]
		partial void Checkout (UIKit.UIButton sender);

		[Action ("ClearCookies:")]
		partial void ClearCookies (UIKit.UIButton sender);

		[Action ("ShowFailedCheckout:")]
		partial void ShowFailedCheckout (UIKit.UIButton sender);

		[Action ("TrackOrderConfirmation:")]
		partial void TrackOrderConfirmation (UIKit.UIButton sender);

		[Action ("VCNCheckout:")]
		partial void VCNCheckout (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (scrollView != null) {
				scrollView.Dispose ();
				scrollView = null;
			}

			if (stackView != null) {
				stackView.Dispose ();
				stackView = null;
			}

			if (amountTextField != null) {
				amountTextField.Dispose ();
				amountTextField = null;
			}

			if (promoIDTextField != null) {
				promoIDTextField.Dispose ();
				promoIDTextField = null;
			}

			if (publicKeyTextfield != null) {
				publicKeyTextfield.Dispose ();
				publicKeyTextfield = null;
			}

			if (caasTextfield != null) {
				caasTextfield.Dispose ();
				caasTextfield = null;
			}

			if (resultLabel != null) {
				resultLabel.Dispose ();
				resultLabel = null;
			}
		}
	}
}
