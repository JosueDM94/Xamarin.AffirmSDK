using System;
using Android.App;
using Android.Runtime;

namespace AffirmSDK.Sample.Android
{
	[Application(Label = "@string/app_name")]
	public class MainApplication : Application
	{
		#region Lifecycle Methods
		protected MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();

			Affirm.Initialize(new Affirm.Configuration.Builder("Y8CQXFF044903JC0") // In Canadian, should use Canada public API key
				.SetEnvironment(Affirm.Environment.Sandbox)
				.SetMerchantName(null)
				.SetLogLevel(Affirm.LogLevelDebug)
				.SetLocation(Affirm.Location.Us)  // "CA" for Canadian, "US" for American (If not set, default will use US)
				.Build());
		}
		#endregion
	}
}
