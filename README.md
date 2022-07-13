# Xamarin.AffirmSDK
This is a set of Xamarin bindings of Affirm SDK for iOS and Android

# Installation

Install by the NuGet Gallery, using the identifier "[Xamarin.AffirmSDK](https://www.nuget.org/packages/Xamarin.AffirmSDK)".

# Example

A demo app that integrates Affirm is included in the repo. You may clone the [GitHub repository](https://github.com/JosueDM94/Xamarin.AffirmSDK) into a new Visual Studio project folder and run the Examples project.

Changelog
==============

All notable changes to this project will be documented in [iOS changelog document](https://github.com/Affirm/affirm-merchant-sdk-ios/blob/master/CHANGELOG.md) and [Android changelog document](https://github.com/Affirm/affirm-merchant-sdk-android/blob/master/CHANGELOG.md).

# Affirm Android SDK

Affirm Android SDK allows you to offer Affirm in your own app.

Usage Overview
==================

Before you can start the initialization of Affirm SDK, you must first set the AffirmSDK with your `public API key` from your sandbox [Merchant Dashboard](https://sandbox.affirm.com/dashboard). You must set this key as follows:

```cs
Affirm.Initialize(new Affirm.Configuration.Builder("public key")
        .SetEnvironment(Affirm.Environment.Sandbox)
        .SetName("merchant name")
        .SetReceiveReasonCodes("true")
        .SetLogLevel(Affirm.LogLevelDebug)
        .SetCheckoutRequestCode(8001)
        .SetVcnCheckoutRequestCode(8002)
        .SetPrequalRequestCode(8003)
        .SetLocation(Affirm.Location.Us)  // "CA" for Canadian, "US" for American (If not set, default use US)
        .Build());
```
- `environment` can be set to `Affirm.Environment.Sandbox` for test.
- To prevent conflicts, you can set a custom affirm's request code.

You can also set `public key` and `merchant name` after the `Initialize` method
```cs
    Affirm.SetMerchantName("merchant name")

    Affirm.SetPublicKey("public key")

    Affirm.SetPublicKeyAndMerchantName("public key", "merchant name")
```

## Checkout

Checkout creation is the process in which a customer uses Affirm to pay for a purchase in your app. You can create a checkout object and launch the affirm checkout using the Checkout function


```cs
Checkout checkout = Checkout.InvokeBuilder()
        .SetOrderId("order id")
        .SetItems(items)
        .SetBilling(shipping)
        .SetShipping(shipping)
        .SetShippingAmount(BigDecimal.ValueOf(0.0))
        .SetTaxAmount(BigDecimal.ValueOf(100.0))
        .SetTotal(BigDecimal.ValueOf(1100.0))
        .SetMetadata(metadata)
        .Build();

Affirm.StartCheckout(this, checkout, false);
//It is recommended that you round the total in the checkout request to two decimal places. Affirm SDK converts the float total to integer cents before initiating the checkout, so may round up or down depending on the decimal places. Ensure that the rounding in your app uses the same calculation across your other backend systems, otherwise, it may cause an error of 1 cent or more in the total validation on your end. 
```

- `checkout` object contains details about the order 
- `useVCN` (boolean) determines whether the checkout flow should use virtual card network to handle the checkout
    - if `true`, it will return `card info` from `VcnCheckoutCallbacks`. Be sure to override onActivityResult, then call the `handleVcnCheckoutData` method.  Please check out the example project for more information.
    ```cs
    protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
    {
        if (Affirm.HandleVcnCheckoutData(this, requestCode, (int)resultCode, data)) 
        {
		    return;
        }
		base.OnActivityResult(requestCode, resultCode, data);
    }
    ```
    
    ```cs
    public void OnAffirmVcnCheckoutCancelledReason(VcnReason vcnReason)
    {
	    Toast.MakeText(this, $"Vcn Checkout Cancelled: {vcnReason}", ToastLength.Long).Show();
    }
    
    public void OnAffirmVcnCheckoutError(string message)
    {
	    Toast.MakeText(this, $"Vcn Checkout Error: {message}", ToastLength.Long).Show();
    }
    
    public void OnAffirmVcnCheckoutSuccess(CardDetails cardDetails)
    {
	    Toast.MakeText(this, $"Vcn Checkout Card: {cardDetails}", ToastLength.Long).Show();
    }
    ```
    
    - if `false`, it will return checkout `token` from `CheckoutCallbacks`. Be sure to override onActivityResult, then call the `handleCheckoutData` method.  Please refer to the example project for more information.
    ```cs    
    protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
    {
        if (Affirm.HandleCheckoutData(this, requestCode, (int)resultCode, data)) 
        {
            return;
        }
		base.OnActivityResult(requestCode, resultCode, data);
    }
    ```

    ```cs
    public void OnAffirmCheckoutSuccess(string token)
    {
	    Toast.MakeText(this, $"Checkout token: {token}", ToastLength.Long).Show();
    }

    public void OnAffirmCheckoutCancelled()
    {
	    Toast.MakeText(this, "Checkout Cancelled", ToastLength.Long).Show();
    }
    
    public void OnAffirmCheckoutError(string message)
    {
	    Toast.MakeText(this, $"Checkout Error: {message}", ToastLength.Long).Show();
    }
    ```
### Charge authorization

Once the checkout has been successfully confirmed by the user, the AffirmCheckoutDelegate object will receive a checkout token. This token should be forwarded to your server, which should then use the token to authorize a charge on the user's account. For more details about the server integration, see our [API documentation](https://docs.affirm.com/Integrate_Affirm/Direct_API#3._Authorize_the_charge).

Note - For VCN Checkout, all actions should be done using your existing payment gateway and debit card processor using the virtual card number returned after a successful checkout.

## Promotional Messaging

Affirm promotional messaging components—payment messaging and educational modals—show customers how they can use Affirm to finance their purchases. Promos consist of promotional messaging, which appears directly in your app, and a modal, which which offers users an ability to prequalify.

### Show promotional message with `AffirmPromotionButton`

To display promotional messaging, SDK provides a `AffirmPromotionButton` class. `AffirmPromotionButton` is implemented as follows:

```xml
 <com.affirm.android.AffirmPromotionButton
    android:id="@+id/promo"
    android:layout_width="wrap_content"
    android:layout_height="wrap_content"
    android:layout_below="@id/price"
    android:layout_centerHorizontal="true"
    android:layout_marginTop="20dp"
    app:htmlStyling="false"
    app:affirmTextFont="@font/apercu_bold"
    app:affirmTextColor="@android:color/darker_gray"
    app:affirmTextSize="16sp"
    app:affirmColor="AffirmColorTypeBlue"
    app:affirmLogoType="AffirmDisplayTypeLogo"/>
```
or
```cs
// Option1 - Load via findViewById
AffirmPromotionButton affirmPromotionButton1 = FindViewById<AffirmPromotionButton>(Resource.Id.promo);
Affirm.ConfigureWithAmount(affirmPromotionButton1, null, PromoPageType.Product, BigDecimal.ValueOf(1100.0), true);
```
or
```cs
// Option2 - Initialize by new
AffirmPromotionButton affirmPromotionButton2 = new AffirmPromotionButton(this);
FindViewById<FrameLayout>(Resource.Id.promo_container).AddView(affirmPromotionButton2);
Affirm.ConfigureWithAmount(affirmPromotionButton2, null, PromoPageType.Product, BigDecimal.ValueOf(1100.0), true);
```

Configure the style of the AffirmPromotionButton

- `ConfigWithLocalStyling` that will use the local styles. 
```cs
// You can custom with the AffirmColor, AffirmLogoType, Typeface, TextSize, TextColor
affirmPromotionButton2.ConfigWithLocalStyling(
				AffirmColor.AffirmColorTypeBlue,
				AffirmLogoType.AffirmDisplayTypeLogo,
                ResourcesCompat.GetFont(this, Resource.Font.apercu_bold),
				Android.Resource.Color.darker_gray,
				Resource.Dimension.affirm_promotion_size);
```

- `ConfigWithHtmlStyling` will use html style from Affirm server. 

You can add fonts by following the steps below, so you can customize the fonts in html

1. Add a font file in the /res/font/ directory. Such as [lacquer_regular.ttf](/samples-java/src/main/res/font/lacquer_regular.ttf).

2. Add a declaration for the font file. You can check the detail in [typeface](/samples-java/src/main/assets/typeface)

3. Use the font in the css file. You can check the detail in [remote_promo.css](/samples-java/src/main/assets/remote_promo.css).

```cs
// If you want to custom the style of promo message, should pass the local or remote url and the file of typeface declaration
affirmPromotionButton2.ConfigWithHtmlStyling("file:///android_asset/remote_promo.css", typefaceDeclaration);
```


Tapping on the `AffirmPromotionButton` automatically start prequalification flow.

(Optional) If you want to handle errors, override onActivityResult so that affirm can handle the result.

```cs
protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
{
    if (Affirm.HandlePrequalData(this, requestCode, (int)resultCode, data)) 
    {
        return;
    }
    base.OnActivityResult(requestCode, resultCode, data);
}
```

```cs
public void OnAffirmPrequalError(string message)
{
    Toast.MakeText(this, $"Prequal Error: {message}", ToastLength.Long).Show();
}
```

### Fetch promotional message, then display it with your own `TextView`.
- You can get promotional message via `fetchPromotion`, a `SpannableString` object is returned after the request is successful
- `onPromotionClick` This method handle events that click on the promotional message
```cs
    TextView promotionTextView = FindViewById<TextView>(Resource.Id.promotionTextView);
    Affirm.PromoRequestData requestData = new Affirm.PromoRequestData.Builder(Price, true)
        .SetPageType(null)
		.Build();
			
    promoRequest = Affirm.FetchPromotion(requestData, promotionTextView.TextSize, this, this);

    public void OnSuccess(SpannableString spannableString, bool showPrequal)
    {
		promotionTextView.TextFormatted = spannableString;
	    promotionTextView.Click += delegate { Affirm.OnPromotionClick(this, requestData, showPrequal); };
	}

	public void OnFailure(AffirmException exception)
    {
	    Toast.MakeText(this, $"Failed to get promo message, reason: {exception.Message}", ToastLength.Short).Show();
    }
```

- Call `create` method will initiate the request, and call `cancel` method to cancel the request.
```cs
    protected override void OnStart()
    {
	    base.OnStart();
		promoRequest?.Create();
    }

	protected override void OnStop()
	{
		promoRequest?.Cancel();
		base.OnStop();
	}
```

## Track Order Confirmed
The trackOrderConfirmed event triggers when a customer completes their purchase. SDK provides `AffirmTrack` object to trigger the tracking.

```cs
AffirmTrack affirmTrack = AffirmTrack.InvokeBuilder()
    .SetAffirmTrackOrder(affirmTrackOrder)
    .SetAffirmTrackProducts(affirmTrackProducts)
    .Build();

Affirm.TrackOrderConfirmed(this, TrackModel());
```

## Fragment supports
We also support using fragment directly, only need to pass a ViewGroup id, we will put the `AffirmFragment` in this specified view.

- Checkout
```cs
    // In your activity/fragment, you need to implement Affirm.CheckoutCallbacks
    Affirm.StartCheckout(this, Resource.Id.container, CheckoutModel(), null, 10, false);

    // - Affirm.CheckoutCallbacks
    public void OnAffirmCheckoutSuccess(string token)
    {
	    Toast.MakeText(this, $"Checkout token: {token}", ToastLength.Long).Show();
    }

    public void OnAffirmCheckoutCancelled()
    {
	    Toast.MakeText(this, "Checkout Cancelled", ToastLength.Long).Show();
    }
    
    public void OnAffirmCheckoutError(string message)
    {
	    Toast.MakeText(this, $"Checkout Error: {message}", ToastLength.Long).Show();
    }
```

- VCN checkout
```cs
    // In your activity/fragment, you need to implement Affirm.VcnCheckoutCallbacks
    Affirm.StartCheckout(this, R.id.container, CheckoutModel(), null, 10, true);

    // - Affirm.VcnCheckoutCallbacks
    public void OnAffirmVcnCheckoutCancelled()
    {
	    Toast.MakeText(this, "Vcn Checkout Cancelled", ToastLength.Long).Show();
    }

    public void OnAffirmVcnCheckoutCancelledReason(VcnReason vcnReason)
    {
	    Toast.MakeText(this, $"Vcn Checkout Cancelled: {vcnReason}", ToastLength.Long).Show();
    }
    
    public void OnAffirmVcnCheckoutError(string message)
    {
	    Toast.MakeText(this, $"Vcn Checkout Error: {message}", ToastLength.Long).Show();
    }
    
    public void OnAffirmVcnCheckoutSuccess(CardDetails cardDetails)
    {
	    Toast.MakeText(this, $"Vcn Checkout Card: {cardDetails}", ToastLength.Long).Show();
    }
```

- Promotion
```cs
    AffirmPromotionButton affirmPromotionButton = FindViewById<AffirmPromotionButton>(Resource.Id.promo);
    Affirm.ConfigureWithAmount(this, Resource.Id.container, affirmPromotionButton, null, PromoPageType.Product, Price, true, null);
```

- Site modal
```cs
    // In your activity/fragment, you need to implement Affirm.PrequalCallbacks
    Affirm.ShowSiteModal(this, Resource.Id.container, null, "5LNMQ33SEUYHLNUC");

    public void OnAffirmPrequalError(string message)
    {
        Toast.MakeText(this, $"Prequal Error: {message}", ToastLength.Long).Show();
    }
```
- Product modal
```cs
    // In your activity/fragment, you need to implement Affirm.PrequalCallbacks
    Affirm.showProductModal(this, Resource.Id.container, Price, null, PromoPageType.Product, null)

   public void OnAffirmPrequalError(string message)
    {
        Toast.MakeText(this, $"Prequal Error: {message}", ToastLength.Long).Show();
    }
```

- Since there is no callback, it will return success after 10 seconds timeout
- We will replace using the HTTP API after the API is done

# Affirm iOS SDK

The Affirm iOS SDK allows you to offer Affirm in your own app.

Usage Overview
==============

An Affirm integration consists of two components: checkout and promotional messaging.

Before you can use these components, you must first set the AffirmSDK with your public API key from your sandbox [Merchant Dashboard](https://sandbox.affirm.com/dashboard). You must set this key to the shared AffirmConfiguration once (preferably in your AppDelegate) as follows:
```cs
AffirmConfiguration.SharedInstance.ConfigureWithPublicKey("PUBLIC_API_KEY", locale: AffirmLocale.Us, environment: AffirmEnvironment.Sandbox, merchantName:@"Affirm Example");
```

## Checkout

### Checkout creation

Checkout creation is the process in which a customer uses Affirm to pay for a purchase in your app. This process is governed by the `AffirmCheckoutViewController` object, which requires three parameters:

- The `AffirmCheckout` object which contains details about the order
- The `useVCN` object which determines whether the checkout flow should use virtual card network to handle the checkout.

  - if set YES, it will return the debit card information from this delegate 
    ```cs
    void VcnCheckout(UIViewController checkoutViewController, AffirmCreditCard creditCard)
    ```

  - if set NO, it will return checkout token from this delegate 
    ```cs
    void Checkout(AffirmCheckoutViewController checkoutViewController, string checkoutToken)
    ```

- The `AffirmCheckoutDelegate` object which receives messages at various stages in the checkout process

Once the AffirmCheckoutViewController has been constructed from the parameters above, you may present it with any other view controller. This initiates the flow which guides the user through the Affirm checkout process. An example of how this is implemented is provided as follows:

```cs
// initialize an AffirmItem with item details
AffirmItem item = new AffirmItem(name: "Affirm Test Item", SKU: "test_item", unitPrice: dollarPrice, quantity: 1, URL: new NSUrl(urlString: "http://sandbox.affirm.com/item"));

// initialize an AffirmShippingDetail with the user's shipping address
AffirmShippingDetail shipping = AffirmShippingDetail.ShippingDetailWithName(name: "Chester Cheetah", line1: "633 Folsom Street", line2: "", city: "San Francisco", state: "CA", zipCode: "94107", countryCode: "USA");

// initialize an AffirmCheckout object with the item(s), shipping details, tax amount, shipping amount, discounts, financing program, and order ID
AffirmCheckout checkout = new AffirmCheckout(items: new AffirmItem[] { item }, shipping: shipping, taxAmount: NSDecimalNumber.Zero, shippingAmount: NSDecimalNumber.Zero, discounts: null, metadata: null, financingProgram: null, orderId: "JKLMO4321");

// The minimum requirements are to initialize the AffirmCheckout object with the item(s), shipping details, and payout Amount
AffirmCheckout checkout = AffirmCheckout.CheckoutWithItems(items: new AffirmItem[] { item }, shipping: shipping, totalAmount: price);

// initialize an UINavigationController with the checkout object and present it
AffirmCheckoutViewController checkoutViewController = new AffirmCheckoutViewController(this, checkout: checkout, useVCN: false, getReasonCodes: false, cardAuthWindow: 10);
UINavigationController nav = new UINavigationController(rootViewController: checkoutViewController);
PresentViewController(nav, animated: true, completionHandler: null);

// It is recommended that you round the total in the checkout request to two decimal places. Affirm SDK converts the float total to integer cents before initiating the checkout, so may round up or down depending on the decimal places. Ensure that the rounding in your app uses the same calculation across your other backend systems, otherwise, it may cause an error of 1 cent or more in the total validation on your end. 
```

The flow ends once the user has successfully confirmed the checkout or vcn checkout, canceled the checkout, or encountered an error in the process. In each of these cases, Affirm will send a message to the AffirmCheckoutDelegate along with additional information about the result.

### Charge authorization

Once the checkout has been successfully confirmed by the user, the AffirmCheckoutDelegate object will receive a checkout token. This token should be forwarded to your server, which should then use the token to authorize a charge on the user's account. For more details about the server integration, see our [API documentation](https://docs.affirm.com/Integrate_Affirm/Direct_API#3._Authorize_the_charge).

Note - For VCN Checkout, all actions should be done using your existing payment gateway and debit card processor using the virtual card number returned after a successful checkout.

## Promotional Messaging

Affirm promotional messaging components—monthly payment messaging and educational modals—show customers how they can use Affirm to finance their purchases. Promos consist of promotional messaging, which appears directly in your app, and a modal, which  offers users an ability to prequalify.

To create promotional messaging view, the SDK provides the `AffirmPromotionalButton` class, only requires the developer to add to their view and configure to implement. The AffirmPromotionalButton is implemented as follows:

```cs
promotionalButton = new AffirmPromotionalButton(promoID: null, 
                                                showCTA: true, 
                                                pageType: AffirmPageType.Product, 
                                                presentingViewController: this, 
                                                frame: new CGRect(x: 0, y: 0, width: 315, height: 34));
stackView.InsertArrangedSubview(promotionalButton, stackIndex: 0);
```

To show / refresh promotional messaging, use
```cs
promotionalButton.ConfigureWithAmount(amount: new NSDecimalNumber(amountText), 
                                      affirmLogoType: AffirmLogoType.Name,
                                      affirmColor: AffirmColorType.Blue,
                                      remoteFontURL: fontURL,
                                      remoteCssURL: cssURL);
```
or
```cs
promotionalButton.ConfigureWithAmount(amount: new NSDecimalNumber(amountText),
									  affirmLogoType: AffirmLogoType.Name,
                                      affirmColor: AffirmColorType.BlueBlack,
                                      font: UIFont.ItalicSystemFontOfSize(size: 15),
                                      textColor: UIColor.Gray);
```

If you have got the html raw string, you could show the promotional messaging using
```cs
promotionalButton.ConfigureWithHtmlString(htmlString: html,
                                          amount: new NSDecimalNumber(amountText), 
                                          remoteFontURL: fontURL,
                                          remoteCssURL: cssURL);
```
**[Note: the amount fields passed to the promotional messaging configuration methods should be in dollars (no cents), so it is best practice to round up to the nearest dollar before passing.]**

If you want to use local fonts, you need do following steps:
> 1. Add the font files to your project (make sure that the files are targeted properly to your application)
> 2. Add the font files to yourApp-Info.plist
> 3. Use the font in your CSS file, for example
```css
@font-face
{
font-family: 'OpenSansCondensed-Bold';
src: local('OpenSansCondensed-Bold'),url('OpenSansCondensed-Bold.ttf') format('truetype');
}

body {
font-family: 'OpenSansCondensed-Light';
font-weight: normal;
!important;
}
```
**[Note: if no promotional message returned, the button will be hidden automatically]**

Tapping on the Promotional button automatically opens a modal in an `AffirmPrequalModalViewController` with more information, including (if you have it configured) a button that prompts the user to prequalify for Affirm financing.

**[Note: The AffirmPrequalModalViewController is deprecated as of SDK version 4.0.13.]** To display the AffirmPromoModal outside of tapping on the AffirmPromotionalButton, you may initialize and display an instance of the promo modal viewController as follows:

```cs
AffirmPromoModalViewController viewController = new AffirmPromoModalViewController(promoId: "promo_id", amount: amount, @delegate: this);
UINavigationController nav = new UINavigationController(rootViewController: viewController);
presentingViewController.PresentViewController(nav, animated: true, completionHandler: null);
```

## Retrieve raw string from As low as message

You can retrieve raw string using `AffirmDataHandler`.

```cs
NSDecimalNumber dollarPrice = new NSDecimalNumber(numberValue: amountTextField.Text);
AffirmDataHandler.GetPromoMessageWithPromoID(promoID: null, 
                                             amount: dollarPrice, 
                                             showCTA: true, 
                                             pageType: AffirmPageType.Banner, 
                                             logoType: AffirmLogoType.Name, 
                                             colorType: AffirmColorType.Blue, 
                                             font: UIFont.BoldSystemFontOfSize(15), 
                                             textColor: UIColor.Gray, 
                                             @delegate: this, 
                                             completionHandler: (NSAttributedString attributedString, UIViewController viewController, NSError error) =>
                                             {
                                                promoButton.SetAttributedTitle(title: attributedString, UIControlState.Normal);
                                                promoViewController = viewController;
                                             });
```
After that, you could present promo modal using
```cs
UINavigationController nav = new UINavigationController(rootViewController: promoViewController);
PresentViewController(nav, animated: true, completionHandler: null);
```

## Track Order Confirmed

The trackOrderConfirmed event triggers when a customer completes their purchase. The SDK provides the  `AffirmOrderTrackerViewController` class to track it, it requires `AffirmOrder` and an array with `AffirmProduct`.

```cs
AffirmOrderTrackerViewController.TrackOrder(order: order, products: new AffirmProduct[] { product0, product1 });
```

**[Note: this feature will be improved after the endpoint is ready for app and it will be disappeared after 5 seconds]**