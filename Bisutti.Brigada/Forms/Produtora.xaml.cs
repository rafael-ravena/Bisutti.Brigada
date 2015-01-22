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
	/// Interaction logic for Produtora.xaml
	/// </summary>
	public partial class Produtora : UserControl, IFormsBase<Model.Produtora>
	{
		public Produtora()
		{
			InitializeComponent();
			DataContext = this;
			ClearForm();
		}
		public bool IsValid()
		{
			if(TxbNome.Text == string.Empty)
			{
				TxbNome.Invalidate("O nome da Produtora é obrigatório.");
				return false;
			}
			return true;
		}
		public List<Model.Produtora> Collection { get; set; }
		public Model.Produtora Element { get; set; }
		public void RefreshBindings()
		{
			TxbNome.Update();
			TxbEmail.Update();
			LstProdutoras.Update();
		}
		public void ClearForm()
		{
			LoadData();
			RefreshBindings();
		}
		public void LoadData()
		{
			Collection = new Data.Produtora().GetCollection(0);
			Element = new Model.Produtora();
		}
		private void SalvarClicked(object sender, RoutedEventArgs e)
		{
			if (!IsValid())
				return;
			if (Element.Id == 0)
				new Data.Produtora().Insert(Element);
			else
				new Data.Produtora().Update(Element);
			ClearForm();
		}
		private void DescartarClicked(object sender, RoutedEventArgs e)
		{
			ClearForm();
		}
		private void EditProdutoraClicked(object sender, RoutedEventArgs e)
		{
			Element = ((Model.Produtora)((Button)sender).DataContext);
			RefreshBindings();
		}
		private void DeleteProdutoraClicked(object sender, RoutedEventArgs e)
		{
			new Data.Produtora().Delete(((Model.Produtora)((Button)sender).DataContext).Id);
			ClearForm();
		}
	}
}
