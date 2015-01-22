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
	/// Interaction logic for Colaborador.xaml
	/// </summary>
	public partial class Colaborador : UserControl, IFormsBase<Model.Colaborador>
	{
		public Dictionary<string, int> disponibilidades;
		public Dictionary<string, int> Disponibilidades
		{
			get
			{
				if (disponibilidades == null)
				{
					disponibilidades = new Dictionary<string, int>();
					IEnumerable<Model.DisponibilidadeDiaria> dispds = Enum.GetValues(typeof(Model.DisponibilidadeDiaria)).Cast<Model.DisponibilidadeDiaria>();
					foreach (Model.DisponibilidadeDiaria disp in dispds)
						disponibilidades.Add(disp.GetDescription(), (int)disp);
				}
				return disponibilidades;
			}
		}
		public Colaborador()
		{
			InitializeComponent();
			DataContext = this;
			ClearForm();
		}
		public void RefreshBindings()
		{
			txbDadosBancarios.Update();
			TxbEmail.Update();
			TxbNome.Update();
			TxbObservacoes.Update();
			TxbReferencia.Update();
			TxbTelefone.Update();
			CboDisponibilidade.Update();
			CkbCerimonial.Update();
			CkbChapelaria.Update();
			CkbRecepcao.Update();
			CkbProducao.Update();
			LstColaboradores.Update();
			SharedMethods.PaintControl(PnDisponibilidade, ControlPaintType.Valid);
		}
		public bool IsValid()
		{
			bool ret = true;
			if (TxbNome.Text == string.Empty)
			{
				TxbNome.Invalidate("O nome do colaborador é obrigatorio.");
				ret = false;
			}
			if (TxbEmail.Text == string.Empty)
			{
				TxbEmail.Invalidate("O e-mail é obrigatório.");
				ret = false;
			}
			if (TxbTelefone.Text == string.Empty)
			{
				TxbTelefone.Invalidate("O telefone é obrigatório.");
				ret = false;
			}
			if (CboDisponibilidade.SelectedIndex == -1)
			{
				CboDisponibilidade.Invalidate("A disponibilidade é obrigatória.");
				ret = false;
			}
			if (!(CkbCerimonial.IsChecked.Value || CkbChapelaria.IsChecked.Value || CkbRecepcao.IsChecked.Value))
			{
				SharedMethods.PaintControl(PnDisponibilidade, ControlPaintType.Warning);
				CkbCerimonial.Warn("Selecione ao menos um tipo de brigada que este colaborador está apto.");
				CkbChapelaria.Warn("Selecione ao menos um tipo de brigada que este colaborador está apto.");
				CkbRecepcao.Warn("Selecione ao menos um tipo de brigada que este colaborador está apto.");
				ret = false;
			}
			return ret;
		}
		public List<Model.Colaborador> Collection { get; set; }
		public Model.Colaborador Element { get; set; }
		public void ClearForm()
		{
			LoadData();
			RefreshBindings();
		}
		public void LoadData()
		{
			Element = new Model.Colaborador();
			Collection = new Data.Colaborador().Filter(
				txbFilterNome.Text,
				CkbFilterCerimonial.IsChecked.Value, CkbFilterChapelaria.IsChecked.Value, CkbFilterRecepcao.IsChecked.Value, CkbFilterProducao.IsChecked.Value,
				CboFilterDisponibilidade.SelectedValue);
			RefreshBindings();
		}
		private void EditColaboradorClicked(object sender, MouseButtonEventArgs e)
		{
			Element = (Model.Colaborador)((Button)sender).DataContext;
			RefreshBindings();
		}
		private void DeleteColaboradorClicked(object sender, MouseButtonEventArgs e)
		{
			new Data.Colaborador().Delete(((Model.Colaborador)((Button)sender).DataContext).Id);
			ClearForm();

		}
		private void FilterText(object sender, KeyEventArgs e)
		{
			ClearForm();
		}
		private void FilterCombo(object sender, SelectionChangedEventArgs e)
		{
			ClearForm();
		}
		private void FilterCheckBox(object sender, RoutedEventArgs e)
		{
			ClearForm();
		}
		private void SalvarClicked(object sender, RoutedEventArgs e)
		{
			if (!IsValid())
				return;
			if (Element.Id == 0)
				new Data.Colaborador().Insert(Element);
			else
				new Data.Colaborador().Update(Element);
			ClearForm();
		}
		private void CancelClicked(object sender, RoutedEventArgs e)
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
