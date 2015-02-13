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

namespace Bisutti.Brigada
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			State = MenuState.Expanded;
			ExpandOrCollapse();
			SwitchMainControl(ConfigurationFacade.LastScreen);
			this.WindowState = System.Windows.WindowState.Maximized;
		}
		public UserControl CurrentControl { get; set; }
		public string CurrentAction { get; set; }
		public MenuState State { get; set; }
		public void Toggle()
		{
			if (State == MenuState.Expanded)
				State = MenuState.Collapsed;
			else
				State = MenuState.Expanded;
			ExpandOrCollapse();
		}
		private void ExpandOrCollapse()
		{
			if (State == MenuState.Collapsed)
				Menu.Width = 30;
			else
				Menu.Width = 180;
		}
		private void ToggleButtonClicked(object sender, RoutedEventArgs e)
		{
			Toggle();
		}
		public void SwitchMainControl(string which)
		{
			if (which == CurrentAction)
				return;
			if (CurrentControl != null)
			{
				ContentPanel.Children.Remove(CurrentControl);
				CurrentControl.Finalize();
			}
			CurrentAction = which;
			switch (which)
			{
				case "Acoes":
					CurrentControl = new Forms.Acoes();
					this.Title = "Envio de e-mails à brigada no período";
					break;
				case "Casas":
					CurrentControl = new Forms.Casa();
					this.Title = "Gerenciamento das casas (localidades)";
					break;
				case "Colaboradores":
					CurrentControl = new Forms.Colaborador();
					this.Title = "Gerenciamento dos colaboradores (brigada)";
					break;
				case "Configuracoes":
					CurrentControl = new Forms.Configuracoes();
					this.Title = "Configurações básicas";
					break;
				case "Importacoes":
					CurrentControl = new Forms.Importacoes();
					this.Title = "Importação de planilhas excel para o aplicativo";
					break;
				case "Eventos":
					CurrentControl = new Forms.Evento();
					this.Title = "Gerenciamento dos eventos no período";
					break;
				case "Produtoras":
					CurrentControl = new Forms.Produtora();
					this.Title = "Gerenciamento das produtoras (atendentes)";
					break;
				case "Brigada":
					CurrentControl = new Forms.Brigada();
					this.Title = "Gerenciamento dos tipos de brigada";
					break;
				case "Manutenção":
					CurrentControl = new Forms.Produtora();
					this.Title = "Gerenciamento dos plantonistas da manutenção por data";
					break;
				case "DJs":
					CurrentControl = new Forms.DJs();
					this.Title = "Gerenciamento dos DJs";
					break;
				default:
					return;
			}
			if (ConfigurationFacade.SaveLastScreen)
				ConfigurationFacade.LastScreen = which;
			ContentPanel.Children.Add(CurrentControl);
		}
		private void DJClicked(object sender, RoutedEventArgs e)
		{
			SwitchMainControl("DJs");
		}
		private void ManutencaoClicked(object sender, RoutedEventArgs e)
		{
			SwitchMainControl("Manutenção");
		}
		private void AcoesClicked(object sender, RoutedEventArgs e)
		{
			SwitchMainControl("Acoes");
		}
		private void CasasClicked(object sender, RoutedEventArgs e)
		{
			SwitchMainControl("Casas");
		}
		private void ColaboradoresClicked(object sender, RoutedEventArgs e)
		{
			SwitchMainControl("Colaboradores");
		}
		private void ConfiguracoesClicked(object sender, RoutedEventArgs e)
		{
			SwitchMainControl("Configuracoes");
		}
		private void EventosClicked(object sender, RoutedEventArgs e)
		{
			SwitchMainControl("Eventos");
		}
		private void ProdutorasClicked(object sender, RoutedEventArgs e)
		{
			SwitchMainControl("Produtoras");
		}
		private void BrigadaClicked(object sender, RoutedEventArgs e)
		{
			SwitchMainControl("Brigada");
		}
		private void ImportacoesClicked(object sender, RoutedEventArgs e)
		{
			SwitchMainControl("Importacoes");
		}
		private void InconsistenciasClicked(object sender, RoutedEventArgs e)
		{
			Inconsistencias wdn = new Inconsistencias(this);
			wdn.Show();
		}
		public enum MenuState
		{
			Collapsed, Expanded
		}

	}
}
