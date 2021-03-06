﻿using System.Linq;
using System.Collections.Generic;
using OKHOSTING.UI.Controls;
using OKHOSTING.UI.Controls.Layouts;
using System;

namespace OKHOSTING.UI.Net4.WebForms.Controls.Layout
{
	public class Grid : System.Web.UI.WebControls.Table, IGrid
	{
		protected int _ColumnCount = 0;

		#region IControl

		string IControl.Name
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		Color IControl.BackgroundColor
		{
			get
			{
				return Platform.Current.Parse(base.BackColor);
			}
			set
			{
				base.BackColor = Platform.Current.Parse(value);
			}
		}

		Color IControl.BorderColor
		{
			get
			{
				return Platform.Current.Parse(base.BorderColor);
			}
			set
			{
				base.BorderColor = Platform.Current.Parse(value);
			}
		}

		double? IControl.Width
		{
			get
			{
				if (base.Width.IsEmpty)
				{
					return null;
				}

				return base.Width.Value;
			}
			set
			{
				if (value.HasValue)
				{
					base.Width = new System.Web.UI.WebControls.Unit(value.Value, System.Web.UI.WebControls.UnitType.Pixel);
				}
				else
				{
					base.Width = new System.Web.UI.WebControls.Unit();
				}
			}
		}

		double? IControl.Height
		{
			get
			{
				if (base.Height.IsEmpty)
				{
					return null;
				}

				return base.Height.Value;
			}
			set
			{
				if (value.HasValue)
				{
					base.Height = new System.Web.UI.WebControls.Unit(value.Value, System.Web.UI.WebControls.UnitType.Pixel);
				}
				else
				{
					base.Height = new System.Web.UI.WebControls.Unit();
				}
			}
		}

		Thickness IControl.Margin
		{
			get
			{
				double left, top, right, bottom;
				Thickness thickness = new Thickness();

				if (double.TryParse(base.Style["margin-left"], out left)) thickness.Left = left;
				if (double.TryParse(base.Style["margin-top"], out top)) thickness.Top = top;
				if (double.TryParse(base.Style["margin-right"], out right)) thickness.Right = right;
				if (double.TryParse(base.Style["margin-bottom"], out bottom)) thickness.Bottom = bottom;

				return new Thickness(left, top, right, bottom);
			}
			set
			{
				if (value.Left.HasValue) base.Style["margin-left"] = string.Format("{0}px", value.Left);
				if (value.Top.HasValue) base.Style["margin-top"] = string.Format("{0}px", value.Top);
				if (value.Right.HasValue) base.Style["margin-right"] = string.Format("{0}px", value.Right);
				if (value.Bottom.HasValue) base.Style["margin-bottom"] = string.Format("{0}px", value.Bottom);
			}
		}

		Thickness IControl.BorderWidth
		{
			get
			{
				double left, top, right, bottom;
				Thickness thickness = new Thickness();

				if (double.TryParse(base.Style["border-left-width"], out left)) thickness.Left = left;
				if (double.TryParse(base.Style["border-top-width"], out top)) thickness.Top = top;
				if (double.TryParse(base.Style["border-right-width"], out right)) thickness.Right = right;
				if (double.TryParse(base.Style["border-bottom-width"], out bottom)) thickness.Bottom = bottom;

				return new Thickness(left, top, right, bottom);
			}
			set
			{
				if (value.Left.HasValue) base.Style["border-left-width"] = string.Format("{0}px", value.Left);
				if (value.Top.HasValue) base.Style["border-top-width"] = string.Format("{0}px", value.Top);
				if (value.Right.HasValue) base.Style["border-right-width"] = string.Format("{0}px", value.Right);
				if (value.Bottom.HasValue) base.Style["border-bottom-width"] = string.Format("{0}px", value.Bottom);
			}
		}

		HorizontalAlignment IControl.HorizontalAlignment
		{
			get
			{
				string cssClass = base.CssClass.Split().Where(c => c.StartsWith("horizontal-alignment")).SingleOrDefault();

				if (string.IsNullOrWhiteSpace(cssClass))
				{
					return HorizontalAlignment.Left;
				}

				if (cssClass.EndsWith("left"))
				{
					return HorizontalAlignment.Left;
				}
				else if (cssClass.EndsWith("right"))
				{
					return HorizontalAlignment.Right;
				}
				else if (cssClass.EndsWith("center"))
				{
					return HorizontalAlignment.Center;
				}
				else if (cssClass.EndsWith("fill"))
				{
					return HorizontalAlignment.Fill;
				}
				else
				{
					return HorizontalAlignment.Left;
				}
			}
			set
			{
				Platform.Current.RemoveCssClassesStartingWith(this, "horizontal-alignment");
				Platform.Current.AddCssClass(this, "horizontal-alignment-" + value.ToString().ToLower());
			}
		}

