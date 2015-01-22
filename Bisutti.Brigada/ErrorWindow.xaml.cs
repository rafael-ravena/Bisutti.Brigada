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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bisutti.Brigada
{
	/// <summary>
	/// Interaction logic for ErrorWindow.xaml
	/// </summary>
	public partial class ErrorWindow : Window
	{
		private const string BreakRule = "\r\n";
		private const string HtmlBreakRule = "<br />";
		private const string Ident = "\t";
		private Exception Exception;
		private bool FeedbackSent { get; set; }
		public ErrorWindow()
		{
			InitializeComponent();
		}
		public ErrorWindow(Exception ex)
		{
			InitializeComponent();
			Exception = ex;
			Erro.Inlines.Add(new Run(Exception.Message) { FontWeight = FontWeights.Bold, FontSize = 15 });
			Erro.Inlines.Add(BreakRule);
			while (Exception.InnerException != null)
			{
				Exception = Exception.InnerException;
				Erro.Inlines.Add(Ident);
				Erro.Inlines.Add(new Run(Exception.Message));
				Erro.Inlines.Add(BreakRule);
			}
			Exception = ex;
			Erro.Inlines.Add(BreakRule);
			Erro.Inlines.Add(Exception.ToString());
			FeedbackSent = false;
		}
		public void Play()
		{
			Storyboard sb = (Storyboard)this.FindResource("FocusInformation");
			Storyboard.SetTarget(sb, Info);
			sb.Begin();
		}
		private void EnviarClicked(object sender, RoutedEventArgs e)
		{
			if (Tela.SelectedItem == null)
			{
				Play();
				Tela.Focus();
				return;
			}
			if(Acao.Text == string.Empty)
			{
				Play();
				Acao.Focus();
				return;
			}
			string msg = string.Format("Tela onde ocorreu o erro: {0}{1}", ((ListBoxItem)Tela.SelectedItem).Name, BreakRule);
			msg += string.Format("Ação do usuário: {0}{1}", Acao.Text, BreakRule);
			msg += "Exception:" + BreakRule;
			foreach (Inline line in Erro.Inlines)
				msg += ((Run)line).Text;
			msg = msg.Replace(BreakRule, HtmlBreakRule);
			Mail.SendEmail("Erro na aplicação Brigada Bisutti", msg, ConfigurationFacade.DevelopersEmail, true);
			FeedbackSent = true;
			this.Close();
		}

		private void CloseClicked(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!FeedbackSent)
			{
				Play();
				e.Cancel = true;
			}
		}

	}
}
