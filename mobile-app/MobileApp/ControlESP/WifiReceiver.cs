using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ControlESP.Activities;

namespace ControlESP {
	public class WifiReceiver : BroadcastReceiver {
		WifiManager wifiManager;

		public WifiReceiver(WifiManager wifiManager) {
			this.wifiManager = wifiManager;
		}

		public override void OnReceive(Context context, Intent intent) {
			string action = intent.Action;
			const string ssid = "\"sound_reactive\"";
			const string password = "\"password\"";

			if (WifiManager.ScanResultsAvailableAction.Equals(action)) {
				List<ScanResult> wifiList = wifiManager
					.ScanResults.ToList();

				WifiConfiguration network = wifiList
					.Where(x => x.Ssid.Equals("sound_reactive"))
					.Select(x => new WifiConfiguration() { Ssid = "sound_reactive" })
					.FirstOrDefault();

				if(network != null) {
					network.PreSharedKey = password;
					network.Ssid = ssid;
					wifiList.ForEach(x => {
						if(x.Ssid != "sound_reactive") {
							WifiConfiguration wifi =
								new WifiConfiguration() {
									Ssid = x.Ssid
								};

							wifiManager.DisableNetwork(wifi.NetworkId);
						}
					});

					wifiManager.EnableNetwork(network.NetworkId, true);

					if(wifiManager.Reconnect()) {
						Intent redirectToMainControl =
							new Intent(context, typeof(MainControlActivity));

						context.StartActivity(redirectToMainControl);
					}
				} 
				else {
					AlertDialog.Builder builder =
						new AlertDialog.Builder(context);

					builder.SetMessage("Not found");
					AlertDialog alert = builder.Create();
					alert.Show();
				}
			}

			context.UnregisterReceiver(this);
		}
	}
}