		VerticalAlignment IControl.VerticalAlignment
		{
			get
			{
				string cssClass = base.CssClass.Split().Where(c => c.StartsWith("vertical-alignment")).SingleOrDefault();

				if (string.IsNullOrWhiteSpace(cssClass))
				{
					return VerticalAlignment.Top;
				}

				if (cssClass.EndsWith("top"))
				{
					return VerticalAlignment.Top;
				}
				else if (cssClass.EndsWith("bottom"))
				{
					return VerticalAlignment.Bottom;
				}
				else if (cssClass.EndsWith("center"))
				{
					return VerticalAlignment.Center;
				}
				else if (cssClass.EndsWith("fill"))
				{
					return VerticalAlignment.Fill;
				}
				else
				{
					return VerticalAlignment.Top;
				}
			}
			set
			{
				Platform.Current.RemoveCssClassesStartingWith(this, "vertical-alignment");
				Platform.Current.AddCssClass(this, "vertical-alignment-" + value.ToString().ToLower());
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

		int IGrid.ColumnCount
		{
			get
			{
				return _ColumnCount;
			}
			set
			{
				_ColumnCount = value;

				foreach (System.Web.UI.WebControls.TableRow row in Rows)
				{
					while (row.Cells.Count < value)
					{
						row.Cells.Add(new System.Web.UI.WebControls.TableCell());
					}

					while (row.Cells.Count > value)
					{
						row.Cells.RemoveAt(row.Cells.Count - 1);
					}
				}
			}
		}

		int IGrid.RowCount
		{
			get
			{
				return Rows.Count;
			}
			set
			{
				while (Rows.Count < value)
				{
					Rows.Add(new System.Web.UI.WebControls.TableRow());
				}

				while (Rows.Count > value)
				{
					Rows.RemoveAt(Rows.Count - 1);
				}

				//re-set columns
				((IGrid) this).ColumnCount = _ColumnCount;
			}
		}

		Thickness IGrid.CellMargin
		{
			get
			{
				return new Thickness(base.CellSpacing);
			}
			set
			{
				base.CellSpacing = (int) value.Bottom;
			}
		}

		Thickness IGrid.CellPadding
		{
			get
			{
				return new Thickness(base.CellSpacing);
			}
			set
			{
				base.CellSpacing = (int) value.Bottom;
			}
		}

		IControl IGrid.GetContent(int row, int column)
		{
			if (Rows[row].Cells[column].Controls.Count == 0)
			{
				return null;
			}

			return (IControl)Rows[row].Cells[column].Controls[0];
		}

		void IGrid.SetContent(int row, int column, IControl content)
		{
			if (row > Rows.Count)
			{
				throw new ArgumentOutOfRangeException(nameof(row));
			}

			if (column > _ColumnCount)
			{
				throw new ArgumentOutOfRangeException(nameof(column));
			}

			while (Rows[row].Cells.Count < _ColumnCount)
			{
				Rows[row].Cells.Add(new System.Web.UI.WebControls.TableCell());
			}

			Rows[row].Cells[column].Controls.Clear();
			Rows[row].Cells[column].Controls.Add((System.Web.UI.Control)content);
		}

		public List<IControl> GetAllControls()
		{
			return IGridExtensions.GetAllControlls(this).ToList();
		}

		void IGrid.SetColumnSpan(int columnSpan, IControl content)
		{
			System.Web.UI.WebControls.TableCell cell = (System.Web.UI.WebControls.TableCell) ((System.Web.UI.WebControls.WebControl) content).Parent;
			cell.ColumnSpan = columnSpan;
		}

		int IGrid.GetColumnSpan(IControl content)
		{
			System.Web.UI.WebControls.TableCell cell = (System.Web.UI.WebControls.TableCell)((System.Web.UI.WebControls.WebControl)content).Parent;
			return cell.ColumnSpan;
		}

		void IGrid.SetRowSpan(int rowSpan, IControl content)
		{
			System.Web.UI.WebControls.TableCell cell = (System.Web.UI.WebControls.TableCell)((System.Web.UI.WebControls.WebControl)content).Parent;
			cell.RowSpan = rowSpan;
		}

		int IGrid.GetRowSpan(IControl content)
		{
			System.Web.UI.WebControls.TableCell cell = (System.Web.UI.WebControls.TableCell)((System.Web.UI.WebControls.WebControl)content).Parent;
			return cell.RowSpan;
		}

		void IGrid.SetWidth(int column, double width)
		{
			foreach (System.Web.UI.WebControls.TableRow row in base.Rows)
			{
				row.Cells[column].Width = new System.Web.UI.WebControls.Unit(width, System.Web.UI.WebControls.UnitType.Pixel);
			}
		}

		double IGrid.GetWidth(int column)
		{
			if (base.Rows.Count == 0)
			{
				return 0;
			}

			return base.Rows[0].Cells[column].Width.Value;
		}

		void IGrid.SetHeight(int row, double height)
		{
			base.Rows[row].Height = new System.Web.UI.WebControls.Unit(height, System.Web.UI.WebControls.UnitType.Pixel);
		}

		double IGrid.GetHeight(int row)
		{
			return base.Rows[row].Height.Value;
		}
	}
}