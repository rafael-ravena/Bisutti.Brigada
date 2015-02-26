using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Bisutti.Brigada.UserControls;
using System.Runtime.CompilerServices;
using System.Data;
using System.Windows.Media.Imaging;

namespace Bisutti.Brigada
{
	public static class SharedMethods
	{
		public static string StringfyTime(int hour, int minute)
		{
			return (hour.ToString().Length < 2 ? "0" + hour : hour.ToString()) + ":" + (minute.ToString().Length < 2 ? "0" + minute : minute.ToString()); ;
		}
		public static void PaintControl(Control control, ControlPaintType controlPaintType)
		{
			control.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + ((int)controlPaintType).ToString("X")));
		}
		public static void PaintControl(Control control)
		{
			PaintControl(control, ControlPaintType.Error);
		}
		public static void PaintControl(Panel panel, ControlPaintType controlPaintType)
		{
			panel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + ((int)controlPaintType).ToString("X")));
		}
		public static void PaintControl(Panel panel)
		{
			PaintControl(panel, ControlPaintType.Error);
		}
		public static void Finalize(this UserControl me)
		{
			me.Resources.Clear();
			me = null;
			GC.AddMemoryPressure((long)1024E+6);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}
		public static void Invalidate(this TextBox control, string message)
		{
			PaintControl(control);
			control.ToolTip = message;
		}
		public static void Invalidate(this TextBox control)
		{
			control.Invalidate("Este campo é obrigatório");
		}
		public static void Invalidate(this ComboBox control, string message)
		{
			PaintControl(control);
			control.ToolTip = message;
		}
		public static void Invalidate(this ComboBox control)
		{
			control.Invalidate("Este campo é obrigatório");
		}
		public static void Invalidate(this CheckBox control, string message)
		{
			PaintControl(control);
			control.ToolTip = message;
		}
		public static void Invalidate(this CheckBox control)
		{
			control.Invalidate("Este campo é obrigatório");
		}
		public static void Invalidate(this ListView control, string message)
		{
			PaintControl(control);
			control.ToolTip = message;
		}
		public static void Invalidate(this ListView control)
		{
			control.Invalidate("Este campo é obrigatório");
		}
		public static void Invalidate(this VIBlend.WPF.Controls.DateTimePicker control, string message)
		{
			PaintControl(control);
			control.ToolTip = message;
		}
		public static void Invalidate(this VIBlend.WPF.Controls.DateTimePicker control)
		{
			control.Invalidate("Este campo é obrigatório");
		}
		public static void Warn(this TextBox control)
		{
			control.Warn("Atenção à este campo pois algo pode dar errado");
		}
		public static void Warn(this TextBox control, string message)
		{
			PaintControl(control, ControlPaintType.Warning);
			control.ToolTip = message;
		}
		public static void Warn(this ComboBox control)
		{
			control.Warn("Atenção à este campo pois algo pode dar errado");
		}
		public static void Warn(this ComboBox control, string message)
		{
			PaintControl(control, ControlPaintType.Warning);
			control.ToolTip = message;
		}
		public static void Warn(this CheckBox control)
		{
			control.Warn("Atenção à este campo pois algo pode dar errado");
		}
		public static void Warn(this CheckBox control, string message)
		{
			PaintControl(control, ControlPaintType.Warning);
			control.ToolTip = message;
		}
		public static void Warn(this ListView control)
		{
			control.Warn("Atenção à este campo pois algo pode dar errado");
		}
		public static void Warn(this ListView control, string message)
		{
			PaintControl(control, ControlPaintType.Warning);
			control.ToolTip = message;
		}
		public static void Warn(this VIBlend.WPF.Controls.DateTimePicker control)
		{
			control.Warn("Atenção à este campo pois algo pode dar errado");
		}
		public static void Warn(this VIBlend.WPF.Controls.DateTimePicker control, string message)
		{
			PaintControl(control, ControlPaintType.Warning);
			control.ToolTip = message;
		}
		public static void Validate(this TextBox control)
		{
			PaintControl(control, ControlPaintType.Valid);
		}
		public static void Validate(this ComboBox control)
		{
			PaintControl(control, ControlPaintType.Valid);
		}
		public static void Validate(this ListView control)
		{
			PaintControl(control, ControlPaintType.Valid);
		}
		public static void Validate(this CheckBox control)
		{
			PaintControl(control, ControlPaintType.Valid);
		}
		public static void Validate(this VIBlend.WPF.Controls.DateTimePicker control)
		{
			PaintControl(control, ControlPaintType.Valid);
		}
		public static void Update(this TextBox control)
		{
			control.UpdateSource();
			control.UpdateTarget();
			control.Validate();
		}
		public static void UpdateSource(this TextBox control)
		{
			if (control.GetBindingExpression(TextBox.TextProperty) != null)
				control.GetBindingExpression(TextBox.TextProperty).UpdateSource();
		}
		public static void UpdateTarget(this TextBox control)
		{
			if (control.GetBindingExpression(TextBox.TextProperty) != null)
				control.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
		}
		public static void Update(this CurrencyTextBox control)
		{
			if (control.GetBindingExpression(CurrencyTextBox.ValueProperty) != null)
				control.GetBindingExpression(CurrencyTextBox.ValueProperty).UpdateSource();
			if (control.GetBindingExpression(CurrencyTextBox.ValueProperty) != null)
				control.GetBindingExpression(CurrencyTextBox.ValueProperty).UpdateTarget();
			control.Validate();
		}
		public static void UpdateSource(this CurrencyTextBox control)
		{
			if (control.GetBindingExpression(CurrencyTextBox.TextProperty) != null)
				control.GetBindingExpression(CurrencyTextBox.TextProperty).UpdateSource();
		}
		public static void UpdateTarget(this CurrencyTextBox control)
		{
			if (control.GetBindingExpression(CurrencyTextBox.TextProperty) != null)
				control.GetBindingExpression(CurrencyTextBox.TextProperty).UpdateTarget();
		}
		public static void Update(this ListView control)
		{
			if (control.GetBindingExpression(ListView.ItemsSourceProperty) != null)
				control.GetBindingExpression(ListView.ItemsSourceProperty).UpdateSource();
			if (control.GetBindingExpression(ListView.ItemsSourceProperty) != null)
				control.GetBindingExpression(ListView.ItemsSourceProperty).UpdateTarget();
		}
		public static void Update(this ListBox control)
		{
			if (control.GetBindingExpression(ListBox.ItemsSourceProperty) != null)
				control.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateSource();
			if (control.GetBindingExpression(ListBox.ItemsSourceProperty) != null)
				control.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
		}
		public static void Update(this CheckBox control)
		{
			if (control.GetBindingExpression(CheckBox.IsCheckedProperty) != null)
				control.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
			if (control.GetBindingExpression(CheckBox.IsCheckedProperty) != null)
				control.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateTarget();
			control.Validate();
		}
		public static void UpdateSource(this CheckBox control)
		{
			if (control.GetBindingExpression(CheckBox.IsCheckedProperty) != null)
				control.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
		}
		public static void UpdateTarget(this CheckBox control)
		{
			if (control.GetBindingExpression(CheckBox.IsCheckedProperty) != null)
				control.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateTarget();
		}
		public static void Update(this ComboBox control)
		{
			if (control.GetBindingExpression(ComboBox.SelectedValueProperty) != null)
				control.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
			if (control.GetBindingExpression(ComboBox.SelectedValueProperty) != null)
				control.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateTarget();
			control.Validate();
		}
		public static void UpdateSource(this ComboBox control)
		{
			if (control.GetBindingExpression(ComboBox.SelectedValueProperty) != null)
				control.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateSource();
		}
		public static void UpdateTarget(this ComboBox control)
		{
			if (control.GetBindingExpression(ComboBox.SelectedValueProperty) != null)
				control.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateTarget();
		}
		public static void Update(this VIBlend.WPF.Controls.DateTimePicker control)
		{
			control.UpdateSource();
			control.UpdateTarget();
			control.Validate();
		}
		public static void UpdateSource(this VIBlend.WPF.Controls.DateTimePicker control)
		{
			if (control.GetBindingExpression(VIBlend.WPF.Controls.DateTimePicker.SelectedDateProperty) != null)
				control.GetBindingExpression(VIBlend.WPF.Controls.DateTimePicker.SelectedDateProperty).UpdateSource();
		}
		public static void UpdateTarget(this VIBlend.WPF.Controls.DateTimePicker control)
		{
			if (control.GetBindingExpression(VIBlend.WPF.Controls.DateTimePicker.SelectedDateProperty) != null)
				control.GetBindingExpression(VIBlend.WPF.Controls.DateTimePicker.SelectedDateProperty).UpdateTarget();
		}
		public static void Update(this VIBlend.WPF.Controls.DateTimeEditor control)
		{
			control.UpdateSource();
			control.UpdateTarget();
			control.Validate();
		}
		public static void UpdateSource(this VIBlend.WPF.Controls.DateTimeEditor control)
		{
			if (control.GetBindingExpression(VIBlend.WPF.Controls.DateTimeEditor.ValueProperty) != null)
				control.GetBindingExpression(VIBlend.WPF.Controls.DateTimeEditor.ValueProperty).UpdateSource();
		}
		public static void UpdateTarget(this VIBlend.WPF.Controls.DateTimeEditor control)
		{
			if (control.GetBindingExpression(VIBlend.WPF.Controls.DateTimeEditor.ValueProperty) != null)
				control.GetBindingExpression(VIBlend.WPF.Controls.DateTimeEditor.ValueProperty).UpdateTarget();
		}
		public static string GetDescription(this Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute attribute =
				(DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
			return attribute == null ? value.ToString() : attribute.Description;
		}
		public static bool ColumnExists(this DataRow dr, int columnIndex)
		{
			return columnIndex < dr.ItemArray.Count();
		}
	}
	public enum ControlPaintType
	{
		Valid = 16777215, Error = 16766424, Warning = 16641961
	}
	public class BoolToObjectConverter : IValueConverter
	{
		public object TrueValue { get; set; }
		public object FalseValue { get; set; }
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? TrueValue : FalseValue;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	public class BoolToImageConverter : IValueConverter
	{
		public string FalsePath { get; set; }
		public string TruePath { get; set; }
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? TruePath : FalsePath;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	public class EnumToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((Enum)value).GetDescription();
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
