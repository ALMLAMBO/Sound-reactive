using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace ControlESP.Activities {
	[Activity(Label = "Main Control", Theme = "@style/AppTheme")]
	public class MainControlActivity : AppCompatActivity {
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.main_control_activity);

			Button button = FindViewById<Button>(
				Resource.Id.send_command_button);

			button.Click += SendLedOn_Click;
			ListView availableLedStripsListView =
				FindViewById<ListView>(Resource.Id.available_led_strips);

			LedStripArrayAdapter adapter = 
				new LedStripArrayAdapter(this, new List<LedControl>());
		}

		private void SendLedOn_Click(object sender, EventArgs e) {
			
		}
	}
}