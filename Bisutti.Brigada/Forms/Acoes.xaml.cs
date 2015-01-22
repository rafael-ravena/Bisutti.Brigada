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
	/// Interaction logic for Acoes.xaml
	/// </summary>
	public partial class Acoes : UserControl
	{
		public Acoes()
		{
			InitializeComponent();
			DataContext = this;
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
		public bool SendAuto
		{
			get
			{
				return ConfigurationFacade.SendAutomatically;
			}
			set
			{
				ConfigurationFacade.SendAutomatically = value;
			}
		}
		public string ProducaoSubject
		{
			get
			{
				return ConfigurationFacade.SubjectProducao;
			}
			set
			{
				ConfigurationFacade.SubjectProducao = value;
			}
		}
		public string ProducaoEmailModel
		{
			get
			{
				return ConfigurationFacade.EmailProducaoModel;
			}
			set
			{
				ConfigurationFacade.EmailProducaoModel = value;
			}
		}
		public string BrigadaSubject
		{
			get
			{
				return ConfigurationFacade.SubjectBrigada;
			}
			set
			{
				ConfigurationFacade.SubjectBrigada = value;
			}
		}
		public string BrigadaEmailModel
		{
			get
			{
				return ConfigurationFacade.EmailBrigadaModel;
			}
			set
			{
				ConfigurationFacade.EmailBrigadaModel = value;
			}
		}
		public DateTime DataInicio
		{
			get
			{
				return ConfigurationFacade.Inicio;
			}
			set
			{
				ConfigurationFacade.Inicio = value;
				txbBrigada.UpdateTarget();
				txbProducao.UpdateTarget();
			}
		}
		public DateTime DataTermino
		{
			get
			{
				return ConfigurationFacade.Termino;
			}
			set
			{
				ConfigurationFacade.Termino = value;
				txbBrigada.UpdateTarget();
				txbProducao.UpdateTarget();
			}
		}
		public string FormatedBrigadaSubject
		{
			get
			{
				return ConfigurationFacade.FormatedBrigadaSubject;
			}
			set { }
		}
		public string FormatedProducaoSubject
		{
			get
			{
				return ConfigurationFacade.FormatedProducaoSubject;
			}
			set { }
		}
		public void SendBrigadaClicked(object sender, RoutedEventArgs e)
		{
			foreach (Model.Colaborador c in new Data.Colaborador().GetBrigada(DataInicio, DataTermino))
			{
				string emails = "";
				List<string> attachments = new List<string>();
				for (int i = 0; i < c.Eventos.Count; i++)
				{
					emails += Mail.GetDescricaoEventoBrigada(c.Eventos[i].Evento, c.Eventos[i]);
					if (c.Eventos[i].TipoBrigada.IncluiAnexo)
						attachments.Add(c.Eventos[i].Evento.Anexo);
				}
				emails = Mail.GetBodyBrigada(c.Nome, emails);
				Mail.SendEmail(ConfigurationFacade.FormatedBrigadaSubject, emails, c.Email, attachments, ConfigurationFacade.SendAutomatically);
			}
		}
		public void SendProducaoClicked(object sender, RoutedEventArgs e)
		{
			foreach(Model.Produtora p in new Data.Produtora().GetBrigada(DataInicio, DataTermino))
			{
				string emails = "";
				List<string> attachments = new List<string>();
				for (int i = 0; i < p.Eventos.Count; i++)
				{
					emails += Mail.GetDescricaoEventoProducao(p.Eventos[i]);
					attachments.Add(p.Eventos[i].Anexo);
				}
				emails = Mail.GetBodyProducao(p.Nome, emails);
				Mail.SendEmail(ConfigurationFacade.FormatedProducaoSubject, emails, p.Email, attachments, ConfigurationFacade.SendAutomatically);
			}
		}
		private void BrigadaToggle(object sender, RoutedEventArgs e)
		{
			ToggleVisibility(GridBrigada, ImgBrigada);
		}
		private void ProducaoToggle(object sender, RoutedEventArgs e)
		{
			ToggleVisibility(GridProducao, ImgProducao);
		}
		private void SearchFileClicked(object sender, RoutedEventArgs e)
		{
			string which = ((Button)sender).Tag.ToString();
			OpenFileDialog file = new OpenFileDialog();
			if (file.ShowDialog() == true)
			{
				switch (which)
				{
					case "Brigada":
						BrigadaEmailModel = file.FileName;
						txbEmailBrigada.UpdateTarget();
						break;
					case "Producao":
						ProducaoEmailModel = file.FileName;
						txbEmailProducao.UpdateTarget();
						break;
				}
			}
		}
		private void EmailToggle(object sender, RoutedEventArgs e)
		{
			ToggleVisibility(GridEmail, ImgEmail);
		}
	}
}
