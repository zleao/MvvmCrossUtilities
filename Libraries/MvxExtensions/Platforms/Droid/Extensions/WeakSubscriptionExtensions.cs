﻿using System;
using System.Reflection;
using Android.Text;
using Android.Widget;
using MvvmCross.WeakSubscription;

namespace MvxExtensions.Platforms.Droid.Extensions
{
    public static class WeakSubscriptionExtensions
    {
        public static EditTextTextChangedEventSubscription WeakSubscribeTextChanged(this EditText source, EventHandler<TextChangedEventArgs> eventHandler)
        {
            return new EditTextTextChangedEventSubscription(source, source.GetType().GetEvent("TextChanged"), eventHandler);
        }

        public class EditTextTextChangedEventSubscription : MvxWeakEventSubscription<EditText, TextChangedEventArgs>
        {
            public EditTextTextChangedEventSubscription(EditText source,
                                                        EventInfo eventInfo,
                                                        EventHandler<TextChangedEventArgs> eventHandler)
                : base(source, eventInfo, eventHandler)
            {
            }

            protected override Delegate CreateEventHandler()
            {
                return new EventHandler<TextChangedEventArgs>(OnSourceEvent);
            }
        }
    }
}
