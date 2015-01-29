using Bisutti.Brigada.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
	/// Interaction logic for Importacoes.xaml
	/// </summary>
	public partial class Importacoes : UserControl
	{
		public Importacoes()
		{
			InitializeComponent();
			DataContext = this;
			BindSheets();
		}
		public Dictionary<int, string> colunas;
		public Dictionary<int, string> Colunas
		{
			get
			{
				if (colunas == null)
				{
					colunas = new Dictionary<int, string>();
					colunas.Add(int.MaxValue, "!");
					colunas.Add(0, "A");
					colunas.Add(1, "B");
					colunas.Add(2, "C");
					colunas.Add(3, "D");
					colunas.Add(4, "E");
					colunas.Add(5, "F");
					colunas.Add(6, "G");
					colunas.Add(7, "H");
					colunas.Add(8, "I");
					colunas.Add(9, "J");
					colunas.Add(10, "K");
					colunas.Add(11, "L");
					colunas.Add(12, "M");
					colunas.Add(13, "N");
					colunas.Add(14, "O");
					colunas.Add(15, "P");
					colunas.Add(16, "Q");
					colunas.Add(17, "R");
					colunas.Add(18, "S");
					colunas.Add(19, "T");
					colunas.Add(20, "U");
					colunas.Add(21, "V");
					colunas.Add(22, "W");
					colunas.Add(23, "X");
					colunas.Add(24, "Y");
					colunas.Add(25, "Z");
				}
				return colunas;
			}
		}
		public int BrigadaNome
		{
			get
			{
				return ConfigurationFacade.BrigadaExcelOrder[0];
			}
			set
			{
				ConfigurationFacade.BrigadaExcelOrder = new int[8] {
					value, BrigadaEmail, BrigadaTelefone, BrigadaDisponibilidade,
					BrigadaCargo, BrigadaReferencia, BrigadaDadosBancarios, BrigadaObservacoes
				};
			}
		}
		public int BrigadaEmail
		{
			get
			{
				return ConfigurationFacade.BrigadaExcelOrder[1];
			}
			set
			{
				ConfigurationFacade.BrigadaExcelOrder = new int[8] {
					BrigadaNome, value, BrigadaTelefone, BrigadaDisponibilidade,
					BrigadaCargo, BrigadaReferencia, BrigadaDadosBancarios, BrigadaObservacoes
				};
			}
		}
		public int BrigadaTelefone
		{
			get
			{
				return ConfigurationFacade.BrigadaExcelOrder[2];
			}
			set
			{
				ConfigurationFacade.BrigadaExcelOrder = new int[8] {
					BrigadaNome, BrigadaEmail, value, BrigadaDisponibilidade,
					BrigadaCargo, BrigadaReferencia, BrigadaDadosBancarios, BrigadaObservacoes
				};
			}
		}
		public int BrigadaDisponibilidade
		{
			get
			{
				return ConfigurationFacade.BrigadaExcelOrder[3];
			}
			set
			{
				ConfigurationFacade.BrigadaExcelOrder = new int[8] {
					BrigadaNome, BrigadaEmail, BrigadaTelefone, value,
					BrigadaCargo, BrigadaReferencia, BrigadaDadosBancarios, BrigadaObservacoes
				};
			}
		}
		public int BrigadaCargo
		{
			get
			{
				return ConfigurationFacade.BrigadaExcelOrder[4];
			}
			set
			{
				ConfigurationFacade.BrigadaExcelOrder = new int[8] {
					BrigadaNome, BrigadaEmail, BrigadaTelefone, BrigadaDisponibilidade,
					value, BrigadaReferencia, BrigadaDadosBancarios, BrigadaObservacoes
				};
			}
		}
		public int BrigadaReferencia
		{
			get
			{
				return ConfigurationFacade.BrigadaExcelOrder[5];
			}
			set
			{
				ConfigurationFacade.BrigadaExcelOrder = new int[8] {
					BrigadaNome, BrigadaEmail, BrigadaTelefone, BrigadaDisponibilidade,
					BrigadaCargo, value, BrigadaDadosBancarios, BrigadaObservacoes
				};
			}
		}
		public int BrigadaDadosBancarios
		{
			get
			{
				return ConfigurationFacade.BrigadaExcelOrder[6];
			}
			set
			{
				ConfigurationFacade.BrigadaExcelOrder = new int[8] {
					BrigadaNome, BrigadaEmail, BrigadaTelefone, BrigadaDisponibilidade,
					BrigadaCargo, BrigadaReferencia, value, BrigadaObservacoes
				};
			}
		}
		public int BrigadaObservacoes
		{
			get
			{
				return ConfigurationFacade.BrigadaExcelOrder[7];
			}
			set
			{
				ConfigurationFacade.BrigadaExcelOrder = new int[8] {
					BrigadaNome, BrigadaEmail, BrigadaTelefone, BrigadaDisponibilidade,
					BrigadaCargo, BrigadaReferencia, BrigadaDadosBancarios, value
				};
			}
		}
		public int EventoData
		{
			get
			{
				return ConfigurationFacade.EventosExcelOrder[0];
			}
			set
			{
				ConfigurationFacade.EventosExcelOrder = new int[8] {
					value, EventoContratante, EventoInicio, EventoTermino,
					EventoProdutora, EventoLocalizacao, EventoTipo, EventoOS
				};
			}
		}
		public int EventoContratante
		{
			get
			{
				return ConfigurationFacade.EventosExcelOrder[1];
			}
			set
			{
				ConfigurationFacade.EventosExcelOrder = new int[8] {
					EventoData, value, EventoInicio, EventoTermino,
					EventoProdutora, EventoLocalizacao, EventoTipo, EventoOS
				};
			}
		}
		public int EventoInicio
		{
			get
			{
				return ConfigurationFacade.EventosExcelOrder[2];
			}
			set
			{
				ConfigurationFacade.EventosExcelOrder = new int[8] {
					EventoData, EventoContratante, value, EventoTermino,
					EventoProdutora, EventoLocalizacao, EventoTipo, EventoOS
				};
			}
		}
		public int EventoTermino
		{
			get
			{
				return ConfigurationFacade.EventosExcelOrder[3];
			}
			set
			{
				ConfigurationFacade.EventosExcelOrder = new int[8] {
					EventoData, EventoContratante, EventoInicio, value,
					EventoProdutora, EventoLocalizacao, EventoTipo, EventoOS
				};
			}
		}
		public int EventoProdutora
		{
			get
			{
				return ConfigurationFacade.EventosExcelOrder[4];
			}
			set
			{
				ConfigurationFacade.EventosExcelOrder = new int[8] {
					EventoData, EventoContratante, EventoInicio, EventoTermino,
					value, EventoLocalizacao, EventoTipo, EventoOS
				};
			}
		}
		public int EventoLocalizacao
		{
			get
			{
				return ConfigurationFacade.EventosExcelOrder[5];
			}
			set
			{
				ConfigurationFacade.EventosExcelOrder = new int[8] {
					EventoData, EventoContratante, EventoInicio, EventoTermino,
					EventoProdutora, value, EventoTipo, EventoOS
				};
			}
		}
		public int EventoTipo
		{
			get
			{
				return ConfigurationFacade.EventosExcelOrder[6];
			}
			set
			{
				ConfigurationFacade.EventosExcelOrder = new int[8] {
					EventoData, EventoContratante, EventoInicio, EventoTermino,
					EventoProdutora, EventoLocalizacao, value, EventoOS
				};
			}
		}
		public int EventoOS
		{
			get
			{
				return ConfigurationFacade.EventosExcelOrder[7];
			}
			set
			{
				ConfigurationFacade.EventosExcelOrder = new int[8] {
					EventoData, EventoContratante, EventoInicio, EventoTermino,
					EventoProdutora, EventoLocalizacao, EventoTipo, value
				};
			}
		}
		public List<MyDictionaryItem> Sheets { get; set; }
		public string LastExcelFile
		{
			get
			{
				return ConfigurationFacade.LastExcelFile;
			}
			set
			{
				ConfigurationFacade.LastExcelFile = value;
			}
		}
		private void BindSheets()
		{
			Sheets = new List<MyDictionaryItem>();
			if (!File.Exists(LastExcelFile) || LastExcelFile.Substring(LastExcelFile.LastIndexOf('.')).Replace("xls", "").Length > 2)
				return;
			foreach (string s in Excel.GetSheetNames(LastExcelFile))
				Sheets.Add(new MyDictionaryItem(s, true));
			LstSheets.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
		}
		private void TraceTable(string table, int count)
		{
			ImportacaoTrace.Inlines.Add(new Run(table + ": ") { FontWeight = FontWeights.Bold });
			ImportacaoTrace.Inlines.Add(new Run(count.ToString() + " itens lidos."));
			ImportacaoTrace.Inlines.Add(new LineBreak());
		}
		private void TraceResults(int total, int count)
		{
			ImportacaoTrace.Inlines.Add(new Run("Final: ") { FontWeight = FontWeights.Bold });
			ImportacaoTrace.Inlines.Add(new LineBreak());
			ImportacaoTrace.Inlines.Add(new Run(string.Format("{0} lidos; {1} duplicados; ", total, total - count)));
			ImportacaoTrace.Inlines.Add(new LineBreak());
			ImportacaoTrace.Inlines.Add(new Run(count + " importados no total.") { FontWeight = FontWeights.Bold });
		}
		private void ClearTrace()
		{
			ImportacaoTrace.Inlines.Clear();
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
		private void BrigadaToggle(object sender, RoutedEventArgs e)
		{
			ToggleVisibility(GridBrigada, ImgBrigada);
		}
		private void EventoToggle(object sender, RoutedEventArgs e)
		{
			ToggleVisibility(GridEvento, ImgEvento);
		}
		private void ImportacoesToggle(object sender, RoutedEventArgs e)
		{
			ToggleVisibility(GridImportacoes, ImgImportacoes);
		}
		private void SearchFileClicked(object sender, RoutedEventArgs e)
		{
			string which = ((Button)sender).Tag.ToString();
			OpenFileDialog file = new OpenFileDialog();
			file.Multiselect = false;
			if (file.ShowDialog() == true)
			{
				if (file.FileName.Substring(file.FileName.LastIndexOf('.')).Replace("xls", "").Length > 2) // ".xlsx" = ".s" || "." after replace
					return;
				LastExcelFile = file.FileName;
				txbArquivoExcel.UpdateTarget();
				BindSheets();
			}
		}
		public void ImportarBrigada(object sender, RoutedEventArgs e)
		{
			ClearTrace();
			List<Model.Colaborador> importar = new List<Model.Colaborador>();
			foreach (MyDictionaryItem i in Sheets.Where(s => s.Value))
			{
				List<Model.Colaborador> colabs = Excel.ImportColaboradores(txbArquivoExcel.Text, i.Key.Replace("$", ""), true);
				importar.AddRange(colabs);
				TraceTable(i.Key, colabs.Count);
			}
			int qto = importar.Count;
			importar = importar.Where(c => c.Email != null && c.Email != string.Empty && c.Telefone != null && c.Telefone != string.Empty).ToList();
			importar = importar.GroupBy(c => c.Nome).Select(grp => grp.First()).ToList();
			List<Model.Colaborador> existentes = new Data.Colaborador().GetCollection(0);
			foreach (Model.Colaborador c in existentes)
				if (importar.Exists(col => col.Nome.ToLower() == c.Nome.ToLower()))
					importar.Remove(importar.FirstOrDefault(col => col.Nome.ToLower() == c.Nome.ToLower()));
			new Data.Colaborador().InsertRange(importar);
			TraceResults(qto, importar.Count);
		}
		public void ImportarEventos(object sender, RoutedEventArgs e)
		{
			ClearTrace();
			List<Model.Evento> importar = new List<Model.Evento>();
			foreach (MyDictionaryItem i in Sheets.Where(s => s.Value))
			{
				List<Model.Evento> evs = Excel.ImportEventos(txbArquivoExcel.Text, i.Key.Replace("$", ""), true);
				importar.AddRange(evs);
				TraceTable(i.Key, evs.Count);
			}
			int qto = importar.Count;
			importar = importar.Where(evento => evento.Data.CompareTo(DateTime.MinValue) > 0 && evento.ProdutoraId != 0 && evento.LocalizacaoId != 0).ToList();
			List<Model.Evento> existentes = new Data.Evento().GetCollection(0);
			foreach (Model.Evento evento in existentes)
				if (importar.Exists(evt => 
						evt.Data.CompareTo(evento.Data) == 0 &&
						evt.ProdutoraId == evento.ProdutoraId &&
						evt.LocalizacaoId == evento.LocalizacaoId &&
						evt.InicioValue == evento.InicioValue))
					importar.Remove(importar.FirstOrDefault(evt =>
					evt.Data.CompareTo(evento.Data) == 0 &&
					evt.ProdutoraId == evento.ProdutoraId &&
					evt.LocalizacaoId == evento.LocalizacaoId &&
					evt.InicioValue == evento.InicioValue));
			new Data.Evento().InsertRange(importar);
			TraceResults(qto, importar.Count);
		}
		public class MyDictionaryItem
		{
			public string Key { get; set; }
			public bool Value { get; set; }
			public MyDictionaryItem() { }
			public MyDictionaryItem(string key, bool value)
			{
				Key = key;
				Value = value;
			}
		}
	}
}
