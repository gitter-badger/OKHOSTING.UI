﻿using System;
using System.Linq;
using System.Collections.Generic;
using OKHOSTING.UI.Controls;

namespace OKHOSTING.UI.Xamarin.Forms.Controls
{
	public class ListPicker : global::Xamarin.Forms.Picker, IListPicker
	{
		public ListPicker()
		{
			base.SelectedIndexChanged += ListPicker_SelectedIndexChanged;
		}

		IList<string> IListPicker.Items
		{
			get
			{
				return base.Items;
			}
			set
			{
				base.Items.Clear();
				
				foreach (string item in value)
				{
					base.Items.Add(item);
				}
			}
		}

		void IDisposable.Dispose()
		{
		}

		#region IInputControl

		private void ListPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, ((IInputControl<string>) this).Value);
			}
		}

		public event EventHandler<string> ValueChanged;

		string IInputControl<string>.Value
		{
			get
			{
				return ((IListPicker)this).Items.ToArray()[base.SelectedIndex];
			}
			set
			{
				int index = ((IListPicker)this).Items.ToList().IndexOf(value);
				base.SelectedIndex = index;
			}
		}

		#endregion

		#region IControl

		string IControl.Name
		{
			get; set;
		}

		bool IControl.Visible
		{
			get
			{
				return base.IsVisible;
			}
			set
			{
				base.IsVisible = value;
			}
		}

		bool IControl.Enabled
		{
			get
			{
				return base.IsEnabled;
			}
			set
			{
				base.IsEnabled = value;
			}
		}

		double? IControl.Width
		{
			get
			{
				return base.WidthRequest;
			}
			set
			{
				if (value.HasValue)
				{
					base.WidthRequest = value.Value;
				}
			}
		}

		double? IControl.Height
		{
			get
			{
				return base.HeightRequest;
			}
			set
			{
				if (value.HasValue)
				{
					base.HeightRequest = value.Value;
				}
			}
		}

		Thickness IControl.Margin
		{
			get; set;
		}

		Color IControl.BackgroundColor
		{
			get
			{
				return Platform.Current.Parse(base.BackgroundColor);
			}
			set
			{
				base.BackgroundColor = Platform.Current.Parse(value);
			}
		}

		Color IControl.BorderColor
		{
			get;
			set;
		}

		Thickness IControl.BorderWidth
		{
			get;
			set;
		}

		HorizontalAlignment IControl.HorizontalAlignment
		{
			get
			{
				return Platform.Current.Parse(base.HorizontalOptions.Alignment);
			}
			set
			{
				base.HorizontalOptions = new global::Xamarin.Forms.LayoutOptions(Platform.Current.Parse(value), false);
			}
		}

		VerticalAlignment IControl.VerticalAlignment
		{
			get
			{
				return Platform.Current.ParseVerticalAlignment(base.VerticalOptions.Alignment);
			}
			set
			{
				base.VerticalOptions = new global::Xamarin.Forms.LayoutOptions(Platform.Current.Parse(value), false);
			}
		}

		/// <summary>
		/// Gets or sets an arbitrary object value that can be used to store custom information about this element. 
		/// </summary>
		/// <remarks>
		/// Returns the intended value. This property has no default value.
		/// </remmarks>
		object IControl.Tag
		{
			get; set;
		}

		#endregion

		#region ITextControl

		string ITextControl.FontFamily
		{
			get;
			set;
		}

		Color ITextControl.FontColor
		{
			get;
			set;
		}

		bool ITextControl.Bold
		{
			get;
			set;
		}

		bool ITextControl.Italic
		{
			get;
			set;
		}

		bool ITextControl.Underline
		{
			get;
			set;
		}

		HorizontalAlignment ITextControl.TextHorizontalAlignment
		{
			get;
			set;
		}

		VerticalAlignment ITextControl.TextVerticalAlignment
		{
			get;
			set;
		}

		Thickness ITextControl.TextPadding
		{
			get;
			set;
		}

		double ITextControl.FontSize
		{
			get;
			set;
		}


		#endregion
	}
}