using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bisutti.Brigada.Forms
{
	/// <summary>
	/// Interaction logic for Configuracoes.xaml
	/// </summary>
	public partial class Configuracoes : UserControl
	{
		public Configuracoes()
		{
			InitializeComponent();
			DataContext = this;
		}
		public string DevelopersEmail
		{
			get
			{
				return ConfigurationFacade.DevelopersEmail;
			}
			set
			{
				ConfigurationFacade.DevelopersEmail = value;
			}
		}
		public bool SaveLastPage
		{
			get
			{
				return ConfigurationFacade.SaveLastScreen;
			}
			set
			{
				ConfigurationFacade.SaveLastScreen = value;
			}
		}
		private void ToggleVisibility(Grid grid, Image img)
		{
			BitmapImage image = new BitmapImage();
			image.BeginInit();
			if (grid.Visibility == Visibility.Visible)
			{
				image.UriSource = new Uri("/Images/down.png", UriKind.Relative);
				grid.Visibility = Visibility.Collapsed;
			}
			else
			{
				image.UriSource = new Uri("/Images/up.png", UriKind.Relative);
				grid.Visibility = System.Windows.Visibility.Visible;
			}
			image.EndInit();
			img.Source = image;
		}
		private void AplicativoToggle(object sender, RoutedEventArgs e)
		{
			ToggleVisibility(GridAplicativo, ImgAplicativo);
		}
	}
}
