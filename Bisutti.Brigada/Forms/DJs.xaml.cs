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
	/// Interaction logic for DJs.xaml
	/// </summary>
	public partial class DJs : UserControl, IFormsBase<Model.DJ>
	{
		public DJs()
		{
			InitializeComponent();
			DataContext = this;
			ClearForm();
		}
		public bool IsValid()
		{
			if (TxbNome.Text == string.Empty)
			{
				TxbNome.Invalidate("O nome do DJ é obrigatório.");
				return false;
			}
			if (TxbTelefone.Text == string.Empty)
			{
				TxbTelefone.Invalidate("O telefone do DJ é obrigatório.");
				return false;
			}
			if (TxbEmail.Text == string.Empty)
			{
				TxbEmail.Invalidate("O e-mail do DJ é obrigatório.");
				return false;
			}
			return true;
		}
		public List<Model.DJ> Collection { get; set; }
		public Model.DJ Element { get; set; }
		public void RefreshBindings()
		{
			TxbNome.Update();
			TxbTelefone.Update();
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
			Collection = new Data.DJ().GetCollection(0);
			Element = new Model.DJ();
		}
		private void SalvarClicked(object sender, RoutedEventArgs e)
		{
			if (!IsValid())
				return;
			if (Element.Id == 0)
				new Data.DJ().Insert(Element);
			else
				new Data.DJ().Update(Element);
			ClearForm();
		}
		private void DescartarClicked(object sender, RoutedEventArgs e)
		{
			ClearForm();
		}
		private void EditElementClicked(object sender, RoutedEventArgs e)
		{
			Element = ((Model.DJ)((Button)sender).DataContext);
			RefreshBindings();
		}
		private void DeleteElementClicked(object sender, RoutedEventArgs e)
		{
			new Data.DJ().Delete(((Model.Produtora)((Button)sender).DataContext).Id);
			ClearForm();
		}
	}
}
