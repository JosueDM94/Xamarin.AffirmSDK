using Android.Runtime;

namespace AffirmSDK.Widget
{
    partial class MoneyFormattedEditText
    {
        // Error CS0534: 'MoneyFormattedEditText' does not implement inherited abstract member 'FormattedEditText.RawTextParser.get'
        protected override AffirmSDK.ITextParser RawTextParser
        {
            get
            {
                try {
                    return global::Java.Lang.Object.GetObject<global::AffirmSDK.ITextParser>(JNIEnv.CallObjectMethod(Handle, JNIEnv.GetMethodID(JNIEnv.GetObjectClass(Handle), "getTextParser", "()Lcom/affirm/android/TextParser;")), JniHandleOwnership.TransferLocalRef);
                } finally {
                }
            }
        }
    }
}