using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Bisutti.Brigada.Forms
{
	public interface IFormsBase<T>
	{
		bool IsValid();
		List<T> Collection { get; set; }
		T Element { get; set; }
		void ClearForm();
		void LoadData();
		void RefreshBindings();
	}
}
