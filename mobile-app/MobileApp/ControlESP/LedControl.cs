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

namespace ControlESP {
	public class LedControl {
		public int Id { get; set; }

		public bool Active { get; set; }

		public Color FirstColor { get; set; } = new Color();

		public Color SecondColor { get; set; } = new Color();
	}
}