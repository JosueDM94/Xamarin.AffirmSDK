using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

namespace AffirmSDK.Sample.OS
{
	public partial class ViewController : UIViewController, IUITextFieldDelegate, IAffirmPrequalDelegate, IAffirmCheckoutDelegate
	{
		AffirmPromotionalButton promotionalButton;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Using AffirmPromotionalButton for first button (See more in configurPromotionalMessage)
			promotionalButton = new AffirmPromotionalButton(promoID: null, showCTA: true, pageType: AffirmPageType.Product, presentingViewController: this, frame: new CGRect(x: 0, y: 0, width: 315, height: 34));
			stackView.InsertArrangedSubview(promotionalButton, stackIndex: 0);

			// Configure Textfields
			publicKeyTextfield.Text = AffirmConfiguration.SharedInstance.PublicKey;
			configureTextField();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			NSNotificationCenter.DefaultCenter.AddObserver(aName: UIKeyboard.WillChangeFrameNotification, keyboardWillChangeFrame);
			NSNotificationCenter.DefaultCenter.AddObserver(aName: UIKeyboard.WillHideNotification, keyboardWillBeHidden);
			configurPromotionalMessage();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			NSNotificationCenter.DefaultCenter.RemoveObserver(this, aName: UIKeyboard.WillChangeFrameNotification, anObject: null);
			NSNotificationCenter.DefaultCenter.RemoveObserver(this, aName: UIKeyboard.WillHideNotification, anObject: null);
		}
		
		private void keyboardWillChangeFrame(NSNotification notification)
        {
			if (notification.UserInfo.ValueForKey(UIKeyboard.FrameEndUserInfoKey) is NSValue value)
            {
				scrollView.ContentInset = new UIEdgeInsets(top: 0, left: 0, bottom: value.CGRectValue.Height, right: 0);
            }
        }
		
		private void keyboardWillBeHidden(NSNotification notification)
        {
			scrollView.ContentInset = UIEdgeInsets.Zero;
		}

		partial void Checkout(UIButton sender)
		{
			var dollarPrice = new NSDecimalNumber(numberValue: amountTextField.Text);
			var item = new AffirmItem(name: "Affirm Test Item", SKU: "test_item", unitPrice: dollarPrice, quantity: 1, URL: new NSUrl(urlString: "http://sandbox.affirm.com/item"));
			var shipping = AffirmShippingDetail.ShippingDetailWithName(name: "Chester Cheetah", line1: "633 Folsom Street", line2: "", city: "San Francisco", state: "CA", zipCode: "94107", countryCode: "USA");

			// Checkout
			var checkout = new AffirmCheckout(items: new AffirmItem[] { item }, shipping: shipping, taxAmount: NSDecimalNumber.Zero, shippingAmount: NSDecimalNumber.Zero, discounts: null, metadata: null, financingProgram: null, orderId: "JKLMO4321");

			// Billing
			var billing = new AffirmBillingDetail(name: "Chester Cheetah", email: "testtester@test.com", phoneNumber: null, line1: "633 Folsom Street", line2: "", city: "San Francisco", state: "CA", zipCode: "94107", countryCode: "USA");
			checkout.Billing = billing;

			// CAAS
			if (caasTextfield.Text is string caas && !string.IsNullOrEmpty(caas))
			{
				checkout.Caas = caas;
			}

			var controller = AffirmCheckoutViewController.StartCheckout(checkout: checkout, @delegate: this);
			PresentViewController(controller, animated: true, completionHandler: null);
		}

		partial void ShowFailedCheckout(UIButton sender)
		{
			var dollarPrice = new NSDecimalNumber(numberValue: amountTextField.Text);
			var item = new AffirmItem(name: "Affirm Test Item", SKU: "test_item", unitPrice: dollarPrice, quantity: 1, URL: new NSUrl(urlString: "http://sandbox.affirm.com/item"));
			var shipping = AffirmShippingDetail.ShippingDetailWithName(name: "Test Tester", email: "testtester@test.com", phoneNumber: "1111111111", line1: "633 Folsom Street", line2: "", city: "San Francisco", state: "CA", zipCode: "94107", countryCode: "USA");

			// Checkout
			var checkout = AffirmCheckout.CheckoutWithItems(items: new AffirmItem[] { item }, shipping: shipping, totalAmount: dollarPrice.ToIntegerCents());

			// CAAS
			if (caasTextfield.Text is string caas && !string.IsNullOrEmpty(caas))
			{
				checkout.Caas = caas;
			}

			var controller = AffirmCheckoutViewController.StartCheckout(checkout: checkout, @delegate: this);
			PresentViewController(controller, animated: true, completionHandler: null);
		}

