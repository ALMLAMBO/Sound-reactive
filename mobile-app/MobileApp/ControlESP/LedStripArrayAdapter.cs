using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ControlESP {
	public class LedStripArrayAdapter : BaseAdapter<LedControl> {
		private List<LedControl> leds;
		private Context context;

		public LedStripArrayAdapter(
			Context context, List<LedControl> leds) {
			
			this.leds = leds;
			this.context = context;
		}

		public override LedControl this[int position] => leds[position];

		public override int Count => leds.Count;

		public override long GetItemId(int position) {
			return position;
		}

		public override View GetView(
			int position, View convertView, ViewGroup parent) {

			View row = convertView;
			if(row == null) {
				row = LayoutInflater.From(context)
					.Inflate(Resource.Layout.led_strip_row, null, false);
			}

			CheckBox activeCheckBox = row
				.FindViewById<CheckBox>(Resource.Id.active_checkbox);

			Button firstColorButton = row
				.FindViewById<Button>(Resource.Id.first_color_button);

			Button secondColorButton = row.
				FindViewById<Button>(Resource.Id.second_color_button);

			Button sendButton = row
				.FindViewById<Button>(Resource.Id.send_button);

			return row;
		}
	}
}