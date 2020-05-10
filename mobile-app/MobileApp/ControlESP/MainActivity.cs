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
using System.Collections.Generic;
using Android.Support.V4.App;
using Android;
using Android.Support.V4.Content;
using Android.Views;

namespace ControlESP {
	[Activity(Label = "Home", Theme = "@style/AppTheme", MainLauncher = true)]
	public class MainActivity : AppCompatActivity {
		private WifiManager wifiManager;
		private WifiReceiver wifiReceiver;
		private const int MY_PERMISSIONS_ACCESS_COARSE_LOCATION = 1;

		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

			Button connectButton = FindViewById<Button>
				(Resource.Id.connect_button);

			wifiManager = GetSystemService(WifiService)
				.JavaCast<WifiManager>();

			if(!wifiManager.IsWifiEnabled) {
				wifiManager.SetWifiEnabled(true);
			}

			connectButton.Click += delegate {
				ConnectToESP();
			};
		}

		private void ConnectToESP() {
			if (ContextCompat.CheckSelfPermission(
						this, Manifest.Permission.AccessCoarseLocation)

					!= Android.Content.PM.Permission.Granted) {

				ActivityCompat.RequestPermissions(
					this,
					new string[] { Manifest.Permission.AccessCoarseLocation },
					MY_PERMISSIONS_ACCESS_COARSE_LOCATION);
			}
			else {
				wifiReceiver = new WifiReceiver(wifiManager);
				IntentFilter filter = new IntentFilter();
				filter.AddAction(WifiManager.ScanResultsAvailableAction);
				RegisterReceiver(wifiReceiver, filter);

				GetWifi();

				//UnregisterReceiver(wifiReceiver);
			}
		}

		private void GetWifi() {
			if(Build.VERSION.SdkInt >= BuildVersionCodes.M) {
				if(ContextCompat.CheckSelfPermission(this,
					Manifest.Permission.AccessCoarseLocation)
					!= Android.Content.PM.Permission.Granted) {

					ActivityCompat.RequestPermissions(this,
						new string[] {
							Manifest.Permission.AccessCoarseLocation
						},
						MY_PERMISSIONS_ACCESS_COARSE_LOCATION);
				}
				else {
					wifiManager.StartScan();
				}
			}
			else {
				wifiManager.StartScan();
			}
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}