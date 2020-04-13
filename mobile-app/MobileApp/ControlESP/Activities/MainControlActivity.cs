using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
	[Activity(Label = "MainControlActivity", Theme = "@style/AppTheme")]
	public class MainControlActivity : AppCompatActivity {
		protected override void OnCreate(Bundle savedInstanceState) {
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.main_control_activity);

			Button button = FindViewById<Button>(
				Resource.Id.led_on_button);

			button.Click += SendLedOn_Click;
		}

		private void SendLedOn_Click(object sender, EventArgs e) {
			try {
				new Android.App.AlertDialog.Builder(this)
					.SetMessage("Sended command")
					.Show();

				return;
			} 
			catch (Exception) {

				throw;
			}
		}
	}
}