using Bisutti.Brigada.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// Interaction logic for Casa.xaml
	/// </summary>
	public partial class Casa : UserControl, IFormsBase<Localizacao>
	{
		public Casa()
		{
			InitializeComponent();
			DataContext = this;
			ClearForm();
		}
		List<Localizacao> casas;
		public List<Localizacao> Collection
		{
			get
			{
				return casas;
			}
			set
			{
				casas = value;
			}
		}
		public Localizacao localizacao;
		public Localizacao Element { get; set; }
		public void LoadData()
		{
			Element = new Localizacao();
			Collection = new Data.Localizacao().Filter(txbFilterSigla.Text);
		}
		public void RefreshBindings()
		{
			TxbNome.Update();
			TxbSigla.Update();
			TxbEndereco.Update();
			lstCasas.Update();
		}
		public void ClearForm()
		{
			LoadData();
			RefreshBindings();
		}
		public bool IsValid()
		{
			bool retValue = true;
			if (TxbNome.Text == string.Empty)
			{
				TxbNome.Invalidate("O nome da casa é obrigatório");
				retValue = false;
			}
			if (TxbSigla.Text == string.Empty)
			{
				TxbSigla.Invalidate("A sigla é obrigatóra");
				retValue = false;
			}
			if (TxbEndereco.Text == string.Empty)
			{
				TxbEndereco.Invalidate("O endereço da casa é obrigatório");
				retValue = false;
			}
			return retValue;
		}
		private void EditCasaClicked(object sender, MouseButtonEventArgs e)
		{
			Element = (Model.Localizacao)((Button)sender).DataContext;
			RefreshBindings();
		}
		private void DeleteCasaClicked(object sender, MouseButtonEventArgs e)
		{
			new Data.Localizacao().Delete(((Model.Localizacao)((Button)sender).DataContext).Id);
			ClearForm();
		}
		private void FilterTextChanged(object sender, KeyEventArgs e)
		{
			ClearForm();
		}
		private void SalvarClicked(object sender, RoutedEventArgs e)
		{
			if (!IsValid())
				return;
			if (Element.Id == 0)
				new Data.Localizacao().Insert(Element);
			else
				new Data.Localizacao().Update(Element);
			ClearForm();
		}
		private void CancelarClicked(object sender, RoutedEventArgs e)
		{
			ClearForm();
		}
		private void GridToggle(object sender, RoutedEventArgs e)
		{
			BitmapImage image = new BitmapImage();
			image.BeginInit();
			if (GridForm.Visibility == Visibility.Visible)
			{
				image.UriSource = new Uri("/Images/down.png", UriKind.Relative);
				GridForm.Visibility = Visibility.Collapsed;
			}
			else
			{
				image.UriSource = new Uri("/Images/up.png", UriKind.Relative);
				GridForm.Visibility = System.Windows.Visibility.Visible;
			}
			image.EndInit();
			ImgGrid.Source = image;
		}
	}
}
