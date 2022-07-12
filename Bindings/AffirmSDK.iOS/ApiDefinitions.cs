using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
using WebKit;

namespace AffirmSDK
{
	// @interface AffirmActivityIndicatorView : UIView
	[BaseType(typeof(UIView))]
	interface AffirmActivityIndicatorView
	{
		// @property (nonatomic, strong) UIColor * _Nonnull progressTintColor;
		[Export("progressTintColor", ArgumentSemantic.Strong)]
		UIColor ProgressTintColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nonnull backgroundTintColor;
		[Export("backgroundTintColor", ArgumentSemantic.Strong)]
		UIColor BackgroundTintColor { get; set; }

		// @property (nonatomic) BOOL isAnimating;
		[Export("isAnimating")]
		bool IsAnimating { get; set; }

		// @property (nonatomic) CGFloat lineWidth;
		[Export("lineWidth")]
		nfloat LineWidth { get; set; }

		// -(void)startAnimating;
		[Export("startAnimating")]
		void StartAnimating();

		// -(void)stopAnimating;
		[Export("stopAnimating")]
		void StopAnimating();
	}

	// @interface AffirmBaseWebViewController : UIViewController <WKNavigationDelegate, WKUIDelegate>
	[BaseType(typeof(UIViewController))]
	[DisableDefaultCtor]
	interface AffirmBaseWebViewController : IWKNavigationDelegate, IWKUIDelegate
	{
		// @property (readonly, nonatomic, strong) WKWebView * _Nonnull webView;
		[Export("webView", ArgumentSemantic.Strong)]
		WKWebView WebView { get; }

		// @property (readonly, nonatomic, strong) AffirmActivityIndicatorView * _Nonnull activityIndicatorView;
		[Export("activityIndicatorView", ArgumentSemantic.Strong)]
		AffirmActivityIndicatorView ActivityIndicatorView { get; }

		// -(void)loadErrorPage:(NSError * _Nonnull)error;
		[Export("loadErrorPage:")]
		void LoadErrorPage(NSError error);

		// -(void)dismiss;
		[Export("dismiss")]
		void Dismiss();
	}

	// @protocol AffirmJSONifiable <NSObject>
	/* Check whether adding [Model] to this declaration is appropriate.
	   [Model] is used to generate a C# class that implements this protocol,
	   and might be useful for protocols that consumers are supposed to implement,
	   since consumers can subclass the generated class instead of implementing
	   the generated interface. If consumers are not supposed to implement this
	   protocol, then [Model] is redundant and will generate code that will never
	   be used. */
	[Protocol]
	[BaseType(typeof(NSObject))]
	interface AffirmJSONifiable
	{
		// @required -(NSMutableDictionary * _Nonnull)toJSONDictionary;
		[Abstract]
		[Export("toJSONDictionary")]
		NSMutableDictionary ToJSONDictionary();
	}

