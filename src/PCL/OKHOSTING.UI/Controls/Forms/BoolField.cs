﻿using System;

namespace OKHOSTING.UI.Controls.Forms
{
	/// <summary>
	/// A field for boolean values
	/// </summary>
	public class BoolField : FormField
	{
		public override object Value
		{
			get
			{
				if (Required)
				{
					return ((ICheckBox) ValueControl).Value;
				}
				else
				{
					string val = ((IListPicker) ValueControl).Value;

					if (string.IsNullOrWhiteSpace(val))
					{
						return null;
					}
					else
					{
						return bool.Parse(val);
					}
				}
			}
			set
			{
				if (value == null && !Required)
				{
					((IListPicker) ValueControl).Value = Resources.Strings.OKHOSTING_UI_Controls_Forms_EmptyValue;
				}
				else
				{
					((IListPicker) ValueControl).Value = value.ToString();
				}
			}
		}

		public override Type ValueType
		{
			get
			{
				return typeof(bool);
			}
		}

		/// <summary>
		/// Creates the controls for displaying the field
		/// </summary>
		protected override void CreateValueControl()
		{
			if (Required)
			{
				ValueControl = Platform.Current.Create<IListPicker>();
				((IListPicker) ValueControl).Items.Add(Resources.Strings.OKHOSTING_UI_Controls_Forms_EmptyValue);
				((IListPicker) ValueControl).Items.Add(Resources.Strings.OKHOSTING_UI_Controls_Forms_BoolField_True);
				((IListPicker) ValueControl).Items.Add(Resources.Strings.OKHOSTING_UI_Controls_Forms_BoolField_False);
			}
			else
			{
				ValueControl = Platform.Current.Create<ICheckBox>();
			}
		}
	}
}