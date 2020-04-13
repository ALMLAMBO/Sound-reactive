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
using Android.Net.Wifi;
using System.Linq;

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
			const string ssid = "\"sound_reactive\"";
			const string password = "\"password\"";

			WifiConfiguration config = new WifiConfiguration() {
				Ssid = ssid,
				PreSharedKey = password
			};

			WifiManager manager = GetSystemService(WifiService)
				.JavaCast<WifiManager>();

			int addNetwork = manager.AddNetwork(config);

			System.Diagnostics.Debug.WriteLine($"addNetwork: {addNetwork}");

			WifiConfiguration network = manager
				.ConfiguredNetworks
				.FirstOrDefault(x => x.Ssid == ssid);

			if (network == null) {
				System.Diagnostics.Debug.WriteLine("Didn't find the network, not connecting.");
				return;
			}

			await Task.Run(() => {
				manager.Disconnect();
			});

			bool enableNetwork = manager
				.EnableNetwork(network.NetworkId, true);

			System.Diagnostics.Debug.WriteLine("enableNetwork = " + enableNetwork);

			if(manager.Reconnect()) {
				var builder = new Android.App.AlertDialog.Builder(this);

				builder.SetMessage("Connected");
				Android.App.AlertDialog alert = builder.Create();
				alert.Show();
			}
			else {
				var builder = new Android.App.AlertDialog.Builder(this);

				builder.SetMessage("Nope");
				Android.App.AlertDialog alert = builder.Create();
				alert.Show();
			}

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