		partial void VCNCheckout(UIButton sender)
		{
			var dollarPrice = new NSDecimalNumber(numberValue: amountTextField.Text);
			var item = new AffirmItem(name: "Affirm Test Item", SKU: "test_item", unitPrice: dollarPrice, quantity: 1, URL: new NSUrl(urlString: "http://sandbox.affirm.com/item"));
			var shipping = AffirmShippingDetail.ShippingDetailWithName(name: "Chester Cheetah", email: null, phoneNumber: null, line1: "633 Folsom Street", line2: "", city: "San Francisco", state: "CA", zipCode: "94107", countryCode: "USA");

			// Checkout
			var checkout = AffirmCheckout.CheckoutWithItems(items: new AffirmItem[] { item }, shipping: shipping, totalAmount: dollarPrice.ToIntegerCents());


			// CAAS
			if (caasTextfield.Text is string caas && !string.IsNullOrEmpty(caas))
			{
				checkout.Caas = caas;
			}

			var controller = AffirmCheckoutViewController.StartCheckout(checkout: checkout, useVCN: true, @delegate: this);
			PresentViewController(controller, animated: true, completionHandler: null);
		}

		partial void TrackOrderConfirmation(UIButton sender)
		{
			sender.TitleLabel.Layer.BackgroundColor = sender.TintColor.CGColor;
			UIView.Animate(duration: 0.2, () =>
			{
				sender.TitleLabel.Layer.BackgroundColor = UIColor.Clear.CGColor;
			});

			var order = AffirmOrder.OrderWithStoreName(storeName: "Affirm Store", checkoutId: null, coupon: "SUMMER2018", currency: "USD", discount: NSDecimalNumber.Zero, orderId: "T12345", paymentMethod: "Visa", revenue: new NSDecimalNumber("2920"), shipping: new NSDecimalNumber("534"), shippingMethod: "Fedex", tax: new NSDecimalNumber("285"), total: new NSDecimalNumber("3739"));
			var product0 = AffirmProduct.ProductWithBrand(brand: "Affirm", category: "Apparel", coupon: "SUMMER2018", name: "Affirm T-Shirt", price: new NSDecimalNumber("730"), productId: "SKU-1234", quantity: 1, variant: "Black", currency: null);
			var product1 = AffirmProduct.ProductWithBrand(brand: "Affirm", category: "Apparel", coupon: "SUMMER2018", name: "Affirm Turtleneck Sweater", price: new NSDecimalNumber("2190"), productId: "SKU-5678", quantity: 1, variant: "Black", currency: null);

			AffirmOrderTrackerViewController.TrackOrder(order: order, products: new AffirmProduct[] { product0, product1 });

			var alertController = UIAlertController.Create(title: null, message: "Track sucessfully", preferredStyle: UIAlertControllerStyle.Alert);
			alertController.AddAction(UIAlertAction.Create(title: "OK", style: UIAlertActionStyle.Cancel, handler: null));
			PresentViewController(alertController, animated: true, completionHandler: null);
		}

		partial void ClearCookies(UIButton sender)
		{
			AffirmConfiguration.DeleteAffirmCookies();
			configurPromotionalMessage();

			var alertController = UIAlertController.Create(title: null, message: "Clear sucessfully", preferredStyle: UIAlertControllerStyle.Alert);
			alertController.AddAction(UIAlertAction.Create(title: "OK", style: UIAlertActionStyle.Cancel, handler: null));
			PresentViewController(alertController, animated: true, completionHandler: null);
		}

		private void configureTextField()
		{
			var textFields = new List<UITextField>() { publicKeyTextfield, amountTextField, promoIDTextField};
			foreach (var textField in textFields) {
				var toolbar = new UIToolbar();
				var flexibleItem = new UIBarButtonItem(systemItem: UIBarButtonSystemItem.FlexibleSpace, target: null, action: null);
				var doneItem = new UIBarButtonItem(systemItem: UIBarButtonSystemItem.Done, target: textField, action: new ObjCRuntime.Selector("resignFirstResponder"));
				toolbar.Items = new UIBarButtonItem[] { flexibleItem, doneItem };
				toolbar.SizeToFit();
				textField.InputAccessoryView = toolbar;
			}
		}

