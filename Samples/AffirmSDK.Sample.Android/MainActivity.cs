using System.Collections.Generic;
using AffirmSdk;
using AffirmSdk.Exception;
using AffirmSdk.Model;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Widget;
using AndroidX.AppCompat.App;
using Java.Lang;
using Java.Math;

namespace AffirmSDK.Sample.Android
{
	[Activity(Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity, Affirm.ICheckoutCallbacks, Affirm.IVcnCheckoutCallbacks, Affirm.IPrequalCallbacks, IPromotionCallback
	{
		private readonly static BigDecimal Price = BigDecimal.ValueOf(1100.0);

		public static string TAG = "MainActivity";

		private IAffirmRequest promoRequest;

		private TextView promotionTextView;
		private Affirm.PromoRequestData requestData;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);

			FindViewById<TextView>(Resource.Id.price).Text = $"${Price.ToPlainString()}";
			FindViewById<Button>(Resource.Id.checkout).Click += delegate { Affirm.StartCheckout(this, CheckoutModel(), false); };
			FindViewById<Button>(Resource.Id.vcnCheckout).Click += delegate { Affirm.StartCheckout(this, CheckoutModel(), true); };
			FindViewById<Button>(Resource.Id.siteModalButton).Click += delegate { Affirm.ShowSiteModal(this, "5LNMQ33SEUYHLNUC"); };
			FindViewById<Button>(Resource.Id.productModalButton).Click += delegate { Affirm.ShowProductModal(this, Price, "0Q97G0Z4Y4TLGHGB"); };
			FindViewById<Button>(Resource.Id.trackOrderConfirmed).Click += delegate {
				Toast.MakeText(this, "Track successfully", ToastLength.Short).Show();
				Affirm.TrackOrderConfirmed(this, TrackModel());
			};

			FindViewById<Button>(Resource.Id.clearCookies).Click += delegate {
				CookiesUtil.ClearCookies(this);
			};

			var promo = FindViewById<AffirmPromotionButton>(Resource.Id.promo);
			Affirm.ConfigureWithAmount(promo, null, PromoPageType.Product, Price, true);

			// Fetch promotion, then use your own TextView to display
			requestData = new Affirm.PromoRequestData.Builder(Price, true)
					.SetPageType(null)
					.Build();

			promotionTextView = FindViewById<TextView>(Resource.Id.promotionTextView);
			promoRequest = Affirm.FetchPromotion(requestData, promotionTextView.TextSize, this, this);
		}

		private AffirmTrack TrackModel()
		{
			var affirmTrackOrder = AffirmTrackOrder.InvokeBuilder()
				.SetStoreName("Affirm Store")
				.SetCoupon("SUMMER2018")
				.SetCurrency(Currency.Usd)  // "CAD" for canadian, "USD" for American
				.SetDiscount(Integer.ValueOf(0))
				.SetPaymentMethod("Visa")
				.SetRevenue(Integer.ValueOf(2920))
				.SetShipping(Integer.ValueOf(534))
				.SetShippingMethod("Fedex")
				.SetTax(Integer.ValueOf(285))
				.SetOrderId("T12345")
				.SetTotal(Integer.ValueOf(3739))
				.Build();


			var affirmTrackProduct = AffirmTrackProduct.InvokeBuilder()
					.SetBrand("Affirm")
					.SetCategory("Apparel")
					.SetCoupon("SUMMER2018")
					.SetName("Affirm T-Shirt")
					.SetPrice(Integer.ValueOf(730))
					.SetProductId("SKU-1234")
					.SetQuantity(Integer.ValueOf(1))
					.SetVariant("Black")
					.Build();


			var affirmTrackProducts = new List<AffirmTrackProduct>();

			affirmTrackProducts.Add(affirmTrackProduct);


			return AffirmTrack.InvokeBuilder()
					.SetAffirmTrackOrder(affirmTrackOrder)
					.SetAffirmTrackProducts(affirmTrackProducts)
					.Build();
		}

		private Checkout CheckoutModel()
		{
			Item item = Item.InvokeBuilder()
				.SetDisplayName("Great Deal Wheel")
				.SetImageUrl("http://www.m2motorsportinc.com/media/catalog/product/cache/1/thumbnail" +
					"/9df78eab33525d08d6e5fb8d27136e95/v/e/velocity-vw125-wheels-rims.jpg")
				.SetQty(Integer.ValueOf(1))
				.SetSku("wheel")
				.SetUnitPrice(BigDecimal.ValueOf(1000.0))
				.SetUrl("http://merchant.com/great_deal_wheel")
				.Build();

			var items = new Dictionary<string, Item>();
			items["wheel"] = item;

			var name = Name.InvokeBuilder().SetFull("John Smith").Build();

			//  In canadian, use CAAddress
			//        val address = CAAddress.builder()
			//                .setStreet1("123 Alder Creek Dr.")
			//                .setStreet2("Floor 7")
			//                .setCity("Toronto")
			//                .setRegion1Code("ON")
			//                .setPostalCode("M4B 1B3")
			//                .setCountryCode("CA")
			//                .build()

			//  In US, use Address
			var address = Address.InvokeBuilder()
				.SetCity("San Francisco")
				.SetCountry("USA")
				.SetLine1("333 Kansas st")
				.SetState("CA")
				.SetZipcode("94107")
				.Build();


			var shipping = Shipping.InvokeBuilder().SetAddress(address).SetName(name).Build();

			var billing = Billing.InvokeBuilder().SetAddress(address).SetName(name).Build();

			// More details on https://docs.affirm.com/affirm-developers/reference/the-metadata-object
			var metadata = new Dictionary<string, string>() {
				{ "webhook_session_id", "ABC123" },
				{ "shipping_type", "UPS Ground" },
				{ "entity_name", "internal-sub_brand-name" }
			};

			return Checkout.InvokeBuilder()
					.SetItems(items)
					.SetBilling(billing)
					.SetShipping(shipping)
					.SetShippingAmount(BigDecimal.ValueOf(0.0))
					.SetTaxAmount(BigDecimal.ValueOf(100.0))
					.SetTotal(Price)
					.SetCurrency(Currency.Usd) // For Canadian, you must set "CAD"; For American, this is optional, you can set "USD" or not set.
					.SetMetadata(metadata)
					.Build();
		}

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

		protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
		{
			if (Affirm.HandleCheckoutData(this, requestCode, (int)resultCode, data)) {
				return;
			}

			if (Affirm.HandleVcnCheckoutData(this, requestCode, (int)resultCode, data)) {
				return;
	
			}

			if (Affirm.HandlePrequalData(this, requestCode, (int)resultCode, data)) {
				return;
	
			}

			base.OnActivityResult(requestCode, resultCode, data);
		}

		public void OnAffirmCheckoutError(string message)
		{
			Toast.MakeText(this, $"Checkout Error: {message}", ToastLength.Long).Show();
		}

		public void OnAffirmCheckoutCancelled()
		{
			Toast.MakeText(this, "Checkout Cancelled", ToastLength.Long).Show();
		}

		public void OnAffirmCheckoutSuccess(string token)
		{
			Toast.MakeText(this, $"Checkout token: {token}", ToastLength.Long).Show();
		}

		public void OnAffirmVcnCheckoutError(string message)
		{
			Toast.MakeText(this, $"Vcn Checkout Error: {message}", ToastLength.Long).Show();
		}

		public void OnAffirmVcnCheckoutCancelled()
		{
			Toast.MakeText(this, "Vcn Checkout Cancelled", ToastLength.Long).Show();
		}

		public void OnAffirmVcnCheckoutCancelledReason(VcnReason vcnReason)
		{
			Toast.MakeText(this, $"Vcn Checkout Cancelled: {vcnReason}", ToastLength.Long).Show();
		}	

		public void OnAffirmVcnCheckoutSuccess(CardDetails cardDetails)
		{
			Toast.MakeText(this, $"Vcn Checkout Card: {cardDetails}", ToastLength.Long).Show();
		}

		public void OnAffirmPrequalError(string message)
		{
			Toast.MakeText(this, $"Prequal Error: {message}", ToastLength.Long).Show();
		}

		public void OnSuccess(SpannableString spannableString, bool showPrequal)
		{
			promotionTextView.TextFormatted = spannableString;
			promotionTextView.Click += delegate { Affirm.OnPromotionClick(this, requestData, showPrequal); };
		}

		public void OnFailure(AffirmException exception)
		{
			Toast.MakeText(this, $"Failed to get promo message, reason: {exception.Message}", ToastLength.Short).Show();
		}
	}
}