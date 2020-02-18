using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MessageSender
{
    [Activity(Label = "MessageDisplayerActivity")]
    public class MessageDisplayerActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            TextView messageTextView = FindViewById<TextView>(Resource.Id.messageTextView);
            messageTextView.Text = Intent.GetStringExtra("message");
        }
    }
}