		private void configurPromotionalMessage()
		{
			var amountText = amountTextField.Text;
			promotionalButton.ConfigureWithAmount(amount: new NSDecimalNumber(amountText),
												  affirmLogoType: AffirmLogoType.Name,
												  affirmColor: AffirmColorType.BlueBlack,
												  font: UIFont.ItalicSystemFontOfSize(size: 15),
												  textColor: UIColor.Gray);
		}

		[Export("textFieldShouldReturn:")]
		public bool ShouldReturn(UITextField textField)
		{
			textField.ResignFirstResponder();
			return true;
		}


		[Export("textFieldDidEndEditing:")]
		public void EditingEnded(UITextField textField)
		{
			var text = textField.Text;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}

			if(textField == publicKeyTextfield)
			{
				AffirmConfiguration.SharedInstance.ConfigureWithPublicKey(publicKey: text, environment: AffirmEnvironment.Sandbox);
			}
			else if( textField == promoIDTextField)
			{
				promotionalButton.PromoID = text;
			}
			configurPromotionalMessage();
		}

		public void DidFailWithError(AffirmBaseWebViewController webViewController, NSError error)
		{
			Console.WriteLine($"Prequal failed with error: {error.LocalizedDescription}");
			if(webViewController is not null)
			{
				var alertController = UIAlertController.Create(title: "Error", message: error.LocalizedDescription, preferredStyle: UIAlertControllerStyle.Alert);
				alertController.AddAction(UIAlertAction.Create(title: "OK", style: UIAlertActionStyle.Default, handler: (action) =>
				{
					webViewController.DismissViewController(animated: true, completionHandler: null);
				}));
				webViewController.PresentViewController(alertController, animated: true, completionHandler: null);
			}
		}

		public void Checkout(AffirmCheckoutViewController checkoutViewController, string checkoutToken)
		{
			resultLabel.Text = $"Received token:\n {checkoutToken}";
			checkoutViewController.DismissViewController(animated: true, completionHandler: null);
		}

		public void VcnCheckout(UIViewController checkoutViewController, AffirmCreditCard creditCard)
		{
			if(creditCard.CardholderName is string cardholderName &&
				creditCard.Number is string number &&
				creditCard.Cvv is string cvv &&
				creditCard.Expiration is string expiration)
            {
				resultLabel.Text = $"Received credit card:\n" +
                    $"credit card id: {creditCard.CreditCardId}\n" +
                    $"checkout token: {creditCard.CheckoutToken}\n" +
                    $"card holder name: {cardholderName}\n" +
                    $"number:{number}\n" +
                    $"cvv: {cvv}\n" +
                    $"expiration: {expiration}\n" +
                    $"callback id: {creditCard.CallbackId}";
			}
			checkoutViewController.DismissViewController(animated: true, completionHandler: null);
		}

		public void CheckoutCancelled(UIViewController checkoutViewController)
		{
			Console.WriteLine("Checkout was cancelled");
			checkoutViewController.DismissViewController(animated: true, completionHandler: null);
		}

		public void CheckoutCancelled(AffirmCheckoutViewController checkoutViewController, AffirmReasonCode reasonCode)
		{
			Console.WriteLine($"Checkout canceled with a reason: {reasonCode.Reason}");
			resultLabel.Text = $"Checkout canceled \n reason: {reasonCode.Reason}, \n checkout_token: {reasonCode.CheckoutToken}";
			checkoutViewController.DismissViewController(animated: true, completionHandler: null);
		}

		public void Checkout(AffirmCheckoutViewController checkoutViewController, NSError error)
		{
			Console.WriteLine($"Checkout failed with error: {error.LocalizedDescription}");
			var alertController = UIAlertController.Create(title: "Error", message: error.LocalizedDescription, preferredStyle: UIAlertControllerStyle.Alert);
			alertController.AddAction(UIAlertAction.Create(title: "OK", style: UIAlertActionStyle.Default, handler: (action) =>
			{
				checkoutViewController.DismissViewController(animated: true, completionHandler: null);
			}));
			checkoutViewController.PresentViewController(alertController, animated: true, completionHandler: null);
		}
	}
}
