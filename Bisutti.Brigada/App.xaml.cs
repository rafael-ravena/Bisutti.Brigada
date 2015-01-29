using Bisutti.Brigada.UserControls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using vib = VIBlend.WPF.Controls;

namespace Bisutti.Brigada
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
		}
		private void ExceptionThrown(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			ErrorWindow err = new ErrorWindow(e.Exception);
			err.ShowDialog();
			e.Handled = true;
		}
		private void ApplicationStartup(object sender, StartupEventArgs e)
		{
			EventManager.RegisterClassHandler(typeof(TextBox), TextBox.KeyDownEvent, new KeyEventHandler(KeyPressed));
			EventManager.RegisterClassHandler(typeof(ComboBox), ComboBox.KeyDownEvent, new KeyEventHandler(KeyPressed));
			EventManager.RegisterClassHandler(typeof(CheckBox), CheckBox.KeyDownEvent, new KeyEventHandler(KeyPressed));
			EventManager.RegisterClassHandler(typeof(CurrencyTextBox), CurrencyTextBox.KeyDownEvent, new KeyEventHandler(KeyPressed));
			EventManager.RegisterClassHandler(typeof(vib.DateTimeEditor), vib.DateTimeEditor.PreviewKeyDownEvent, new KeyEventHandler(KeyPressed));
			EventManager.RegisterClassHandler(typeof(vib.DateTimePicker), vib.DateTimePicker.PreviewKeyDownEvent, new KeyEventHandler(KeyPressed));
		}
		private void KeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Enter) return;
			// Creating a FocusNavigationDirection object and setting it to a
			// local field that contains the direction selected.
			FocusNavigationDirection focusDirection = FocusNavigationDirection.Next;
			// MoveFocus takes a TranversalRequest as its argument.
			TraversalRequest request = new TraversalRequest(focusDirection);
			// Gets the element with keyboard focus.
			UIElement elementWithFocus = (UIElement)Keyboard.FocusedElement;
			// Change keyboard focus.
			if (elementWithFocus != null)
				if (elementWithFocus.MoveFocus(request))
					e.Handled = true;
		}
	}
}
