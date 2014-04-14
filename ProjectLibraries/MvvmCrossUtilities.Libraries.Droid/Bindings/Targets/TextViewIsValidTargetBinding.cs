using System;
using Android.Widget;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Droid.Target;

namespace MvvmCrossUtilities.Libraries.Droid.Bindings.Targets
{
    public class TextViewIsValidTargetBinding : MvxAndroidTargetBinding
    {
        protected TextView TextView
        {
            get { return (TextView)Target; }
        }

        public TextViewIsValidTargetBinding(TextView textView)
            : base(textView)
        {
        }

        public override MvxBindingMode DefaultMode
        {
            get { return MvxBindingMode.OneWay; }
        }

        public override Type TargetType
        {
            get { return typeof(TextView); }
        }

        protected override void SetValueImpl(object target, object value)
        {
            var boolValue = value as bool?;
            if (boolValue == null)
            {
                MvxBindingTrace.Trace(MvxTraceLevel.Warning, "Null value passed to TextView.IsValid binding");
                return;
            }

            ((TextView)target).Error = boolValue.Value ? null : string.Empty;
        }
    }
}