using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

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
	}
}
