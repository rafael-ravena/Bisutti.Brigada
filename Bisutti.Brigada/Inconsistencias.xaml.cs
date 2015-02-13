using Bisutti.Brigada.Core;
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
using System.Windows.Shapes;

namespace Bisutti.Brigada
{
	/// <summary>
	/// Interaction logic for Inconsistencias.xaml
	/// </summary>
	public partial class Inconsistencias : Window
	{
		private MainWindow Window { get; set; }
		public Inconsistencias(MainWindow window)
		{
			InitializeComponent();
			DataContext = this;
			Window = window;
			new Listener().FindInconsistency(ConfigurationFacade.Inicio, ConfigurationFacade.Termino);
		}

		public List<Inconsistencia> Collection
		{
			get
			{
				return new Data.Inconsistencia().GetCollection(0);
			}
		}
		private void CorrectClicked(object sender, MouseButtonEventArgs e)
		{
			Inconsistencia i = (Inconsistencia)((Button)sender).DataContext;
			Window.SwitchMainControl(i.Source);
			switch(i.Source)
			{
				case "Eventos":
					((Forms.Evento)Window.CurrentControl).Element = new Data.Evento().GetElement(i.SourceIds);
					((Forms.Evento)Window.CurrentControl).RefreshBindings();
					break;
				case "Colaboradores":
					((Forms.Colaborador)Window.CurrentControl).Element = new Data.Colaborador().GetElement(i.SourceIds);
					((Forms.Colaborador)Window.CurrentControl).RefreshBindings();
					break;
				case "DJs":
					((Forms.DJs)Window.CurrentControl).Element = new Data.DJ().GetElement(i.SourceIds);
					((Forms.DJs)Window.CurrentControl).RefreshBindings();
					break;
				case "Produtoras":
					((Forms.Produtora)Window.CurrentControl).Element = new Data.Produtora().GetElement(i.SourceIds);
					((Forms.Produtora)Window.CurrentControl).RefreshBindings();
					break;
			}
		}
	}
}
