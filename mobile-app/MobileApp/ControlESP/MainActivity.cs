using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Util;
using Android.Content;
using ControlESP.Activities;

namespace ControlESP {
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
	public class MainActivity : AppCompatActivity {
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			Button connectButton = FindViewById<Button>
				(Resource.Id.connect_button);

			connectButton.Click += async delegate {
				await ConnectToESP();
			};
		}

		private async Task ConnectToESP() {
			await Task.Run(() => {
				Console.WriteLine("Clicked button!");
			});

			Intent mainPage = new Intent(this,
				typeof(MainControlActivity));

			StartActivity(mainPage);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}