﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKHOSTING.UI.Controls;

namespace OKHOSTING.UI.UWP.Controls
{
	public class PasswordTextBox : Windows.UI.Xaml.Controls.StackPanel, IPasswordTextBox
	{
		public readonly Windows.UI.Xaml.Controls.PasswordBox NativeTextBox;

		public PasswordTextBox()
		{
			NativeTextBox = new Windows.UI.Xaml.Controls.PasswordBox();
			base.Children.Add(NativeTextBox);
		}

		public string Text
		{
			get
			{
				return NativeTextBox.Password;
			}
			set
			{
				NativeTextBox.Password = value;
			}
		}


		void IDisposable.Dispose()
		{
		}

		#region IControl

		bool IControl.Visible
		{
			get
			{
				return base.Visibility == Windows.UI.Xaml.Visibility.Visible;
			}
			set
			{
				if (value)
				{
					base.Visibility = Windows.UI.Xaml.Visibility.Visible;
				}
				else
				{
					base.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
				}
			}
		}

		bool IControl.Enabled
		{
			get
			{
				return true;
			}
			set
			{
				//do nothing
			}
		}

		double? IControl.Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				if (value.HasValue)
				{
					base.Width = value.Value;
				}
			}
		}

		double? IControl.Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				if (value.HasValue)
				{
					base.Height = value.Value;
				}
			}
		}

		Thickness IControl.Margin
		{
			get
			{
				return App.Current.Parse(base.Margin);
			}
			set
			{
				base.Margin = App.Current.Parse(value);
			}
		}

		Color IControl.BackgroundColor
		{
			get
			{
				return App.Current.Parse(((Windows.UI.Xaml.Media.SolidColorBrush)base.Background).Color);
			}
			set
			{
				base.Background = new Windows.UI.Xaml.Media.SolidColorBrush(App.Current.Parse(value));
			}
		}

		Color IControl.BorderColor
		{
			get
			{
				return App.Current.Parse(((Windows.UI.Xaml.Media.SolidColorBrush)base.BorderBrush).Color);
			}
			set
			{
				base.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(App.Current.Parse(value));
			}
		}

		Thickness IControl.BorderWidth
		{
			get
			{
				return App.Current.Parse(base.BorderThickness);
			}
			set
			{
				base.BorderThickness = App.Current.Parse(value);
			}
		}

		HorizontalAlignment IControl.HorizontalAlignment
		{
			get
			{
				return App.Current.Parse(base.HorizontalAlignment);
			}
			set
			{
				base.HorizontalAlignment = App.Current.Parse(value);
			}
		}

		VerticalAlignment IControl.VerticalAlignment
		{
			get
			{
				return App.Current.Parse(base.VerticalAlignment);
			}
			set
			{
				base.VerticalAlignment = App.Current.Parse(value);
			}
		}

		#endregion
	}
}