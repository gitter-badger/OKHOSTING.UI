﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using OKHOSTING.UI.Controls;
using OKHOSTING.UI.Controls.Layouts;

namespace OKHOSTING.UI.Net4.WPF.Controls.Layout
{
	public class Grid : System.Windows.Controls.Grid, IGrid
	{
		public Grid()
		{
		}

		int IGrid.ColumnCount
		{
			get
			{
				return base.ColumnDefinitions.Count;
			}
			set
			{
				//remove all controls from rows to be removed
				for (int i = 0; i < base.Children.Count; i++)
				{
					UIElement element = base.Children[i];

					if (Grid.GetColumn(element) > value)
					{
						base.Children.Remove(element);
					}
				}

				while (base.ColumnDefinitions.Count < value)
				{
					base.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition());
				}

				while (base.ColumnDefinitions.Count > value)
				{
					base.ColumnDefinitions.RemoveAt(base.ColumnDefinitions.Count - 1);
				}
			}
		}

		int IGrid.RowCount
		{
			get
			{
				return base.RowDefinitions.Count;
			}
			set
			{
				//remove all controls from rows to be removed
				for (int i = 0; i < base.Children.Count; i++)
				{
					UIElement element = base.Children[i];

					if (Grid.GetRow(element) > value)
					{
						base.Children.Remove(element);
					}
				}

				while (base.RowDefinitions.Count < value)
				{
					base.RowDefinitions.Add(new System.Windows.Controls.RowDefinition());
				}

				while (base.RowDefinitions.Count > value)
				{
					base.RowDefinitions.RemoveAt(base.RowDefinitions.Count - 1);
				}
			}
		}

		IControl IGrid.GetContent(int row, int column)
		{
			foreach (UIElement children in base.Children)
			{
				if (Grid.GetRow(children) == row && Grid.GetColumn(children) == column)
				{
					return (IControl) children;
				}
			}

			return null;
		}

		void IGrid.SetContent(int row, int column, IControl content)
		{
			var currentControl = ((IGrid)this).GetContent(row, column);

			if (currentControl != null)
			{
				base.Children.Remove((System.Windows.UIElement) currentControl);
			}

			Grid.SetRow((System.Windows.UIElement) content, row);
			Grid.SetColumn((System.Windows.UIElement) content, column);

			base.Children.Add((System.Windows.UIElement) content);
		}

		protected override Size ArrangeOverride(Size arrangeSize)
		{
			//apply paddings here? http://stackoverflow.com/questions/1319974/wpf-grid-with-column-row-margin-padding
			return base.ArrangeOverride(arrangeSize);
		}

		void IDisposable.Dispose()
		{
		}

		void IGrid.SetColumnSpan(int columnSpan, IControl content)
		{
			SetColumnSpan((UIElement) content, columnSpan);
		}

		int IGrid.GetColumnSpan(IControl content)
		{
			return GetColumnSpan((UIElement) content);
		}

		void IGrid.SetRowSpan(int rowSpan, IControl content)
		{
			SetRowSpan((UIElement) content, rowSpan);
		}

		int IGrid.GetRowSpan(IControl content)
		{
			return GetRowSpan((UIElement) content);
		}

		void IGrid.SetWidth(int column, double width)
		{
			base.ColumnDefinitions[column].Width = new GridLength(width, GridUnitType.Pixel);
		}

		double IGrid.GetWidth(int column)
		{
			return base.ColumnDefinitions[column].Width.Value;
		}

		void IGrid.SetHeight(int row, double height)
		{
			base.RowDefinitions[row].Height = new GridLength(height, GridUnitType.Pixel);
		}

		double IGrid.GetHeight(int row)
		{
			return base.RowDefinitions[row].Height.Value;
		}

		#region IControl

		bool IControl.Visible
		{
			get
			{
				return base.Visibility == System.Windows.Visibility.Visible;
			}
			set
			{
				if (value)
				{
					base.Visibility = System.Windows.Visibility.Visible;
				}
				else
				{
					base.Visibility = System.Windows.Visibility.Hidden;
				}
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
				return Platform.Current.Parse(base.Margin);
			}
			set
			{
				base.Margin = Platform.Current.Parse(value);
			}
		}

		Color IControl.BackgroundColor
		{
			get
			{
				return Platform.Current.Parse(((System.Windows.Media.SolidColorBrush)base.Background).Color);
			}
			set
			{
				base.Background = new System.Windows.Media.SolidColorBrush(Platform.Current.Parse(value));
			}
		}

		Color IControl.BorderColor
		{
			get; set;
		}

		Thickness IControl.BorderWidth
		{
			get; set;
		}

		HorizontalAlignment IControl.HorizontalAlignment
		{
			get
			{
				return Platform.Current.Parse(base.HorizontalAlignment);
			}
			set
			{
				base.HorizontalAlignment = Platform.Current.Parse(value);
			}
		}

		VerticalAlignment IControl.VerticalAlignment
		{
			get
			{
				return Platform.Current.Parse(base.VerticalAlignment);
			}
			set
			{
				base.VerticalAlignment = Platform.Current.Parse(value);
			}
		}

		Thickness IGrid.CellMargin
		{
			get; set;
		}

		Thickness IGrid.CellPadding
		{
			get; set;
		}

		#endregion
	}
}