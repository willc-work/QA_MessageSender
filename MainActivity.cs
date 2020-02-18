using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using AlertDialog = Android.App.AlertDialog;
using Android.Content;

namespace MessageSender
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button sendEmail = FindViewById<Button>(Resource.Id.sendButton);
            Button sendSMS = FindViewById<Button>(Resource.Id.sendSMSButton);
            Button displayMessage = FindViewById<Button>(Resource.Id.displayMessageButton);

            EditText recipient = FindViewById<EditText>(Resource.Id.email);
            EditText subject = FindViewById<EditText>(Resource.Id.subject);
            EditText message = FindViewById<EditText>(Resource.Id.message);

            sendEmail.Click += delegate
            {
                if (recipient.Text.Trim().Length == 0 || subject.Text.Trim().Length == 0 || message.Text.Trim().Length == 0)
                {
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Error");
                    dialog.SetMessage("You must supply a recipient, subject and a message");
                    dialog.SetNeutralButton("OK", (s, a) => { });
                    dialog.Show();
                }
                else
                {
                    var email = new Intent(Intent.ActionSend);
                    email.PutExtra(Intent.ExtraEmail, new string[] { recipient.Text });
                    email.PutExtra(Intent.ExtraSubject, subject.Text);
                    email.PutExtra(Intent.ExtraText, message.Text);
                    email.SetType("message/rfc822");

                    StartActivity(email);
                };
            };

            sendSMS.Click += (sender, args) =>
            {
                string url = string.Format("smsto:{0}", recipient.Text);
                var uri = Android.Net.Uri.Parse(url);
                var intent = new Intent(Intent.ActionSendto, uri);
                intent.PutExtra("sms_body", message.Text);
                StartActivity(intent);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}