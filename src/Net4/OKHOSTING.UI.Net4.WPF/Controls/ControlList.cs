﻿using OKHOSTING.UI.Controls;
using System;
using System.Collections.Generic;
using Native = global::System.Windows.Controls;
using System.Collections;

namespace OKHOSTING.UI.Net4.WPF.Controls
{
	public class ControlList : IList<IControl>
	{
		public ControlList(Native.UIElementCollection innerList)
		{
			if (innerList == null)
			{
				throw new ArgumentNullException("innerList");
			}

			InnerList = innerList;
		}

		protected readonly Native.UIElementCollection InnerList;

		public IControl this[int index]
		{
			get
			{
				return (IControl) InnerList[index];
			}
			set
			{
				InnerList[index] = (Native.Control) value;
			}
		}

		public int Count
		{
			get
			{
				return InnerList.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public void Add(IControl item)
		{
			InnerList.Add((Native.Control) item);
		}

		public void Clear()
		{
			InnerList.Clear();
		}

		public bool Contains(IControl item)
		{
			return InnerList.Contains((Native.Control) item);
		}

		public void CopyTo(IControl[] array, int arrayIndex)
		{
			InnerList.CopyTo((Native.Control[]) array, arrayIndex);
		}

		public IEnumerator<IControl> GetEnumerator()
		{
			foreach (Native.Control view in InnerList)
			{
				yield return (IControl) view;
			}
		}

		public int IndexOf(IControl item)
		{
			return InnerList.IndexOf((Native.Control) item);
		}

		public void Insert(int index, IControl item)
		{
			InnerList.Insert(index, (Native.Control) item);
		}

		public bool Remove(IControl item)
		{
			InnerList.Remove((Native.Control) item);

			return true;
		}

		public void RemoveAt(int index)
		{
			InnerList.RemoveAt(index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}