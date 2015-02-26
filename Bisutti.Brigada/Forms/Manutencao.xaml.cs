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
	/// Interaction logic for Manutencao.xaml
	/// </summary>
	public partial class Manutencao : UserControl, IFormsBase<Model.Manutencao>
	{
		public Manutencao()
		{
			InitializeComponent();
			DataContext = this;
			ClearForm();
		}
		public bool IsValid()
		{
			bool ok = true;
			if (txbData.SelectedDate == null)
			{
				txbData.Invalidate("Selecione a data do plantão");
				ok = false;
			}
			if (TxbNome.Text == string.Empty)
			{
				TxbNome.Invalidate("O nome do plantonista é obrigatório.");
				ok = false;
			}
			if (TxbTelefone.Text == string.Empty)
			{
				TxbTelefone.Invalidate("O telefone do plantonista é obrigatório.");
				ok = false;
			}
			return ok;
		}
		public DateTime FilterInicial
		{
			get
			{
				return ConfigurationFacade.Inicio;
			}
			set
			{
				ConfigurationFacade.Inicio = value;
			}
		}
		public DateTime FilterFinal
		{
			get
			{
				return ConfigurationFacade.Termino;
			}
			set
			{
				ConfigurationFacade.Termino = value;
			}
		}
		public List<Model.Manutencao> Collection { get; set; }
		public Model.Manutencao Element { get; set; }
		public void ClearForm()
		{
			LoadData();
			RefreshBindings();
		}
		public void RefreshBindings()
		{
			TxbNome.Update();
			TxbTelefone.Update();
			LstManutencao.Update();
		}
		public void LoadData()
		{
			Collection = new Data.Manutencao().Filter(ConfigurationFacade.Inicio, ConfigurationFacade.Termino);
			Element = new Model.Manutencao();
			Element.Data = ConfigurationFacade.Inicio;
		}
		private void SalvarClicked(object sender, RoutedEventArgs e)
		{
			if (!IsValid())
				return;
			if (Element.Id == 0)
				new Data.Manutencao().Insert(Element);
			else
				new Data.Manutencao().Update(Element);
			ClearForm();
		}
		private void DescartarClicked(object sender, RoutedEventArgs e)
		{
			ClearForm();
		}
		private void EditElementClicked(object sender, RoutedEventArgs e)
		{
			Element = ((Model.Manutencao)((Button)sender).DataContext);
			RefreshBindings();
		}
		private void DeleteElementClicked(object sender, RoutedEventArgs e)
		{
			new Data.DJ().Delete(((Model.Produtora)((Button)sender).DataContext).Id);
			ClearForm();
		}
		public void FilterChanged(object sender, VIBlend.WPF.Controls.DateTimePickerSelectionChangedEventArgs e)
		{
			ClearForm();
		}
	}
}
