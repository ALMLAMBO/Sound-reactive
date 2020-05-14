using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Yuku.AmbilWarna;

namespace ControlESP {
	public class ColorPickerListener : 
		AmbilWarnaDialog.IOnAmbilWarnaListener {

		private Color c;
		private Context context;

		public ColorPickerListener(Context context) {
			this.context = context;
		}

		public void OnCancel(AmbilWarnaDialog dialog) {
			
		}

		public void OnOk(AmbilWarnaDialog dialog, int color) {
			c = new Color(color);
		}

		public Color GetColor() {
			return c;
		}
	}
}