	// @interface AffirmBillingDetail : NSObject <AffirmJSONifiable, NSCopying>
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmBillingDetail : AffirmJSONifiable, INSCopying
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable name;
		[NullAllowed, Export("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable phoneNumber;
		[NullAllowed, Export("phoneNumber")]
		string PhoneNumber { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable email;
		[NullAllowed, Export("email")]
		string Email { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable line1;
		[NullAllowed, Export("line1")]
		string Line1 { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable line2;
		[NullAllowed, Export("line2")]
		string Line2 { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable city;
		[NullAllowed, Export("city")]
		string City { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable state;
		[NullAllowed, Export("state")]
		string State { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable zipCode;
		[NullAllowed, Export("zipCode")]
		string ZipCode { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable countryCode;
		[NullAllowed, Export("countryCode")]
		string CountryCode { get; }

		// +(AffirmBillingDetail * _Nonnull)billingDetailWithName:(NSString * _Nullable)name email:(NSString * _Nullable)email phoneNumber:(NSString * _Nullable)phoneNumber addressWithLine1:(NSString * _Nullable)line1 line2:(NSString * _Nullable)line2 city:(NSString * _Nullable)city state:(NSString * _Nullable)state zipCode:(NSString * _Nullable)zipCode countryCode:(NSString * _Nullable)countryCode __attribute__((swift_name("billingDetail(name:email:phoneNumber:line1:line2:city:state:zipCode:countryCode:)")));
		[Static]
		[Export("billingDetailWithName:email:phoneNumber:addressWithLine1:line2:city:state:zipCode:countryCode:")]
		AffirmBillingDetail BillingDetailWithName([NullAllowed] string name, [NullAllowed] string email, [NullAllowed] string phoneNumber, [NullAllowed] string line1, [NullAllowed] string line2, [NullAllowed] string city, [NullAllowed] string state, [NullAllowed] string zipCode, [NullAllowed] string countryCode);

		// -(instancetype _Nonnull)initBillingDetailWithName:(NSString * _Nullable)name email:(NSString * _Nullable)email phoneNumber:(NSString * _Nullable)phoneNumber addressWithLine1:(NSString * _Nullable)line1 line2:(NSString * _Nullable)line2 city:(NSString * _Nullable)city state:(NSString * _Nullable)state zipCode:(NSString * _Nullable)zipCode countryCode:(NSString * _Nullable)countryCode __attribute__((swift_name("init(name:email:phoneNumber:line1:line2:city:state:zipCode:countryCode:)")));
		[Export("initBillingDetailWithName:email:phoneNumber:addressWithLine1:line2:city:state:zipCode:countryCode:")]
		IntPtr Constructor([NullAllowed] string name, [NullAllowed] string email, [NullAllowed] string phoneNumber, [NullAllowed] string line1, [NullAllowed] string line2, [NullAllowed] string city, [NullAllowed] string state, [NullAllowed] string zipCode, [NullAllowed] string countryCode);
	}

	// @interface AffirmCardInfoViewController : UIViewController
	[BaseType(typeof(UIViewController))]
	[DisableDefaultCtor]
	interface AffirmCardInfoViewController
	{
		[Wrap("WeakDelegate")]
		[NullAllowed]
		IAffirmCheckoutDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<AffirmCheckoutDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readonly, copy, nonatomic) AffirmCheckout * _Nonnull checkout;
		[Export("checkout", ArgumentSemantic.Copy)]
		AffirmCheckout Checkout { get; }

		// @property (readonly, nonatomic, strong) AffirmCreditCard * _Nonnull creditCard;
		[Export("creditCard", ArgumentSemantic.Strong)]
		AffirmCreditCard CreditCard { get; }

		// @property (readonly, nonatomic) BOOL getReasonCodes;
		[Export("getReasonCodes")]
		bool GetReasonCodes { get; }

		// +(UINavigationController * _Nonnull)startCheckoutWithNavigation:(AffirmCheckout * _Nonnull)checkout creditCard:(AffirmCreditCard * _Nonnull)creditCard getReasonCodes:(BOOL)getReasonCodes delegate:(id<AffirmCheckoutDelegate> _Nonnull)delegate __attribute__((swift_name("startNavigation(checkout:creditCard:getReasonCodes:delegate:)")));
		[Static]
		[Export("startCheckoutWithNavigation:creditCard:getReasonCodes:delegate:")]
		UINavigationController StartCheckoutWithNavigation(AffirmCheckout checkout, AffirmCreditCard creditCard, bool getReasonCodes, IAffirmCheckoutDelegate @delegate);
	}

	// @interface AffirmBrand : NSObject
	[BaseType(typeof(NSObject))]
	interface AffirmBrand
	{
		// @property (nonatomic, strong) NSString * _Nonnull name;
		[Export("name", ArgumentSemantic.Strong)]
		string Name { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull rangeStart;
		[Export("rangeStart", ArgumentSemantic.Strong)]
		string RangeStart { get; set; }

		// @property (nonatomic, strong) NSString * _Nonnull rangeEnd;
		[Export("rangeEnd", ArgumentSemantic.Strong)]
		string RangeEnd { get; set; }

		// @property (nonatomic) NSInteger length;
		[Export("length")]
		nint Length { get; set; }

		// @property (nonatomic) AffirmBrandType type;
		[Export("type", ArgumentSemantic.Assign)]
		AffirmBrandType Type { get; set; }

		// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name rangeStart:(NSString * _Nonnull)rangeStart rangeEnd:(NSString * _Nonnull)rangeEnd length:(NSInteger)length type:(AffirmBrandType)type;
		[Export("initWithName:rangeStart:rangeEnd:length:type:")]
		IntPtr Constructor(string name, string rangeStart, string rangeEnd, nint length, AffirmBrandType type);

		// +(instancetype _Nonnull)brandWithName:(NSString * _Nonnull)name rangeStart:(NSString * _Nonnull)rangeStart rangeEnd:(NSString * _Nonnull)rangeEnd length:(NSInteger)length type:(AffirmBrandType)type;
		[Static]
		[Export("brandWithName:rangeStart:rangeEnd:length:type:")]
		AffirmBrand BrandWithName(string name, string rangeStart, string rangeEnd, nint length, AffirmBrandType type);

		// -(BOOL)matchesNumber:(NSString * _Nonnull)number;
		[Export("matchesNumber:")]
		bool MatchesNumber(string number);
	}

	// @interface AffirmCardValidator : NSObject
	[BaseType(typeof(NSObject))]
	interface AffirmCardValidator
	{
		// +(instancetype _Nonnull)sharedCardValidator;
		[Static]
		[Export("sharedCardValidator")]
		AffirmCardValidator SharedCardValidator();

		// -(AffirmBrand * _Nullable)brandForCardNumber:(NSString * _Nonnull)cardNumber;
		[Export("brandForCardNumber:")]
		[return: NullAllowed]
		AffirmBrand BrandForCardNumber(string cardNumber);

		// +(NSArray<NSNumber *> * _Nonnull)cardNumberFormatForBrand:(AffirmBrandType)type;
		[Static]
		[Export("cardNumberFormatForBrand:")]
		NSNumber[] CardNumberFormatForBrand(AffirmBrandType type);
	}

	// @interface AffirmCheckout : NSObject <AffirmJSONifiable, NSCopying>
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmCheckout : AffirmJSONifiable, INSCopying
	{
		// @property (nonatomic) BOOL sendShippingAddress;
		[Export("sendShippingAddress")]
		bool SendShippingAddress { get; set; }

		// @property (readonly, copy, nonatomic) NSArray<AffirmItem *> * _Nonnull items;
		[Export("items", ArgumentSemantic.Copy)]
		AffirmItem[] Items { get; }

		// @property (readonly, copy, nonatomic) AffirmShippingDetail * _Nullable shipping;
		[NullAllowed, Export("shipping", ArgumentSemantic.Copy)]
		AffirmShippingDetail Shipping { get; }

		// @property (copy, nonatomic) AffirmBillingDetail * _Nullable billing;
		[NullAllowed, Export("billing", ArgumentSemantic.Copy)]
		AffirmBillingDetail Billing { get; set; }

		// @property (readonly, copy, nonatomic) NSDecimalNumber * _Nonnull taxAmount;
		[Export("taxAmount", ArgumentSemantic.Copy)]
		NSDecimalNumber TaxAmount { get; }

		// @property (readonly, copy, nonatomic) NSDecimalNumber * _Nonnull shippingAmount;
		[Export("shippingAmount", ArgumentSemantic.Copy)]
		NSDecimalNumber ShippingAmount { get; }

		// @property (readonly, copy, nonatomic) NSArray<AffirmDiscount *> * _Nullable discounts;
		[NullAllowed, Export("discounts", ArgumentSemantic.Copy)]
		AffirmDiscount[] Discounts { get; }

		// @property (readonly, copy, nonatomic) NSDictionary * _Nullable metadata;
		[NullAllowed, Export("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }

		// @property (copy, nonatomic) NSDecimalNumber * _Nonnull totalAmount;
		[Export("totalAmount", ArgumentSemantic.Copy)]
		NSDecimalNumber TotalAmount { get; set; }

		// @property (copy, nonatomic) API_DEPRECATED("Use totalAmount instead.", ios(2.0, 13.0)) NSDecimalNumber * payoutAmount __attribute__((availability(ios, introduced=2.0, deprecated=13.0)));
		[Introduced(PlatformName.iOS, 2, 0, message: "Use totalAmount instead.")]
		[Deprecated(PlatformName.iOS, 13, 0, message: "Use totalAmount instead.")]
		[Export("payoutAmount", ArgumentSemantic.Copy)]
		NSDecimalNumber PayoutAmount { get; set; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable financingProgram;
		[NullAllowed, Export("financingProgram")]
		string FinancingProgram { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable orderId;
		[NullAllowed, Export("orderId")]
		string OrderId { get; }

		// @property (copy, nonatomic) NSString * _Nullable caas;
		[NullAllowed, Export("caas")]
		string Caas { get; set; }

		// -(instancetype _Nonnull)initWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping taxAmount:(NSDecimalNumber * _Nonnull)taxAmount shippingAmount:(NSDecimalNumber * _Nonnull)shippingAmount discounts:(NSArray<AffirmDiscount *> * _Nullable)discounts metadata:(NSDictionary * _Nullable)metadata financingProgram:(NSString * _Nullable)financingProgram __attribute__((swift_name("init(items:shipping:taxAmount:shippingAmount:discounts:metadata:financingProgram:)")));
		[Export("initWithItems:shipping:taxAmount:shippingAmount:discounts:metadata:financingProgram:")]
		IntPtr Constructor(AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, NSDecimalNumber taxAmount, NSDecimalNumber shippingAmount, [NullAllowed] AffirmDiscount[] discounts, [NullAllowed] NSDictionary metadata, [NullAllowed] string financingProgram);

		// -(instancetype _Nonnull)initWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping taxAmount:(NSDecimalNumber * _Nonnull)taxAmount shippingAmount:(NSDecimalNumber * _Nonnull)shippingAmount discounts:(NSArray<AffirmDiscount *> * _Nullable)discounts metadata:(NSDictionary * _Nullable)metadata financingProgram:(NSString * _Nullable)financingProgram orderId:(NSString * _Nullable)orderId __attribute__((swift_name("init(items:shipping:taxAmount:shippingAmount:discounts:metadata:financingProgram:orderId:)")));
		[Export("initWithItems:shipping:taxAmount:shippingAmount:discounts:metadata:financingProgram:orderId:")]
		IntPtr Constructor(AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, NSDecimalNumber taxAmount, NSDecimalNumber shippingAmount, [NullAllowed] AffirmDiscount[] discounts, [NullAllowed] NSDictionary metadata, [NullAllowed] string financingProgram, [NullAllowed] string orderId);

		// +(AffirmCheckout * _Nonnull)checkoutWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping taxAmount:(NSDecimalNumber * _Nonnull)taxAmount shippingAmount:(NSDecimalNumber * _Nonnull)shippingAmount financingProgram:(NSString * _Nullable)financingProgram __attribute__((swift_name("checkout(items:shipping:taxAmount:shippingAmount:financingProgram:)")));
		[Static]
		[Export("checkoutWithItems:shipping:taxAmount:shippingAmount:financingProgram:")]
		AffirmCheckout CheckoutWithItems(AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, NSDecimalNumber taxAmount, NSDecimalNumber shippingAmount, [NullAllowed] string financingProgram);

		// +(AffirmCheckout * _Nonnull)checkoutWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping taxAmount:(NSDecimalNumber * _Nonnull)taxAmount shippingAmount:(NSDecimalNumber * _Nonnull)shippingAmount discounts:(NSArray<AffirmDiscount *> * _Nullable)discounts metadata:(NSDictionary * _Nullable)metadata financingProgram:(NSString * _Nullable)financingProgram __attribute__((swift_name("checkout(items:shipping:taxAmount:shippingAmount:discounts:metadata:financingProgram:)")));
		[Static]
		[Export("checkoutWithItems:shipping:taxAmount:shippingAmount:discounts:metadata:financingProgram:")]
		AffirmCheckout CheckoutWithItems(AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, NSDecimalNumber taxAmount, NSDecimalNumber shippingAmount, [NullAllowed] AffirmDiscount[] discounts, [NullAllowed] NSDictionary metadata, [NullAllowed] string financingProgram);

		// +(AffirmCheckout * _Nonnull)checkoutWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping taxAmount:(NSDecimalNumber * _Nonnull)taxAmount shippingAmount:(NSDecimalNumber * _Nonnull)shippingAmount __attribute__((swift_name("checkout(items:shipping:taxAmount:shippingAmount:)")));
		[Static]
		[Export("checkoutWithItems:shipping:taxAmount:shippingAmount:")]
		AffirmCheckout CheckoutWithItems(AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, NSDecimalNumber taxAmount, NSDecimalNumber shippingAmount);

		// +(AffirmCheckout * _Nonnull)checkoutWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping taxAmount:(NSDecimalNumber * _Nonnull)taxAmount shippingAmount:(NSDecimalNumber * _Nonnull)shippingAmount discounts:(NSArray<AffirmDiscount *> * _Nullable)discounts metadata:(NSDictionary * _Nullable)metadata __attribute__((swift_name("checkout(items:shipping:taxAmount:shippingAmount:discounts:metadata:)")));
		[Static]
		[Export("checkoutWithItems:shipping:taxAmount:shippingAmount:discounts:metadata:")]
		AffirmCheckout CheckoutWithItems(AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, NSDecimalNumber taxAmount, NSDecimalNumber shippingAmount, [NullAllowed] AffirmDiscount[] discounts, [NullAllowed] NSDictionary metadata);

		// -(instancetype _Nonnull)initWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping discounts:(NSArray<AffirmDiscount *> * _Nullable)discounts metadata:(NSDictionary * _Nullable)metadata financingProgram:(NSString * _Nullable)financingProgram payoutAmount:(NSDecimalNumber * _Nonnull)payoutAmount __attribute__((availability(ios, introduced=2.0, deprecated=13.0))) __attribute__((swift_name("init(items:shipping:discounts:metadata:financingProgram:payoutAmount:)")));
		//[Introduced (PlatformName.iOS, 2, 0, message: "Use initWithItems:shipping:discounts:metadata:financingProgram:totalAmount: instead.")]
		//[Deprecated (PlatformName.iOS, 13, 0, message: "Use initWithItems:shipping:discounts:metadata:financingProgram:totalAmount: instead.")]
		//[Export ("initWithItems:shipping:discounts:metadata:financingProgram:payoutAmount:")]
		//IntPtr Constructor (AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, [NullAllowed] AffirmDiscount[] discounts, [NullAllowed] NSDictionary metadata, [NullAllowed] string financingProgram, NSDecimalNumber payoutAmount);

		// -(instancetype _Nonnull)initWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping discounts:(NSArray<AffirmDiscount *> * _Nullable)discounts metadata:(NSDictionary * _Nullable)metadata financingProgram:(NSString * _Nullable)financingProgram totalAmount:(NSDecimalNumber * _Nonnull)totalAmount __attribute__((swift_name("init(items:shipping:discounts:metadata:financingProgram:totalAmount:)")));
		[Export("initWithItems:shipping:discounts:metadata:financingProgram:totalAmount:")]
		IntPtr Constructor(AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, [NullAllowed] AffirmDiscount[] discounts, [NullAllowed] NSDictionary metadata, [NullAllowed] string financingProgram, NSDecimalNumber totalAmount);

		// +(AffirmCheckout * _Nonnull)checkoutWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping payoutAmount:(NSDecimalNumber * _Nonnull)payoutAmount __attribute__((availability(ios, introduced=2.0, deprecated=13.0))) __attribute__((swift_name("checkout(items:shipping:payoutAmount:)")));
		//[Introduced (PlatformName.iOS, 2, 0, message: "Use checkoutWithItems:shipping:totalAmount: instead.")]
		//[Deprecated (PlatformName.iOS, 13, 0, message: "Use checkoutWithItems:shipping:totalAmount: instead.")]
		//[Static]
		//[Export ("checkoutWithItems:shipping:payoutAmount:")]
		//AffirmCheckout CheckoutWithItems (AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, NSDecimalNumber payoutAmount);

		// +(AffirmCheckout * _Nonnull)checkoutWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping totalAmount:(NSDecimalNumber * _Nonnull)totalAmount __attribute__((swift_name("checkout(items:shipping:totalAmount:)")));
		[Static]
		[Export("checkoutWithItems:shipping:totalAmount:")]
		AffirmCheckout CheckoutWithItems(AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, NSDecimalNumber totalAmount);

		// +(AffirmCheckout * _Nonnull)checkoutWithItems:(NSArray<AffirmItem *> * _Nonnull)items shipping:(AffirmShippingDetail * _Nullable)shipping totalAmount:(NSDecimalNumber * _Nonnull)totalAmount metadata:(NSDictionary * _Nullable)metadata __attribute__((swift_name("checkout(items:shipping:totalAmount:metadata:)")));
		[Static]
		[Export("checkoutWithItems:shipping:totalAmount:metadata:")]
		AffirmCheckout CheckoutWithItems(AffirmItem[] items, [NullAllowed] AffirmShippingDetail shipping, NSDecimalNumber totalAmount, [NullAllowed] NSDictionary metadata);
	}

	// @protocol AffirmCheckoutDelegate <NSObject>
	[Protocol, Model(AutoGeneratedName = true)]
	[BaseType(typeof(NSObject))]
	interface AffirmCheckoutDelegate
	{
		// @required -(void)checkout:(AffirmCheckoutViewController * _Nonnull)checkoutViewController completedWithToken:(NSString * _Nonnull)checkoutToken;
		[Abstract]
		[Export("checkout:completedWithToken:")]
		void Checkout(AffirmCheckoutViewController checkoutViewController, string checkoutToken);

		// @required -(void)vcnCheckout:(UIViewController * _Nonnull)checkoutViewController completedWithCreditCard:(AffirmCreditCard * _Nonnull)creditCard;
		[Abstract]
		[Export("vcnCheckout:completedWithCreditCard:")]
		void VcnCheckout(UIViewController checkoutViewController, AffirmCreditCard creditCard);

		// @required -(void)checkoutCancelled:(UIViewController * _Nonnull)checkoutViewController;
		[Abstract]
		[Export("checkoutCancelled:")]
		void CheckoutCancelled(UIViewController checkoutViewController);

		// @required -(void)checkoutCancelled:(AffirmCheckoutViewController * _Nonnull)checkoutViewController checkoutCanceledWithReason:(AffirmReasonCode * _Nonnull)reasonCode;
		[Abstract]
		[Export("checkoutCancelled:checkoutCanceledWithReason:")]
		void CheckoutCancelled(AffirmCheckoutViewController checkoutViewController, AffirmReasonCode reasonCode);

		// @required -(void)checkout:(AffirmCheckoutViewController * _Nonnull)checkoutViewController didFailWithError:(NSError * _Nonnull)error;
		[Abstract]
		[Export("checkout:didFailWithError:")]
		void Checkout(AffirmCheckoutViewController checkoutViewController, NSError error);
	}

	interface IAffirmCheckoutDelegate { }

	// @interface AffirmCheckoutViewController : AffirmBaseWebViewController
	[BaseType(typeof(AffirmBaseWebViewController))]
	[DisableDefaultCtor]
	interface AffirmCheckoutViewController
	{
		[Wrap("WeakDelegate")]
		[NullAllowed]
		IAffirmCheckoutDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<AffirmCheckoutDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readonly, copy, nonatomic) AffirmCheckout * _Nonnull checkout;
		[Export("checkout", ArgumentSemantic.Copy)]
		AffirmCheckout Checkout { get; }

		// @property (readonly, nonatomic) BOOL useVCN;
		[Export("useVCN")]
		bool UseVCN { get; }

		// @property (readonly, nonatomic) BOOL getReasonCodes;
		[Export("getReasonCodes")]
		bool GetReasonCodes { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull checkoutARI;
		[Export("checkoutARI")]
		string CheckoutARI { get; }

		// @property (readonly, nonatomic) NSInteger cardAuthWindow;
		[Export("cardAuthWindow")]
		nint CardAuthWindow { get; }

		// -(instancetype _Nonnull)initWithDelegate:(id<AffirmCheckoutDelegate> _Nonnull)delegate checkout:(AffirmCheckout * _Nonnull)checkout useVCN:(BOOL)useVCN getReasonCodes:(BOOL)getReasonCodes __attribute__((swift_name("init(delegate:checkout:useVCN:getReasonCodes:)"))) __attribute__((objc_designated_initializer));
		[Export("initWithDelegate:checkout:useVCN:getReasonCodes:")]
		[DesignatedInitializer]
		IntPtr Constructor(IAffirmCheckoutDelegate @delegate, AffirmCheckout checkout, bool useVCN, bool getReasonCodes);

		// -(instancetype _Nonnull)initWithDelegate:(id<AffirmCheckoutDelegate> _Nonnull)delegate checkout:(AffirmCheckout * _Nonnull)checkout useVCN:(BOOL)useVCN getReasonCodes:(BOOL)getReasonCodes cardAuthWindow:(NSInteger)cardAuthWindow __attribute__((swift_name("init(delegate:checkout:useVCN:getReasonCodes:cardAuthWindow:)"))) __attribute__((objc_designated_initializer));
		[Export("initWithDelegate:checkout:useVCN:getReasonCodes:cardAuthWindow:")]
		[DesignatedInitializer]
		IntPtr Constructor(IAffirmCheckoutDelegate @delegate, AffirmCheckout checkout, bool useVCN, bool getReasonCodes, nint cardAuthWindow);

		// +(UINavigationController * _Nonnull)startCheckoutWithNavigation:(AffirmCheckout * _Nonnull)checkout useVCN:(BOOL)useVCN getReasonCodes:(BOOL)getReasonCodes delegate:(id<AffirmCheckoutDelegate> _Nonnull)delegate __attribute__((availability(ios, introduced=2.0, deprecated=14.0))) __attribute__((swift_name("startNavigation(checkout:useVCN:getReasonCodes:delegate:)")));
		[Introduced(PlatformName.iOS, 2, 0, message: "Use initWithDelegate:checkout:useVCN:getReasonCodes: instead.")]
		[Deprecated(PlatformName.iOS, 14, 0, message: "Use initWithDelegate:checkout:useVCN:getReasonCodes: instead.")]
		[Static]
		[Export("startCheckoutWithNavigation:useVCN:getReasonCodes:delegate:")]
		UINavigationController StartCheckoutWithNavigation(AffirmCheckout checkout, bool useVCN, bool getReasonCodes, IAffirmCheckoutDelegate @delegate);

		// +(AffirmCheckoutViewController * _Nonnull)startCheckout:(AffirmCheckout * _Nonnull)checkout delegate:(id<AffirmCheckoutDelegate> _Nonnull)delegate __attribute__((availability(ios, introduced=2.0, deprecated=14.0))) __attribute__((swift_name("start(checkout:delegate:)")));
		[Introduced(PlatformName.iOS, 2, 0, message: "Use initWithDelegate:checkout:useVCN:getReasonCodes: instead.")]
		[Deprecated(PlatformName.iOS, 14, 0, message: "Use initWithDelegate:checkout:useVCN:getReasonCodes: instead.")]
		[Static]
		[Export("startCheckout:delegate:")]
		AffirmCheckoutViewController StartCheckout(AffirmCheckout checkout, IAffirmCheckoutDelegate @delegate);

		// +(AffirmCheckoutViewController * _Nonnull)startCheckout:(AffirmCheckout * _Nonnull)checkout useVCN:(BOOL)useVCN delegate:(id<AffirmCheckoutDelegate> _Nonnull)delegate __attribute__((availability(ios, introduced=2.0, deprecated=14.0))) __attribute__((swift_name("start(checkout:useVCN:delegate:)")));
		[Introduced(PlatformName.iOS, 2, 0, message: "Use initWithDelegate:checkout:useVCN:getReasonCodes: instead.")]
		[Deprecated(PlatformName.iOS, 14, 0, message: "Use initWithDelegate:checkout:useVCN:getReasonCodes: instead.")]
		[Static]
		[Export("startCheckout:useVCN:delegate:")]
		AffirmCheckoutViewController StartCheckout(AffirmCheckout checkout, bool useVCN, IAffirmCheckoutDelegate @delegate);

		// +(AffirmCheckoutViewController * _Nonnull)startCheckout:(AffirmCheckout * _Nonnull)checkout useVCN:(BOOL)useVCN getReasonCodes:(BOOL)getReasonCodes delegate:(id<AffirmCheckoutDelegate> _Nonnull)delegate __attribute__((availability(ios, introduced=2.0, deprecated=14.0))) __attribute__((swift_name("start(checkout:useVCN:getReasonCodes:delegate:)")));
		[Introduced(PlatformName.iOS, 2, 0, message: "Use initWithDelegate:checkout:useVCN:getReasonCodes: instead.")]
		[Deprecated(PlatformName.iOS, 14, 0, message: "Use initWithDelegate:checkout:useVCN:getReasonCodes: instead.")]
		[Static]
		[Export("startCheckout:useVCN:getReasonCodes:delegate:")]
		AffirmCheckoutViewController StartCheckout(AffirmCheckout checkout, bool useVCN, bool getReasonCodes, IAffirmCheckoutDelegate @delegate);
	}

	// @protocol AffirmRequestProtocol <NSObject>
	/* Check whether adding [Model] to this declaration is appropriate.
	   [Model] is used to generate a C# class that implements this protocol,
	   and might be useful for protocols that consumers are supposed to implement,
	   since consumers can subclass the generated class instead of implementing
	   the generated interface. If consumers are not supposed to implement this
	   protocol, then [Model] is redundant and will generate code that will never
	   be used. */
	[Protocol]
	[BaseType(typeof(NSObject))]
	interface AffirmRequestProtocol
	{
		// @required -(NSString * _Nonnull)path;
		[Abstract]
		[Export("path")]
		// [Verify (MethodToProperty)]
		string Path { get; }

		// @required -(AffirmHTTPMethod)method;
		[Abstract]
		[Export("method")]
		// [Verify (MethodToProperty)]
		AffirmHTTPMethod Method { get; }

		// @required -(NSDictionary * _Nonnull)parameters;
		[Abstract]
		[Export("parameters")]
		// [Verify (MethodToProperty)]
		NSDictionary Parameters { get; }

		// @optional -(Class _Nonnull)responseClass;
		[Export("responseClass")]
		// [Verify (MethodToProperty)]
		Class ResponseClass { get; }

		// @optional -(NSDictionary * _Nonnull)headers;
		[Export("headers")]
		// [Verify (MethodToProperty)]
		NSDictionary Headers { get; }
	}

	interface IAffirmRequestProtocol { }

	// @protocol AffirmResponseProtocol <NSObject>
	/* Check whether adding [Model] to this declaration is appropriate.
	   [Model] is used to generate a C# class that implements this protocol,
	   and might be useful for protocols that consumers are supposed to implement,
	   since consumers can subclass the generated class instead of implementing
	   the generated interface. If consumers are not supposed to implement this
	   protocol, then [Model] is redundant and will generate code that will never
	   be used. */
	[Protocol]
	[BaseType(typeof(NSObject))]
	interface AffirmResponseProtocol
	{
		// @optional +(id<AffirmResponseProtocol> _Nonnull)parse:(NSData * _Nonnull)data;
		[Static]
		[Export("parse:")]
		AffirmResponseProtocol Parse(NSData data);

		// @optional +(id<AffirmResponseProtocol> _Nullable)parseError:(NSData * _Nonnull)data;
		[Static]
		[Export("parseError:")]
		[return: NullAllowed]
		AffirmResponseProtocol ParseError(NSData data);
	}

	// typedef void (^AffirmRequestHandler)(id<AffirmResponseProtocol> _Nullable, NSError * _Nullable);
	delegate void AffirmRequestHandler([NullAllowed] AffirmResponseProtocol arg0, [NullAllowed] NSError arg1);

	// @protocol AffirmClientProtocol <NSObject>
	/* Check whether adding [Model] to this declaration is appropriate.
	   [Model] is used to generate a C# class that implements this protocol,
	   and might be useful for protocols that consumers are supposed to implement,
	   since consumers can subclass the generated class instead of implementing
	   the generated interface. If consumers are not supposed to implement this
	   protocol, then [Model] is redundant and will generate code that will never
	   be used. */
	[Protocol]
	[BaseType(typeof(NSObject))]
	interface AffirmClientProtocol
	{
		// @required +(NSString * _Nonnull)host;
		[Static, Abstract]
		[Export("host")]
		// [Verify (MethodToProperty)]
		string Host { get; }

		// @optional +(void)send:(id<AffirmRequestProtocol> _Nonnull)request handler:(AffirmRequestHandler _Nonnull)handler;
		[Static]
		[Export("send:handler:")]
		void Handler(IAffirmRequestProtocol request, AffirmRequestHandler handler);
	}

	// @interface AffirmTrackerClient : NSObject <AffirmClientProtocol>
	[BaseType(typeof(NSObject))]
	interface AffirmTrackerClient : AffirmClientProtocol
	{
	}

	// @interface AffirmPromoClient : NSObject <AffirmClientProtocol>
	[BaseType(typeof(NSObject))]
	interface AffirmPromoClient : AffirmClientProtocol
	{
	}

	// @interface AffirmCheckoutClient : NSObject <AffirmClientProtocol>
	[BaseType(typeof(NSObject))]
	interface AffirmCheckoutClient : AffirmClientProtocol
	{
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants
	{
		// extern double AffirmSDKVersionNumber;
		[Field("AffirmSDKVersionNumber", "__Internal")]
		double AffirmSDKVersionNumber { get; }

		// extern const unsigned char [] AffirmSDKVersionString;
		[Field("AffirmSDKVersionString", "__Internal")]
		IntPtr AffirmSDKVersionString { get; }

		// extern const NSErrorDomain _Nonnull AffirmSDKErrorDomain;
		[Field("AffirmSDKErrorDomain", "__Internal")]
		NSString AffirmSDKErrorDomain { get; }

		// extern NSString *const _Nonnull AFFIRM_US_DOMAIN;
		[Field("AFFIRM_US_DOMAIN", "__Internal")]
		NSString AFFIRM_US_DOMAIN { get; }

		// extern NSString *const _Nonnull AFFIRM_CA_DOMAIN;
		[Field("AFFIRM_CA_DOMAIN", "__Internal")]
		NSString AFFIRM_CA_DOMAIN { get; }

		// extern NSString *const _Nonnull AFFIRM_MAX_PROMO_AMOUNT;
		[Field("AFFIRM_MAX_PROMO_AMOUNT", "__Internal")]
		NSString AFFIRM_MAX_PROMO_AMOUNT { get; }

		// extern NSString *const _Nonnull AFFIRM_CHECKOUT_CONFIRMATION_URL;
		[Field("AFFIRM_CHECKOUT_CONFIRMATION_URL", "__Internal")]
		NSString AFFIRM_CHECKOUT_CONFIRMATION_URL { get; }

		// extern NSString *const _Nonnull AFFIRM_CHECKOUT_CANCELLATION_URL;
		[Field("AFFIRM_CHECKOUT_CANCELLATION_URL", "__Internal")]
		NSString AFFIRM_CHECKOUT_CANCELLATION_URL { get; }

		// extern NSString *const _Nonnull AFFIRM_PREQUAL_REFERRING_URL;
		[Field("AFFIRM_PREQUAL_REFERRING_URL", "__Internal")]
		NSString AFFIRM_PREQUAL_REFERRING_URL { get; }

		// extern NSString *const _Nonnull AffirmFontFamilyNameCalibre;
		[Field("AffirmFontFamilyNameCalibre", "__Internal")]
		NSString AffirmFontFamilyNameCalibre { get; }

		// extern NSString *const _Nonnull AffirmFontNameCalibreMedium;
		[Field("AffirmFontNameCalibreMedium", "__Internal")]
		NSString AffirmFontNameCalibreMedium { get; }

		// extern NSString *const _Nonnull AffirmFontNameCalibreBold;
		[Field("AffirmFontNameCalibreBold", "__Internal")]
		NSString AffirmFontNameCalibreBold { get; }

		// extern NSString *const _Nonnull AffirmFontNameCalibreSemibold;
		[Field("AffirmFontNameCalibreSemibold", "__Internal")]
		NSString AffirmFontNameCalibreSemibold { get; }

		// extern NSString *const _Nonnull AffirmFontNameCalibreRegular;
		[Field("AffirmFontNameCalibreRegular", "__Internal")]
		NSString AffirmFontNameCalibreRegular { get; }

		// extern NSString *const _Nonnull AffirmFontFamilyNameAlmaMono;
		[Field("AffirmFontFamilyNameAlmaMono", "__Internal")]
		NSString AffirmFontFamilyNameAlmaMono { get; }

		// extern NSString *const _Nonnull AffirmFontNameAlmaMonoBold;
		[Field("AffirmFontNameAlmaMonoBold", "__Internal")]
		NSString AffirmFontNameAlmaMonoBold { get; }
	}

	// @interface AffirmConfiguration : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmConfiguration
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull publicKey;
		[Export("publicKey")]
		string PublicKey { get; }

		// @property (readonly, nonatomic) AffirmEnvironment environment;
		[Export("environment")]
		AffirmEnvironment Environment { get; }

		// @property (readonly, nonatomic) AffirmLocale locale;
		[Export("locale")]
		AffirmLocale Locale { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull currency;
		[Export("currency")]
		string Currency { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable merchantName;
		[NullAllowed, Export("merchantName")]
		string MerchantName { get; }

		// @property (readonly, nonatomic) BOOL isProductionEnvironment;
		[Export("isProductionEnvironment")]
		bool IsProductionEnvironment { get; }

		// @property (readonly, nonatomic, strong) AffirmCreditCard * _Nullable creditCard;
		[NullAllowed, Export("creditCard", ArgumentSemantic.Strong)]
		AffirmCreditCard CreditCard { get; }

		// @property (copy, nonatomic) NSString * _Nullable cardTip;
		[NullAllowed, Export("cardTip")]
		string CardTip { get; set; }

		// @property (readonly, nonatomic) BOOL isCreditCardExists;
		[Export("isCreditCardExists")]
		bool IsCreditCardExists { get; }

		// @property (readonly, nonatomic, strong) WKProcessPool * _Nonnull pool;
		[Export("pool", ArgumentSemantic.Strong)]
		WKProcessPool Pool { get; }

		// @property (readonly, nonatomic, strong, class) NS_SWIFT_NAME(shared) AffirmConfiguration * sharedInstance __attribute__((swift_name("shared")));
		[Static]
		[Export("sharedInstance", ArgumentSemantic.Strong)]
		AffirmConfiguration SharedInstance { get; }

		// -(void)configureWithPublicKey:(NSString * _Nonnull)publicKey environment:(AffirmEnvironment)environment __attribute__((swift_name("configure(publicKey:environment:)")));
		[Export("configureWithPublicKey:environment:")]
		void ConfigureWithPublicKey(string publicKey, AffirmEnvironment environment);

		// -(void)configureWithPublicKey:(NSString * _Nonnull)publicKey environment:(AffirmEnvironment)environment merchantName:(NSString * _Nullable)merchantName __attribute__((swift_name("configure(publicKey:environment:merchantName:)")));
		[Export("configureWithPublicKey:environment:merchantName:")]
		void ConfigureWithPublicKey(string publicKey, AffirmEnvironment environment, [NullAllowed] string merchantName);

		// -(void)configureWithPublicKey:(NSString * _Nonnull)publicKey environment:(AffirmEnvironment)environment locale:(AffirmLocale)locale merchantName:(NSString * _Nullable)merchantName __attribute__((swift_name("configure(publicKey:environment:locale:merchantName:)")));
		[Export("configureWithPublicKey:environment:locale:merchantName:")]
		void ConfigureWithPublicKey(string publicKey, AffirmEnvironment environment, AffirmLocale locale, [NullAllowed] string merchantName);

		// +(NSString * _Nonnull)affirmSDKVersion;
		[Static]
		[Export("affirmSDKVersion")]
		// [Verify (MethodToProperty)]
		string AffirmSDKVersion { get; }

		// -(NSString * _Nonnull)domain;
		[Export("domain")]
		// [Verify (MethodToProperty)]
		string Domain { get; }

		// -(NSString * _Nonnull)jsURL;
		[Export("jsURL")]
		// [Verify (MethodToProperty)]
		string JsURL { get; }

		// -(NSString * _Nonnull)environmentDescription;
		[Export("environmentDescription")]
		// [Verify (MethodToProperty)]
		string EnvironmentDescription { get; }

		// +(NSArray<NSHTTPCookie *> * _Nonnull)cookiesForAffirm;
		[Static]
		[Export("cookiesForAffirm")]
		// [Verify (MethodToProperty)]
		NSHttpCookie[] CookiesForAffirm { get; }

		// +(void)deleteAffirmCookies;
		[Static]
		[Export("deleteAffirmCookies")]
		void DeleteAffirmCookies();
	}

	// @interface AffirmCreditCardBillAddress : NSObject
	[BaseType(typeof(NSObject))]
	interface AffirmCreditCardBillAddress
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable city;
		[NullAllowed, Export("city")]
		string City { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable state;
		[NullAllowed, Export("state")]
		string State { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable zipCode;
		[NullAllowed, Export("zipCode")]
		string ZipCode { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable line1;
		[NullAllowed, Export("line1")]
		string Line1 { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable line2;
		[NullAllowed, Export("line2")]
		string Line2 { get; }

		// -(instancetype _Nonnull)initWithDict:(NSDictionary * _Nonnull)dict;
		[Export("initWithDict:")]
		IntPtr Constructor(NSDictionary dict);
	}

	// @interface AffirmCreditCard : NSObject
	[BaseType(typeof(NSObject))]
	interface AffirmCreditCard
	{
		// @property (readonly, nonatomic, strong) AffirmCreditCardBillAddress * _Nullable billingAddress;
		[NullAllowed, Export("billingAddress", ArgumentSemantic.Strong)]
		AffirmCreditCardBillAddress BillingAddress { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull checkoutToken;
		[Export("checkoutToken")]
		string CheckoutToken { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable created;
		[NullAllowed, Export("created")]
		string Created { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable cvv;
		[NullAllowed, Export("cvv")]
		string Cvv { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable number;
		[NullAllowed, Export("number")]
		string Number { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull callbackId;
		[Export("callbackId")]
		string CallbackId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable cardholderName;
		[NullAllowed, Export("cardholderName")]
		string CardholderName { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable expiration;
		[NullAllowed, Export("expiration")]
		string Expiration { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull creditCardId;
		[Export("creditCardId")]
		string CreditCardId { get; }

		// @property (copy, nonatomic) NSDate * _Nonnull expiredDate;
		[Export("expiredDate", ArgumentSemantic.Copy)]
		NSDate ExpiredDate { get; set; }

		// +(AffirmCreditCard * _Nonnull)creditCardWithDict:(NSDictionary * _Nonnull)dict __attribute__((swift_name("creditCard(dict:)")));
		[Static]
		[Export("creditCardWithDict:")]
		AffirmCreditCard CreditCardWithDict(NSDictionary dict);

		// -(instancetype _Nonnull)initWithDict:(NSDictionary * _Nonnull)dict __attribute__((swift_name("init(dict:)")));
		[Export("initWithDict:")]
		IntPtr Constructor(NSDictionary dict);
	}

	// @protocol AffirmPrequalDelegate <NSObject>
	[Protocol, Model(AutoGeneratedName = true)]
	[BaseType(typeof(NSObject))]
	interface AffirmPrequalDelegate
	{
		// @required -(void)webViewController:(AffirmBaseWebViewController * _Nullable)webViewController didFailWithError:(NSError * _Nonnull)error;
		[Abstract]
		[Export("webViewController:didFailWithError:")]
		void DidFailWithError([NullAllowed] AffirmBaseWebViewController webViewController, NSError error);
	}

	interface IAffirmPrequalDelegate { }

	// @interface AffirmPromotionalButton : UIView
	[BaseType(typeof(UIView))]
	[DisableDefaultCtor]
	interface AffirmPromotionalButton
	{
		// @property (nonatomic, weak) UIViewController<AffirmPrequalDelegate> * _Nullable presentingViewController __attribute__((iboutlet));
		[NullAllowed, Export("presentingViewController", ArgumentSemantic.Weak)]
		IAffirmPrequalDelegate PresentingViewController { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable promoID;
		[NullAllowed, Export("promoID")]
		string PromoID { get; set; }

		// @property (nonatomic) BOOL showCTA;
		[Export("showCTA")]
		bool ShowCTA { get; set; }

		// @property (nonatomic) AffirmPageType pageType;
		[Export("pageType", ArgumentSemantic.Assign)]
		AffirmPageType PageType { get; set; }

		// -(instancetype _Nonnull)initWithPromoID:(NSString * _Nullable)promoID showCTA:(BOOL)showCTA presentingViewController:(UIViewController<AffirmPrequalDelegate> * _Nonnull)presentingViewController frame:(CGRect)frame __attribute__((swift_name("init(promoID:showCTA:presentingViewController:frame:)")));
		[Export("initWithPromoID:showCTA:presentingViewController:frame:")]
		IntPtr Constructor([NullAllowed] string promoID, bool showCTA, IAffirmPrequalDelegate presentingViewController, CGRect frame);

		// -(instancetype _Nonnull)initWithShowCTA:(BOOL)showCTA pageType:(AffirmPageType)pageType presentingViewController:(UIViewController<AffirmPrequalDelegate> * _Nonnull)presentingViewController frame:(CGRect)frame __attribute__((swift_name("init(showCTA:pageType:presentingViewController:frame:)")));
		[Export("initWithShowCTA:pageType:presentingViewController:frame:")]
		IntPtr Constructor(bool showCTA, AffirmPageType pageType, IAffirmPrequalDelegate presentingViewController, CGRect frame);

		// -(instancetype _Nonnull)initWithPromoID:(NSString * _Nullable)promoID showCTA:(BOOL)showCTA pageType:(AffirmPageType)pageType presentingViewController:(UIViewController<AffirmPrequalDelegate> * _Nonnull)presentingViewController frame:(CGRect)frame __attribute__((swift_name("init(promoID:showCTA:pageType:presentingViewController:frame:)")));
		[Export("initWithPromoID:showCTA:pageType:presentingViewController:frame:")]
		IntPtr Constructor([NullAllowed] string promoID, bool showCTA, AffirmPageType pageType, IAffirmPrequalDelegate presentingViewController, CGRect frame);

		// -(void)configureByHtmlStylingWithAmount:(NSDecimalNumber * _Nonnull)amount __attribute__((swift_name("configureByHtmlStyling(amount:)")));
		[Export("configureByHtmlStylingWithAmount:")]
		void ConfigureByHtmlStylingWithAmount(NSDecimalNumber amount);

		// -(void)configureByHtmlStylingWithAmount:(NSDecimalNumber * _Nonnull)amount affirmLogoType:(AffirmLogoType)affirmLogoType affirmColor:(AffirmColorType)affirmColor __attribute__((swift_name("configureByHtmlStyling(amount:affirmLogoType:affirmColor:)")));
		[Export("configureByHtmlStylingWithAmount:affirmLogoType:affirmColor:")]
		void ConfigureByHtmlStylingWithAmount(NSDecimalNumber amount, AffirmLogoType affirmLogoType, AffirmColorType affirmColor);

		// -(void)configureByHtmlStylingWithAmount:(NSDecimalNumber * _Nonnull)amount affirmLogoType:(AffirmLogoType)affirmLogoType affirmColor:(AffirmColorType)affirmColor remoteCssURL:(NSURL * _Nullable)remoteCssURL __attribute__((swift_name("configureByHtmlStyling(amount:affirmLogoType:affirmColor:remoteCssURL:)")));
		[Export("configureByHtmlStylingWithAmount:affirmLogoType:affirmColor:remoteCssURL:")]
		void ConfigureByHtmlStylingWithAmount(NSDecimalNumber amount, AffirmLogoType affirmLogoType, AffirmColorType affirmColor, [NullAllowed] NSUrl remoteCssURL);

		// -(void)configureByHtmlStylingWithAmount:(NSDecimalNumber * _Nonnull)amount affirmLogoType:(AffirmLogoType)affirmLogoType affirmColor:(AffirmColorType)affirmColor remoteFontURL:(NSURL * _Nullable)remoteFontURL remoteCssURL:(NSURL * _Nullable)remoteCssURL __attribute__((swift_name("configureByHtmlStyling(amount:affirmLogoType:affirmColor:remoteFontURL:remoteCssURL:)")));
		[Export("configureByHtmlStylingWithAmount:affirmLogoType:affirmColor:remoteFontURL:remoteCssURL:")]
		void ConfigureByHtmlStylingWithAmount(NSDecimalNumber amount, AffirmLogoType affirmLogoType, AffirmColorType affirmColor, [NullAllowed] NSUrl remoteFontURL, [NullAllowed] NSUrl remoteCssURL);

		// -(void)configureByHtmlStylingWithAmount:(NSDecimalNumber * _Nonnull)amount items:(NSArray<AffirmItem *> * _Nullable)items affirmLogoType:(AffirmLogoType)affirmLogoType affirmColor:(AffirmColorType)affirmColor remoteFontURL:(NSURL * _Nullable)remoteFontURL remoteCssURL:(NSURL * _Nullable)remoteCssURL __attribute__((swift_name("configureByHtmlStyling(amount:items:affirmLogoType:affirmColor:remoteFontURL:remoteCssURL:)")));
		[Export("configureByHtmlStylingWithAmount:items:affirmLogoType:affirmColor:remoteFontURL:remoteCssURL:")]
		void ConfigureByHtmlStylingWithAmount(NSDecimalNumber amount, [NullAllowed] AffirmItem[] items, AffirmLogoType affirmLogoType, AffirmColorType affirmColor, [NullAllowed] NSUrl remoteFontURL, [NullAllowed] NSUrl remoteCssURL);

		// -(void)configureWithAmount:(NSDecimalNumber * _Nonnull)amount affirmLogoType:(AffirmLogoType)affirmLogoType affirmColor:(AffirmColorType)affirmColor maxFontSize:(CGFloat)maxFontSize __attribute__((swift_name("configure(amount:affirmLogoType:affirmColor:maxFontSize:)")));
		[Export("configureWithAmount:affirmLogoType:affirmColor:maxFontSize:")]
		void ConfigureWithAmount(NSDecimalNumber amount, AffirmLogoType affirmLogoType, AffirmColorType affirmColor, nfloat maxFontSize);

		// -(void)configureWithAmount:(NSDecimalNumber * _Nonnull)amount affirmLogoType:(AffirmLogoType)affirmLogoType affirmColor:(AffirmColorType)affirmColor font:(UIFont * _Nonnull)font textColor:(UIColor * _Nonnull)textColor __attribute__((swift_name("configure(amount:affirmLogoType:affirmColor:font:textColor:)")));
		[Export("configureWithAmount:affirmLogoType:affirmColor:font:textColor:")]
		void ConfigureWithAmount(NSDecimalNumber amount, AffirmLogoType affirmLogoType, AffirmColorType affirmColor, UIFont font, UIColor textColor);

		// -(void)configureWithAmount:(NSDecimalNumber * _Nonnull)amount items:(NSArray<AffirmItem *> * _Nullable)items affirmLogoType:(AffirmLogoType)affirmLogoType affirmColor:(AffirmColorType)affirmColor font:(UIFont * _Nonnull)font textColor:(UIColor * _Nonnull)textColor __attribute__((swift_name("configure(amount:items:affirmLogoType:affirmColor:font:textColor:)")));
		[Export("configureWithAmount:items:affirmLogoType:affirmColor:font:textColor:")]
		void ConfigureWithAmount(NSDecimalNumber amount, [NullAllowed] AffirmItem[] items, AffirmLogoType affirmLogoType, AffirmColorType affirmColor, UIFont font, UIColor textColor);

		// -(void)configureWithHtmlString:(NSString * _Nonnull)htmlString amount:(NSDecimalNumber * _Nonnull)amount remoteFontURL:(NSURL * _Nullable)remoteFontURL remoteCssURL:(NSURL * _Nullable)remoteCssURL __attribute__((swift_name("configure(htmlString:amount:remoteFontURL:remoteCssURL:)")));
		[Export("configureWithHtmlString:amount:remoteFontURL:remoteCssURL:")]
		void ConfigureWithHtmlString(string htmlString, NSDecimalNumber amount, [NullAllowed] NSUrl remoteFontURL, [NullAllowed] NSUrl remoteCssURL);
	}

	// @interface Helper (AffirmPromotionalButton)
	[Category(AllowStaticMembers = true)]
	[BaseType(typeof(AffirmPromotionalButton))]
	interface AffirmPromotionalButtonHelper
	{
		// +(UIImage * _Nonnull)getAffirmDisplayForLogoType:(AffirmLogoType)logoType colorType:(AffirmColorType)colorType __attribute__((swift_name("getAffirmDisplay(logoType:colorType:)")));
		[Static]
		[Export("getAffirmDisplayForLogoType:colorType:")]
		UIImage GetAffirmDisplayForLogoType(AffirmLogoType logoType, AffirmColorType colorType);

		// +(CGSize)sizeForLogoType:(AffirmLogoType)logoType logoSize:(CGSize)logoSize height:(CGFloat)height __attribute__((swift_name("size(logoType:logoSize:height:)")));
		[Static]
		[Export("sizeForLogoType:logoSize:height:")]
		CGSize SizeForLogoType(AffirmLogoType logoType, CGSize logoSize, nfloat height);

		// +(NSAttributedString * _Nonnull)appendLogo:(UIImage * _Nonnull)logo toText:(NSString * _Nonnull)text font:(UIFont * _Nonnull)font textColor:(UIColor * _Nonnull)textColor logoType:(AffirmLogoType)logoType __attribute__((swift_name("append(logo:text:font:textColor:logoType:)")));
		[Static]
		[Export("appendLogo:toText:font:textColor:logoType:")]
		NSAttributedString AppendLogo(UIImage logo, string text, UIFont font, UIColor textColor, AffirmLogoType logoType);
	}

	// @interface AffirmDataHandler : NSObject
	[BaseType(typeof(NSObject))]
	interface AffirmDataHandler
	{
		// +(void)getPromoMessageWithPromoID:(NSString * _Nullable)promoID amount:(NSDecimalNumber * _Nonnull)amount showCTA:(BOOL)showCTA pageType:(AffirmPageType)pageType logoType:(AffirmLogoType)logoType colorType:(AffirmColorType)colorType font:(UIFont * _Nonnull)font textColor:(UIColor * _Nonnull)textColor delegate:(id<AffirmPrequalDelegate> _Nonnull)delegate completionHandler:(void (^ _Nonnull)(NSAttributedString * _Nullable, UIViewController * _Nullable, NSError * _Nullable))completionHandler __attribute__((swift_name("getPromoMessage(promoID:amount:showCTA:pageType:logoType:colorType:font:textColor:presentingViewController:completionHandler:)")));
		[Static]
		[Export("getPromoMessageWithPromoID:amount:showCTA:pageType:logoType:colorType:font:textColor:delegate:completionHandler:")]
		void GetPromoMessageWithPromoID([NullAllowed] string promoID, NSDecimalNumber amount, bool showCTA, AffirmPageType pageType, AffirmLogoType logoType, AffirmColorType colorType, UIFont font, UIColor textColor, IAffirmPrequalDelegate @delegate, Action<NSAttributedString, UIViewController, NSError> completionHandler);

		// +(void)getPromoMessageWithPromoID:(NSString * _Nullable)promoID amount:(NSDecimalNumber * _Nonnull)amount showCTA:(BOOL)showCTA pageType:(AffirmPageType)pageType logoType:(AffirmLogoType)logoType colorType:(AffirmColorType)colorType font:(UIFont * _Nonnull)font textColor:(UIColor * _Nonnull)textColor presentingViewController:(id<AffirmPrequalDelegate> _Nonnull)delegate withNavigation:(BOOL)withNavigation completionHandler:(void (^ _Nonnull)(NSAttributedString * _Nullable, UIViewController * _Nullable, NSError * _Nullable))completionHandler __attribute__((swift_name("getPromoMessage(promoID:amount:showCTA:pageType:logoType:colorType:font:textColor:presentingViewController:withNavigation:completionHandler:)")));
		[Static]
		[Export("getPromoMessageWithPromoID:amount:showCTA:pageType:logoType:colorType:font:textColor:presentingViewController:withNavigation:completionHandler:")]
		void GetPromoMessageWithPromoID([NullAllowed] string promoID, NSDecimalNumber amount, bool showCTA, AffirmPageType pageType, AffirmLogoType logoType, AffirmColorType colorType, UIFont font, UIColor textColor, IAffirmPrequalDelegate @delegate, bool withNavigation, Action<NSAttributedString, UIViewController, NSError> completionHandler);

		// +(void)getPromoMessageWithPromoID:(NSString * _Nullable)promoID amount:(NSDecimalNumber * _Nonnull)amount showCTA:(BOOL)showCTA pageType:(AffirmPageType)pageType logoType:(AffirmLogoType)logoType colorType:(AffirmColorType)colorType font:(UIFont * _Nonnull)font textColor:(UIColor * _Nonnull)textColor presentingViewController:(id<AffirmPrequalDelegate> _Nonnull)delegate withNavigation:(BOOL)withNavigation withHtmlValue:(BOOL)withHtmlValue completionHandler:(void (^ _Nonnull)(NSAttributedString * _Nullable, NSString * _Nullable, UIViewController * _Nullable, NSError * _Nullable))completionHandler __attribute__((swift_name("getPromoMessage(promoID:amount:showCTA:pageType:logoType:colorType:font:textColor:presentingViewController:withNavigation:withHtmlValue:completionHandler:)")));
		[Static]
		[Export("getPromoMessageWithPromoID:amount:showCTA:pageType:logoType:colorType:font:textColor:presentingViewController:withNavigation:withHtmlValue:completionHandler:")]
		void GetPromoMessageWithPromoID([NullAllowed] string promoID, NSDecimalNumber amount, bool showCTA, AffirmPageType pageType, AffirmLogoType logoType, AffirmColorType colorType, UIFont font, UIColor textColor, IAffirmPrequalDelegate @delegate, bool withNavigation, bool withHtmlValue, Action<NSAttributedString, NSString, UIViewController, NSError> completionHandler);

		// +(void)getPromoMessageWithPromoID:(NSString * _Nullable)promoID amount:(NSDecimalNumber * _Nonnull)amount items:(NSArray<AffirmItem *> * _Nullable)items showCTA:(BOOL)showCTA pageType:(AffirmPageType)pageType logoType:(AffirmLogoType)logoType colorType:(AffirmColorType)colorType font:(UIFont * _Nonnull)font textColor:(UIColor * _Nonnull)textColor presentingViewController:(id<AffirmPrequalDelegate> _Nonnull)delegate withNavigation:(BOOL)withNavigation withHtmlValue:(BOOL)withHtmlValue completionHandler:(void (^ _Nonnull)(NSAttributedString * _Nullable, NSString * _Nullable, UIViewController * _Nullable, NSError * _Nullable))completionHandler __attribute__((swift_name("getPromoMessage(promoID:amount:items:showCTA:pageType:logoType:colorType:font:textColor:presentingViewController:withNavigation:withHtmlValue:completionHandler:)")));
		[Static]
		[Export("getPromoMessageWithPromoID:amount:items:showCTA:pageType:logoType:colorType:font:textColor:presentingViewController:withNavigation:withHtmlValue:completionHandler:")]
		void GetPromoMessageWithPromoID([NullAllowed] string promoID, NSDecimalNumber amount, [NullAllowed] AffirmItem[] items, bool showCTA, AffirmPageType pageType, AffirmLogoType logoType, AffirmColorType colorType, UIFont font, UIColor textColor, IAffirmPrequalDelegate @delegate, bool withNavigation, bool withHtmlValue, Action<NSAttributedString, NSString, UIViewController, NSError> completionHandler);
	}

	// @interface AffirmDiscount : NSObject <AffirmJSONifiable, NSCopying>
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmDiscount : AffirmJSONifiable, INSCopying
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSDecimalNumber * _Nonnull amount;
		[Export("amount", ArgumentSemantic.Copy)]
		NSDecimalNumber Amount { get; }

		// +(AffirmDiscount * _Nonnull)discountWithName:(NSString * _Nonnull)name amount:(NSDecimalNumber * _Nonnull)amount __attribute__((swift_name("discount(name:amount:)")));
		[Static]
		[Export("discountWithName:amount:")]
		AffirmDiscount DiscountWithName(string name, NSDecimalNumber amount);

		// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name amount:(NSDecimalNumber * _Nonnull)amount __attribute__((swift_name("init(name:amount:)")));
		[Export("initWithName:amount:")]
		IntPtr Constructor(string name, NSDecimalNumber amount);
	}

	// @interface AffirmEligibilityViewController : UIViewController
	[BaseType(typeof(UIViewController))]
	[DisableDefaultCtor]
	interface AffirmEligibilityViewController
	{
		[Wrap("WeakDelegate")]
		[NullAllowed]
		IAffirmCheckoutDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<AffirmCheckoutDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readonly, copy, nonatomic) AffirmCheckout * _Nonnull checkout;
		[Export("checkout", ArgumentSemantic.Copy)]
		AffirmCheckout Checkout { get; }

		// @property (readonly, nonatomic) BOOL getReasonCodes;
		[Export("getReasonCodes")]
		bool GetReasonCodes { get; }

		// +(UINavigationController * _Nonnull)startCheckoutWithNavigation:(AffirmCheckout * _Nonnull)checkout getReasonCodes:(BOOL)getReasonCodes delegate:(id<AffirmCheckoutDelegate> _Nonnull)delegate __attribute__((swift_name("startNavigation(checkout:getReasonCodes:delegate:)")));
		[Static]
		[Export("startCheckoutWithNavigation:getReasonCodes:delegate:")]
		UINavigationController StartCheckoutWithNavigation(AffirmCheckout checkout, bool getReasonCodes, IAffirmCheckoutDelegate @delegate);
	}

	// @interface AffirmFontLoader : NSObject
	[BaseType(typeof(NSObject))]
	interface AffirmFontLoader
	{
		// +(void)loadFontIfNeeded;
		[Static]
		[Export("loadFontIfNeeded")]
		void LoadFontIfNeeded();
	}

	// @interface AffirmHowToViewController : UIViewController
	[BaseType(typeof(UIViewController))]
	interface AffirmHowToViewController
	{
	}

	// @interface AffirmItem : NSObject <AffirmJSONifiable, NSCopying>
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmItem : AffirmJSONifiable, INSCopying
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull SKU;
		[Export("SKU")]
		string SKU { get; }

		// @property (readonly, copy, nonatomic) NSDecimalNumber * _Nonnull unitPrice;
		[Export("unitPrice", ArgumentSemantic.Copy)]
		NSDecimalNumber UnitPrice { get; }

		// @property (readonly, nonatomic) NSInteger quantity;
		[Export("quantity")]
		nint Quantity { get; }

		// @property (readonly, copy, nonatomic) NSURL * _Nonnull URL;
		[Export("URL", ArgumentSemantic.Copy)]
		NSUrl URL { get; }

		// @property (readonly, copy, nonatomic) NSURL * _Nonnull imageURL;
		[Export("imageURL", ArgumentSemantic.Copy)]
		NSUrl ImageURL { get; }

		// +(AffirmItem * _Nonnull)itemWithName:(NSString * _Nonnull)name SKU:(NSString * _Nonnull)SKU unitPrice:(NSDecimalNumber * _Nonnull)unitPrice quantity:(NSInteger)quantity URL:(NSURL * _Nonnull)URL __attribute__((swift_name("item(name:sku:unitPrice:quantity:url:)")));
		[Static]
		[Export("itemWithName:SKU:unitPrice:quantity:URL:")]
		AffirmItem ItemWithName(string name, string SKU, NSDecimalNumber unitPrice, nint quantity, NSUrl URL);

		// -(instancetype _Nonnull)initWithName:(NSString * _Nonnull)name SKU:(NSString * _Nonnull)SKU unitPrice:(NSDecimalNumber * _Nonnull)unitPrice quantity:(NSInteger)quantity URL:(NSURL * _Nonnull)URL __attribute__((swift_name("init(name:sku:unitPrice:quantity:url:)")));
		[Export("initWithName:SKU:unitPrice:quantity:URL:")]
		IntPtr Constructor(string name, string SKU, NSDecimalNumber unitPrice, nint quantity, NSUrl URL);
	}

	// @interface AffirmLogger : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmLogger
	{
		// @property (nonatomic) BOOL enableLogPrinted;
		[Export("enableLogPrinted")]
		bool EnableLogPrinted { get; set; }

		// @property (readonly, nonatomic, strong, class) NS_SWIFT_NAME(shared) AffirmLogger * sharedInstance __attribute__((swift_name("shared")));
		[Static]
		[Export("sharedInstance", ArgumentSemantic.Strong)]
		AffirmLogger SharedInstance { get; }

		// -(void)logException:(NSString * _Nonnull)message;
		[Export("logException:")]
		void LogException(string message);

		// -(void)logEvent:(NSString * _Nonnull)name;
		[Export("logEvent:")]
		void LogEvent(string name);

		// -(void)logEvent:(NSString * _Nonnull)name parameters:(NSDictionary * _Nonnull)parameters;
		[Export("logEvent:parameters:")]
		void LogEvent(string name, NSDictionary parameters);

		// -(void)trackEvent:(NSString * _Nonnull)name;
		[Export("trackEvent:")]
		void TrackEvent(string name);

		// -(void)trackEvent:(NSString * _Nonnull)name parameters:(NSDictionary * _Nonnull)parameters;
		[Export("trackEvent:parameters:")]
		void TrackEvent(string name, NSDictionary parameters);
	}

	// @interface AffirmOrder : NSObject <AffirmJSONifiable, NSCopying>
	[BaseType(typeof(NSObject))]
	interface AffirmOrder : AffirmJSONifiable, INSCopying
	{
		// @property (copy, nonatomic) NSString * _Nonnull storeName;
		[Export("storeName")]
		string StoreName { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable checkoutId;
		[NullAllowed, Export("checkoutId")]
		string CheckoutId { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable coupon;
		[NullAllowed, Export("coupon")]
		string Coupon { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable currency;
		[NullAllowed, Export("currency")]
		string Currency { get; set; }

		// @property (copy, nonatomic) NSDecimalNumber * _Nullable discount;
		[NullAllowed, Export("discount", ArgumentSemantic.Copy)]
		NSDecimalNumber Discount { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull orderId;
		[Export("orderId")]
		string OrderId { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable paymentMethod;
		[NullAllowed, Export("paymentMethod")]
		string PaymentMethod { get; set; }

		// @property (copy, nonatomic) NSDecimalNumber * _Nullable revenue;
		[NullAllowed, Export("revenue", ArgumentSemantic.Copy)]
		NSDecimalNumber Revenue { get; set; }

		// @property (copy, nonatomic) NSDecimalNumber * _Nullable shipping;
		[NullAllowed, Export("shipping", ArgumentSemantic.Copy)]
		NSDecimalNumber Shipping { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable shippingMethod;
		[NullAllowed, Export("shippingMethod")]
		string ShippingMethod { get; set; }

		// @property (copy, nonatomic) NSDecimalNumber * _Nullable tax;
		[NullAllowed, Export("tax", ArgumentSemantic.Copy)]
		NSDecimalNumber Tax { get; set; }

		// @property (copy, nonatomic) NSDecimalNumber * _Nullable total;
		[NullAllowed, Export("total", ArgumentSemantic.Copy)]
		NSDecimalNumber Total { get; set; }

		// -(instancetype _Nonnull)initWithStoreName:(NSString * _Nonnull)storeName checkoutId:(NSString * _Nullable)checkoutId coupon:(NSString * _Nullable)coupon currency:(NSString * _Nullable)currency discount:(NSDecimalNumber * _Nullable)discount orderId:(NSString * _Nonnull)orderId paymentMethod:(NSString * _Nullable)paymentMethod revenue:(NSDecimalNumber * _Nullable)revenue shipping:(NSDecimalNumber * _Nullable)shipping shippingMethod:(NSString * _Nullable)shippingMethod tax:(NSDecimalNumber * _Nullable)tax total:(NSDecimalNumber * _Nullable)total __attribute__((swift_name("init(storeName:checkoutId:coupon:currency:discount:orderId:paymentMethod:revenue:shipping:shippingMethod:tax:total:)")));
		[Export("initWithStoreName:checkoutId:coupon:currency:discount:orderId:paymentMethod:revenue:shipping:shippingMethod:tax:total:")]
		IntPtr Constructor(string storeName, [NullAllowed] string checkoutId, [NullAllowed] string coupon, [NullAllowed] string currency, [NullAllowed] NSDecimalNumber discount, string orderId, [NullAllowed] string paymentMethod, [NullAllowed] NSDecimalNumber revenue, [NullAllowed] NSDecimalNumber shipping, [NullAllowed] string shippingMethod, [NullAllowed] NSDecimalNumber tax, [NullAllowed] NSDecimalNumber total);

		// +(AffirmOrder * _Nonnull)orderWithStoreName:(NSString * _Nonnull)storeName checkoutId:(NSString * _Nullable)checkoutId coupon:(NSString * _Nullable)coupon currency:(NSString * _Nullable)currency discount:(NSDecimalNumber * _Nullable)discount orderId:(NSString * _Nonnull)orderId paymentMethod:(NSString * _Nullable)paymentMethod revenue:(NSDecimalNumber * _Nullable)revenue shipping:(NSDecimalNumber * _Nullable)shipping shippingMethod:(NSString * _Nullable)shippingMethod tax:(NSDecimalNumber * _Nullable)tax total:(NSDecimalNumber * _Nullable)total __attribute__((swift_name("order(storeName:checkoutId:coupon:currency:discount:orderId:paymentMethod:revenue:shipping:shippingMethod:tax:total:)")));
		[Static]
		[Export("orderWithStoreName:checkoutId:coupon:currency:discount:orderId:paymentMethod:revenue:shipping:shippingMethod:tax:total:")]
		AffirmOrder OrderWithStoreName(string storeName, [NullAllowed] string checkoutId, [NullAllowed] string coupon, [NullAllowed] string currency, [NullAllowed] NSDecimalNumber discount, string orderId, [NullAllowed] string paymentMethod, [NullAllowed] NSDecimalNumber revenue, [NullAllowed] NSDecimalNumber shipping, [NullAllowed] string shippingMethod, [NullAllowed] NSDecimalNumber tax, [NullAllowed] NSDecimalNumber total);

		// +(AffirmOrder * _Nonnull)orderWithStoreName:(NSString * _Nonnull)storeName orderId:(NSString * _Nonnull)orderId __attribute__((swift_name("order(storeName:orderId:)")));
		[Static]
		[Export("orderWithStoreName:orderId:")]
		AffirmOrder OrderWithStoreName(string storeName, string orderId);
	}

	// @interface AffirmOrderTrackerViewController : UIViewController
	[BaseType(typeof(UIViewController))]
	[DisableDefaultCtor]
	interface AffirmOrderTrackerViewController
	{
		// @property (readonly, copy, nonatomic) AffirmOrder * _Nonnull order;
		[Export("order", ArgumentSemantic.Copy)]
		AffirmOrder Order { get; }

		// @property (readonly, copy, nonatomic) NSArray<AffirmProduct *> * _Nonnull products;
		[Export("products", ArgumentSemantic.Copy)]
		AffirmProduct[] Products { get; }

		// +(void)trackOrder:(AffirmOrder * _Nonnull)order products:(NSArray<AffirmProduct *> * _Nonnull)products __attribute__((swift_name("track(order:products:)")));
		[Static]
		[Export("trackOrder:products:")]
		void TrackOrder(AffirmOrder order, AffirmProduct[] products);
	}

	// @interface AffirmPopupViewController : AffirmBaseWebViewController
	[BaseType(typeof(AffirmBaseWebViewController))]
	interface AffirmPopupViewController
	{
		// -(instancetype _Nonnull)initWithURL:(NSURL * _Nonnull)URL;
		[Export("initWithURL:")]
		IntPtr Constructor(NSUrl URL);
	}

	// @interface AffirmPrequalModalViewController : AffirmBaseWebViewController
	[BaseType(typeof(AffirmBaseWebViewController))]
	[DisableDefaultCtor]
	interface AffirmPrequalModalViewController
	{
		[Wrap("WeakDelegate")]
		[NullAllowed]
		IAffirmPrequalDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<AffirmPrequalDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(instancetype _Nonnull)initWithURL:(NSURL * _Nonnull)URL delegate:(id<AffirmPrequalDelegate> _Nonnull)delegate __attribute__((swift_name("init(url:delegate:)"))) __attribute__((objc_designated_initializer));
		[Export("initWithURL:delegate:")]
		[DesignatedInitializer]
		IntPtr Constructor(NSUrl URL, IAffirmPrequalDelegate @delegate);
	}

	// @interface AffirmProduct : NSObject <AffirmJSONifiable, NSCopying>
	[BaseType(typeof(NSObject))]
	interface AffirmProduct : AffirmJSONifiable, INSCopying
	{
		// @property (copy, nonatomic) NSString * _Nullable brand;
		[NullAllowed, Export("brand")]
		string Brand { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable category;
		[NullAllowed, Export("category")]
		string Category { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable coupon;
		[NullAllowed, Export("coupon")]
		string Coupon { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable name;
		[NullAllowed, Export("name")]
		string Name { get; set; }

		// @property (copy, nonatomic) NSDecimalNumber * _Nullable price;
		[NullAllowed, Export("price", ArgumentSemantic.Copy)]
		NSDecimalNumber Price { get; set; }

		// @property (copy, nonatomic) NSString * _Nonnull productId;
		[Export("productId")]
		string ProductId { get; set; }

		// @property (nonatomic) NSInteger quantity;
		[Export("quantity")]
		nint Quantity { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable variant;
		[NullAllowed, Export("variant")]
		string Variant { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable currency;
		[NullAllowed, Export("currency")]
		string Currency { get; set; }

		// -(instancetype _Nonnull)initWithBrand:(NSString * _Nullable)brand category:(NSString * _Nullable)category coupon:(NSString * _Nullable)coupon name:(NSString * _Nullable)name price:(NSDecimalNumber * _Nullable)price productId:(NSString * _Nonnull)productId quantity:(NSInteger)quantity variant:(NSString * _Nullable)variant currency:(NSString * _Nullable)currency __attribute__((swift_name("init(brand:category:coupon:name:price:productId:quantity:variant:currency:)")));
		[Export("initWithBrand:category:coupon:name:price:productId:quantity:variant:currency:")]
		IntPtr Constructor([NullAllowed] string brand, [NullAllowed] string category, [NullAllowed] string coupon, [NullAllowed] string name, [NullAllowed] NSDecimalNumber price, string productId, nint quantity, [NullAllowed] string variant, [NullAllowed] string currency);

		// +(AffirmProduct * _Nonnull)productWithBrand:(NSString * _Nullable)brand category:(NSString * _Nullable)category coupon:(NSString * _Nullable)coupon name:(NSString * _Nullable)name price:(NSDecimalNumber * _Nullable)price productId:(NSString * _Nonnull)productId quantity:(NSInteger)quantity variant:(NSString * _Nullable)variant currency:(NSString * _Nullable)currency __attribute__((swift_name("product(brand:category:coupon:name:price:productId:quantity:variant:currency:)")));
		[Static]
		[Export("productWithBrand:category:coupon:name:price:productId:quantity:variant:currency:")]
		AffirmProduct ProductWithBrand([NullAllowed] string brand, [NullAllowed] string category, [NullAllowed] string coupon, [NullAllowed] string name, [NullAllowed] NSDecimalNumber price, string productId, nint quantity, [NullAllowed] string variant, [NullAllowed] string currency);

		// +(AffirmProduct * _Nonnull)productWithProductId:(NSString * _Nonnull)productId __attribute__((swift_name("product(productId:)")));
		[Static]
		[Export("productWithProductId:")]
		AffirmProduct ProductWithProductId(string productId);
	}

	// @interface AffirmPromoModalViewController : AffirmBaseWebViewController
	[BaseType(typeof(AffirmBaseWebViewController))]
	[DisableDefaultCtor]
	interface AffirmPromoModalViewController
	{
		[Wrap("WeakDelegate")]
		[NullAllowed]
		IAffirmPrequalDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<AffirmPrequalDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(instancetype _Nonnull)initWithPromoId:(NSString * _Nullable)promoId amount:(NSDecimalNumber * _Nonnull)amount delegate:(id<AffirmPrequalDelegate> _Nonnull)delegate __attribute__((swift_name("init(promoId:amount:delegate:)"))) __attribute__((objc_designated_initializer));
		[Export("initWithPromoId:amount:delegate:")]
		[DesignatedInitializer]
		IntPtr Constructor([NullAllowed] string promoId, NSDecimalNumber amount, IAffirmPrequalDelegate @delegate);

		// -(instancetype _Nonnull)initWithPromoId:(NSString * _Nullable)promoId amount:(NSDecimalNumber * _Nonnull)amount pageType:(AffirmPageType)pageType delegate:(id<AffirmPrequalDelegate> _Nonnull)delegate __attribute__((swift_name("init(promoId:amount:pageType:delegate:)"))) __attribute__((objc_designated_initializer));
		[Export("initWithPromoId:amount:pageType:delegate:")]
		[DesignatedInitializer]
		IntPtr Constructor([NullAllowed] string promoId, NSDecimalNumber amount, AffirmPageType pageType, IAffirmPrequalDelegate @delegate);
	}

	// @interface AffirmReasonCode : NSObject
	[BaseType(typeof(NSObject))]
	interface AffirmReasonCode
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull reason;
		[Export("reason")]
		string Reason { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull checkout_token;
		[Export("checkout_token")]
		string CheckoutToken { get; }

		// +(AffirmReasonCode * _Nonnull)reasonCodeWithDict:(NSDictionary * _Nonnull)dict __attribute__((swift_name("reasonCode(dict:)")));
		[Static]
		[Export("reasonCodeWithDict:")]
		AffirmReasonCode ReasonCodeWithDict(NSDictionary dict);

		// -(instancetype _Nonnull)initWithDict:(NSDictionary * _Nonnull)dict __attribute__((swift_name("init(dict:)")));
		[Export("initWithDict:")]
		IntPtr Constructor(NSDictionary dict);
	}

	// @interface AffirmLogRequest : NSObject <AffirmRequestProtocol>
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmLogRequest : AffirmRequestProtocol
	{
		// @property (readonly, nonatomic) NSInteger logCount;
		[Export("logCount")]
		nint LogCount { get; }

		// @property (readonly, copy, nonatomic) NSDictionary * _Nonnull eventParameters;
		[Export("eventParameters", ArgumentSemantic.Copy)]
		NSDictionary EventParameters { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull eventName;
		[Export("eventName")]
		string EventName { get; }

		// -(instancetype _Nonnull)initWithEventName:(NSString * _Nonnull)eventName eventParameters:(NSDictionary * _Nonnull)eventParameters logCount:(NSInteger)logCount;
		[Export("initWithEventName:eventParameters:logCount:")]
		IntPtr Constructor(string eventName, NSDictionary eventParameters, nint logCount);
	}

	// @interface AffirmPromoRequest : NSObject <AffirmRequestProtocol>
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmPromoRequest : AffirmRequestProtocol
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull publicKey;
		[Export("publicKey")]
		string PublicKey { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull promoId;
		[Export("promoId")]
		string PromoId { get; }

		// @property (readonly, copy, nonatomic) NSDecimalNumber * _Nonnull amount;
		[Export("amount", ArgumentSemantic.Copy)]
		NSDecimalNumber Amount { get; }

		// @property (readonly, nonatomic) BOOL showCTA;
		[Export("showCTA")]
		bool ShowCTA { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable pageType;
		[NullAllowed, Export("pageType")]
		string PageType { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull logoType;
		[Export("logoType")]
		string LogoType { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull logoColor;
		[Export("logoColor")]
		string LogoColor { get; }

		// @property (readonly, copy, nonatomic) NSArray<AffirmItem *> * _Nonnull items;
		[Export("items", ArgumentSemantic.Copy)]
		AffirmItem[] Items { get; }

		// -(instancetype _Nonnull)initWithPublicKey:(NSString * _Nonnull)publicKey promoId:(NSString * _Nonnull)promoId amount:(NSDecimalNumber * _Nonnull)amount showCTA:(BOOL)showCTA pageType:(NSString * _Nullable)pageType logoType:(NSString * _Nullable)logoType logoColor:(NSString * _Nullable)logoColor;
		[Export("initWithPublicKey:promoId:amount:showCTA:pageType:logoType:logoColor:")]
		IntPtr Constructor(string publicKey, string promoId, NSDecimalNumber amount, bool showCTA, [NullAllowed] string pageType, [NullAllowed] string logoType, [NullAllowed] string logoColor);

		// -(instancetype _Nonnull)initWithPublicKey:(NSString * _Nonnull)publicKey promoId:(NSString * _Nonnull)promoId amount:(NSDecimalNumber * _Nonnull)amount showCTA:(BOOL)showCTA pageType:(NSString * _Nullable)pageType logoType:(NSString * _Nullable)logoType logoColor:(NSString * _Nullable)logoColor items:(NSArray<AffirmItem *> * _Nullable)items;
		[Export("initWithPublicKey:promoId:amount:showCTA:pageType:logoType:logoColor:items:")]
		IntPtr Constructor(string publicKey, string promoId, NSDecimalNumber amount, bool showCTA, [NullAllowed] string pageType, [NullAllowed] string logoType, [NullAllowed] string logoColor, [NullAllowed] AffirmItem[] items);
	}

	// @interface AffirmCheckoutRequest : NSObject <AffirmRequestProtocol>
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmCheckoutRequest : AffirmRequestProtocol
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull publicKey;
		[Export("publicKey")]
		string PublicKey { get; }

		// @property (readonly, copy, nonatomic) AffirmCheckout * _Nonnull checkout;
		[Export("checkout", ArgumentSemantic.Copy)]
		AffirmCheckout Checkout { get; }

		// @property (readonly, nonatomic) BOOL useVCN;
		[Export("useVCN")]
		bool UseVCN { get; }

		// @property (readonly, nonatomic) NSInteger cardAuthWindow;
		[Export("cardAuthWindow")]
		nint CardAuthWindow { get; }

		// -(instancetype _Nonnull)initWithPublicKey:(NSString * _Nonnull)publicKey checkout:(AffirmCheckout * _Nonnull)checkout useVCN:(BOOL)useVCN cardAuthWindow:(NSInteger)cardAuthWindow;
		[Export("initWithPublicKey:checkout:useVCN:cardAuthWindow:")]
		IntPtr Constructor(string publicKey, AffirmCheckout checkout, bool useVCN, nint cardAuthWindow);
	}

	// @interface AffirmPromoResponse : NSObject <AffirmResponseProtocol>
	[BaseType(typeof(NSObject))]
	interface AffirmPromoResponse : AffirmResponseProtocol
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull htmlAla;
		[Export("htmlAla")]
		string HtmlAla { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull ala;
		[Export("ala")]
		string Ala { get; }

		// @property (readonly, nonatomic) BOOL showPrequal;
		[Export("showPrequal")]
		bool ShowPrequal { get; }
	}

	// @interface AffirmCheckoutResponse : NSObject <AffirmResponseProtocol>
	[BaseType(typeof(NSObject))]
	interface AffirmCheckoutResponse : AffirmResponseProtocol
	{
		// @property (readonly, copy, nonatomic) NSURL * _Nonnull redirectURL;
		[Export("redirectURL", ArgumentSemantic.Copy)]
		NSUrl RedirectURL { get; }

		// -(NSDictionary * _Nonnull)dictionary;
		[Export("dictionary")]
		// [Verify (MethodToProperty)]
		NSDictionary Dictionary { get; }
	}

	// @interface AffirmErrorResponse : NSObject <AffirmResponseProtocol>
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmErrorResponse : AffirmResponseProtocol
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull message;
		[Export("message")]
		string Message { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull code;
		[Export("code")]
		string Code { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSNumber * _Nonnull statusCode;
		[Export("statusCode", ArgumentSemantic.Copy)]
		NSNumber StatusCode { get; }

		// -(instancetype _Nonnull)initWithMessage:(NSString * _Nonnull)message code:(NSString * _Nonnull)code type:(NSString * _Nonnull)type statusCode:(NSNumber * _Nonnull)statusCode;
		[Export("initWithMessage:code:type:statusCode:")]
		IntPtr Constructor(string message, string code, string type, NSNumber statusCode);

		// -(NSDictionary * _Nonnull)dictionary;
		[Export("dictionary")]
		// [Verify (MethodToProperty)]
		NSDictionary Dictionary { get; }
	}

	// @interface AffirmShippingDetail : NSObject <AffirmJSONifiable, NSCopying>
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface AffirmShippingDetail : AffirmJSONifiable, INSCopying
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable phoneNumber;
		[NullAllowed, Export("phoneNumber")]
		string PhoneNumber { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable email;
		[NullAllowed, Export("email")]
		string Email { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull line1;
		[Export("line1")]
		string Line1 { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull line2;
		[Export("line2")]
		string Line2 { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull city;
		[Export("city")]
		string City { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull state;
		[Export("state")]
		string State { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull zipCode;
		[Export("zipCode")]
		string ZipCode { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull countryCode;
		[Export("countryCode")]
		string CountryCode { get; }

		// +(AffirmShippingDetail * _Nonnull)shippingDetailWithName:(NSString * _Nonnull)name addressWithLine1:(NSString * _Nonnull)line1 line2:(NSString * _Nonnull)line2 city:(NSString * _Nonnull)city state:(NSString * _Nonnull)state zipCode:(NSString * _Nonnull)zipCode countryCode:(NSString * _Nonnull)countryCode __attribute__((swift_name("shippingDetail(name:line1:line2:city:state:zipCode:countryCode:)")));
		[Static]
		[Export("shippingDetailWithName:addressWithLine1:line2:city:state:zipCode:countryCode:")]
		AffirmShippingDetail ShippingDetailWithName(string name, string line1, string line2, string city, string state, string zipCode, string countryCode);

		// +(AffirmShippingDetail * _Nonnull)shippingDetailWithName:(NSString * _Nonnull)name email:(NSString * _Nullable)email phoneNumber:(NSString * _Nullable)phoneNumber addressWithLine1:(NSString * _Nonnull)line1 line2:(NSString * _Nonnull)line2 city:(NSString * _Nonnull)city state:(NSString * _Nonnull)state zipCode:(NSString * _Nonnull)zipCode countryCode:(NSString * _Nonnull)countryCode __attribute__((swift_name("shippingDetail(name:email:phoneNumber:line1:line2:city:state:zipCode:countryCode:)")));
		[Static]
		[Export("shippingDetailWithName:email:phoneNumber:addressWithLine1:line2:city:state:zipCode:countryCode:")]
		AffirmShippingDetail ShippingDetailWithName(string name, [NullAllowed] string email, [NullAllowed] string phoneNumber, string line1, string line2, string city, string state, string zipCode, string countryCode);

		// -(instancetype _Nonnull)initShippingDetailWithName:(NSString * _Nonnull)name email:(NSString * _Nullable)email phoneNumber:(NSString * _Nullable)phoneNumber addressWithLine1:(NSString * _Nonnull)line1 line2:(NSString * _Nonnull)line2 city:(NSString * _Nonnull)city state:(NSString * _Nonnull)state zipCode:(NSString * _Nonnull)zipCode countryCode:(NSString * _Nonnull)countryCode __attribute__((swift_name("init(name:email:phoneNumber:line1:line2:city:state:zipCode:countryCode:)")));
		[Export("initShippingDetailWithName:email:phoneNumber:addressWithLine1:line2:city:state:zipCode:countryCode:")]
		IntPtr Constructor(string name, [NullAllowed] string email, [NullAllowed] string phoneNumber, string line1, string line2, string city, string state, string zipCode, string countryCode);
	}

	// @interface Utils (NSDictionary)
	[Category]
	[BaseType(typeof(NSDictionary))]
	interface NSDictionaryUtils
	{
		// -(NSString * _Nonnull)queryURLEncoding;
		[Export("queryURLEncoding")]
		// [Verify (MethodToProperty)]
		string QueryURLEncoding();

		// -(NSError * _Nonnull)convertToNSErrorWithCode:(NSNumber * _Nonnull)code;
		[Export("convertToNSErrorWithCode:")]
		NSError ConvertToNSErrorWithCode(NSNumber code);

		// -(NSError * _Nonnull)convertToNSError;
		[Export("convertToNSError")]
		// [Verify (MethodToProperty)]
		NSError ConvertToNSError();
	}

	// @interface Utils (NSBundle)
	[Category(AllowStaticMembers = true)]
	[BaseType(typeof(NSBundle))]
	interface NSBundleUtils
	{
		// +(NSBundle * _Nonnull)sdkBundle;
		[Static]
		[Export("sdkBundle")]
		// [Verify (MethodToProperty)]
		NSBundle SdkBundle();

		// +(NSBundle * _Nonnull)resourceBundle;
		[Static]
		[Export("resourceBundle")]
		// [Verify (MethodToProperty)]
		NSBundle ResourceBundle();
	}

	// @interface Utils (NSString)
	[Category]
	[BaseType(typeof(NSString))]
	interface NSStringUtils
	{
		// -(NSDictionary * _Nonnull)convertToDictionary;
		[Export("convertToDictionary")]
		// [Verify (MethodToProperty)]
		NSDictionary ConvertToDictionary();

		// -(NSString * _Nonnull)stringByRemovingIllegalCharacters;
		[Export("stringByRemovingIllegalCharacters")]
		// [Verify (MethodToProperty)]
		string StringByRemovingIllegalCharacters();

		// -(NSDecimalNumber * _Nonnull)currencyDecimal;
		[Export("currencyDecimal")]
		// [Verify (MethodToProperty)]
		NSDecimalNumber CurrencyDecimal();
	}

	// @interface Utils (NSDecimalNumber)
	[Category]
	[BaseType(typeof(NSDecimalNumber))]
	interface NSDecimalNumberUtils
	{
		// -(NSDecimalNumber * _Nonnull)toIntegerCents;
		[Export("toIntegerCents")]
		// [Verify (MethodToProperty)]
		NSDecimalNumber ToIntegerCents();

		// -(NSString * _Nonnull)formattedString;
		[Export("formattedString")]
		// [Verify (MethodToProperty)]
		string FormattedString();
	}

	// @interface AffirmValidationUtils : NSObject
	[BaseType(typeof(NSObject))]
	interface AffirmValidationUtils
	{
		// +(void)checkNotNil:(id _Nonnull)value name:(NSString * _Nonnull)name;
		[Static]
		[Export("checkNotNil:name:")]
		void CheckNotNil(NSObject value, string name);

		// +(void)checkNotNegative:(NSDecimalNumber * _Nonnull)value name:(NSString * _Nonnull)name;
		[Static]
		[Export("checkNotNegative:name:")]
		void CheckNotNegative(NSDecimalNumber value, string name);
	}

	// @interface Utils (UIImage)	
	[Category(AllowStaticMembers = true)]
	[BaseType(typeof(UIImage))]
    interface UIImageUtils
	{
		// +(UIImage * _Nullable)imageNamed:(NSString * _Nonnull)name ofType:(NSString * _Nonnull)type inBundle:(NSBundle * _Nullable)bundle;
		[Static]
		[Export("imageNamed:ofType:inBundle:")]
		[return: NullAllowed]
		UIImage ImageNamed(string name, string type, [NullAllowed] NSBundle bundle);
	}
}