using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;

namespace AffirmSDK
{
	[Native]
	public enum AffirmBrandType : ulong
	{
		Unknown,
		Visa,
		Amex,
		Mastercard,
		Discover,
		Jcb,
		DinersClub,
		UnionPay
	}

	[Native]
	public enum AffirmHTTPMethod : ulong
	{
		Get,
		Post
	}

	[Native]
	public enum AffirmEnvironment : long
	{
		Production,
		Sandbox
	}

	[Native]
	public enum AffirmLocale : long
	{
		Us,
		Ca
	}

	[Native]
	public enum AffirmPageType : long
	{
		None,
		Banner,
		Cart,
		Category,
		Homepage,
		Landing,
		Payment,
		Product,
		Search
	}

	[Native]
	public enum AffirmLogoType : long
	{
		Name,
		Text,
		Symbol,
		SymbolHollow
	}

	[Native]
	public enum AffirmColorType : long
	{
		Default,
		Blue,
		Black,
		White,
		BlueBlack
	}
}