using Microsoft.Win32;
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
	/// Interaction logic for Evento.xaml
	/// </summary>
	public partial class Evento : UserControl, IFormsBase<Model.Evento>
	{
		public Evento()
		{
			InitializeComponent();
			DataContext = this;
			Brigadas = new Data.TipoBrigada().GetCollection(0);
			Colaboradores = new Data.Colaborador().GetCollection(0);
			Localizacoes = new Data.Localizacao().GetCollection(0);
			Produtoras = new Data.Produtora().GetCollection(0);
			ClearFilterClicked(new object(), new RoutedEventArgs());
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
		private void SalvarClicked(object sender, RoutedEventArgs e)
		{
			if (!IsValid())
				return;
			if (Element.Id == 0)
				new Data.Evento().Insert(Element);
			else
				new Data.Evento().Update(Element);
			ClearForm();
		}
		private void CancelarClicked(object sender, RoutedEventArgs e)
		{
			ClearForm();
		}
		private void DeleteBrigadaClicked(object sender, RoutedEventArgs e)
		{
			new Data.Brigada().Delete(((Model.Brigada)((Button)sender).DataContext).Id);
			ClearForm();
		}
		private void EditEventoClicked(object sender, MouseButtonEventArgs e)
		{
			Element = (Model.Evento)((Button)sender).DataContext;
			RefreshBindings();
		}
		private void DeleteEventoClicked(object sender, MouseButtonEventArgs e)
		{
			int[] brigadaIds = ((Model.Evento)((Button)sender).DataContext).Colaboradores.Select(b => b.Id).ToArray<int>();
			foreach (int id in brigadaIds)
			{
				new Data.Brigada().Delete(id);
			}
			new Data.Evento().Delete(((Model.Evento)((Button)sender).DataContext).Id);
			ClearForm();
		}
		private void AddBrigadaClicked(object sender, RoutedEventArgs e)
		{
			if (!IsBrigadaValid())
				return;
			foreach (Model.Evento evento in LstEventos.SelectedItems.Cast<Model.Evento>().ToList())
			{
				new Data.Brigada().Insert(new Model.Brigada
				{
					EventoId = evento.Id,
					ColaboradorId = ((Model.Colaborador)LstColaboradores.SelectedItem).Id,
					TipoBrigadaId = ((Model.TipoBrigada)CboTipoBrigada.SelectedItem).Id
				});
			}
			ClearForm();
		}
		private void EventoToggle(object sender, RoutedEventArgs e)
		{
			ToggleVisibility(GridEvento, ImgEvento);
		}
		private void BrigadaToggle(object sender, RoutedEventArgs e)
		{
			ToggleVisibility(GridBrigada, ImgBrigada);
		}
		private void FilterChanged(object sender, VIBlend.WPF.Controls.DateTimePickerSelectionChangedEventArgs e)
		{
			ClearForm();
		}
		private void FilterChanged(object sender, SelectionChangedEventArgs e)
		{
			ClearForm();
		}
		private void ClearFilterClicked(object sender, RoutedEventArgs e)
		{
			FilterInicial = DateTime.Now;
			FilterFinal = DateTime.Now.AddDays(7);
			FilterLocal = 0;
			FilterProdutora = 0;
			cboFilterLocal.SelectedIndex = -1;
			cboFilterProdutora.SelectedIndex = -1;
			txbFilterInicial.GetBindingExpression(VIBlend.WPF.Controls.DateTimePicker.SelectedDateProperty).UpdateTarget();
			txbFilterFinal.GetBindingExpression(VIBlend.WPF.Controls.DateTimePicker.SelectedDateProperty).UpdateTarget();
			cboFilterLocal.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateTarget();
			cboFilterProdutora.GetBindingExpression(ComboBox.SelectedValueProperty).UpdateTarget();
		}
		private void SearchFileClicked(object sender, RoutedEventArgs e)
		{
			OpenFileDialog file = new OpenFileDialog();
			if (file.ShowDialog() == true)
			{
				Element.Anexo = file.FileName;
				TxbAnexo.UpdateTarget();
			}
		}
		public DateTime FilterInicial { get; set; }
		public DateTime FilterFinal { get; set; }
		public int FilterLocal { get; set; }
		public int FilterProdutora { get; set; }
		public List<Model.TipoBrigada> Brigadas { get; set; }
		public List<Model.Colaborador> Colaboradores { get; set; }
		public List<Model.Localizacao> Localizacoes { get; set; }
		public List<Model.Produtora> Produtoras { get; set; }
		public Dictionary<string, int> tipos;
		public Dictionary<string, int> Tipos
		{
			get
			{
				if (tipos == null)
				{
					tipos = new Dictionary<string, int>();
					IEnumerable<Model.TipoEvento> tiposEvento = Enum.GetValues(typeof(Model.TipoEvento)).Cast<Model.TipoEvento>();
					foreach (Model.TipoEvento tipoEvento in tiposEvento)
						tipos.Add(tipoEvento.GetDescription(), (int)tipoEvento);
				}
				return tipos;
			}
		}
		public bool IsBrigadaValid()
		{
			LstColaboradores.Validate();
			CboTipoBrigada.Validate();
			LstEventos.Validate();
			bool ok = true;
			if (LstColaboradores.SelectedItem == null)
			{
				LstColaboradores.Invalidate("É necessário selecionar um colaborador.");
				ok = false;
			}
			if (CboTipoBrigada.SelectedItem == null)
			{
				CboTipoBrigada.Invalidate("Selecione um tipo.");
				ok = false;
			}
			if (LstEventos.SelectedItems.Count <= 0)
			{
				LstEventos.Invalidate("Selecione os eventos que deseja adicionar a brigada");
				ok = false;
			}
			return ok;
		}
		public bool IsValid()
		{
			bool ok = true;
			if (txbDataEvento.SelectedDate == null)
			{
				txbDataEvento.Invalidate("Selecione a data do evento");
				ok = false;
			}
			if (txbInicioEvento.Value == null)
			{
				txbInicioEvento.Invalidate("Indique o início do evento");
				ok = false;
			}
			if (txbTerminoEvento.Value == null)
			{
				txbTerminoEvento.Invalidate("Indique o término do evento");
				ok = false;
			}
			if (cboLocalizacao.SelectedIndex < 0)
			{
				cboLocalizacao.Invalidate("Selecione o local do evento.");
				ok = false;
			}
			if (cboProdutora.SelectedIndex < 0)
			{
				cboProdutora.Invalidate("Selecione a produtora do evento.");
				ok = false;
			}
			if (cboTipoEvento.SelectedIndex < 0)
			{
				cboTipoEvento.Invalidate("Selecione o tipo de evento.");
				ok = false;
			}
			if (TxbContratante.Text == null || TxbContratante.Text == string.Empty)
			{
				TxbContratante.Invalidate("Preencha o nome do contratante.");
				ok = false;
			}
			return ok;
		}
		public List<Model.Evento> Collection { get; set; }
		public Model.Evento Element { get; set; }
		public void ClearForm()
		{
			LoadData();
			RefreshBindings();
		}
		public void LoadData()
		{
			Collection = new Data.Evento().Filter(FilterInicial, FilterFinal, FilterProdutora, FilterLocal);
			Element = new Model.Evento { Data = DateTime.Today, InicioValue = 19 * 60, TerminoValue = 4 * 60 };
		}
		public void RefreshBindings()
		{
			txbDataEvento.Update();
			if (txbDataEvento.SelectedDate != Element.Data)
				txbDataEvento.SelectedDate = Element.Data;
			txbInicioEvento.Update();
			txbTerminoEvento.Update();
			cboLocalizacao.Update();
			cboProdutora.Update();
			cboTipoEvento.Update();
			TxbContratante.Update();
			TxbAnexo.Update();
			LstEventos.Update();
		}
	}
}
