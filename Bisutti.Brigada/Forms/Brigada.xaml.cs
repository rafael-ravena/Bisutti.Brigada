using Bisutti.Brigada.Model;
using System;
using System.Collections.Generic;
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
	/// Interaction logic for Brigada.xaml
	/// </summary>
	public partial class Brigada : UserControl, IFormsBase<TipoBrigada>
	{
		public Brigada()
		{
			InitializeComponent();
			DataContext = this;
			ClearForm();
		}
		public bool IsValid()
		{
			bool ret = true;
			if (TxbNome.Text == string.Empty)
			{
				TxbNome.Invalidate("O tipo de paga é obrigatorio.");
				ret = false;
			}
			if(TxbPaga.Value.Value <= 0)
			{
				TxbPaga.Invalidate("O valor deve ser maior que 0 (zero).");
				ret = false;
			}
			return ret;
		}
		public List<TipoBrigada> Collection { get; set; }
		public TipoBrigada Element { get; set; }
		public void RefreshBindings()
		{
			TxbNome.Update();
			TxbPaga.Update();
			CkbIncluiAnexo.Update();
			LstBrigadas.Update();
		}
		public void ClearForm()
		{
			LoadData();
			RefreshBindings();
		}
		public void LoadData()
		{
			Element = new TipoBrigada();
			Collection = new Data.TipoBrigada().GetCollection(0);
		}
		private void SalvarClicked(object sender, RoutedEventArgs e)
		{
			if (!IsValid())
				return;
			if (Element.Id == 0)
				new Data.TipoBrigada().Insert(Element);
			else
				new Data.TipoBrigada().Update(Element);
			ClearForm();
		}
		private void DescartarClicked(object sender, RoutedEventArgs e)
		{
			ClearForm();
		}
		private void EditBrigadaClicked(object sender, RoutedEventArgs e)
		{
			Element = ((TipoBrigada)((Button)sender).DataContext);
			RefreshBindings();
		}
		private void DeleteBrigadaClicked(object sender, RoutedEventArgs e)
		{
			new Data.TipoBrigada().Delete(((TipoBrigada)((Button)sender).DataContext).Id);
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
