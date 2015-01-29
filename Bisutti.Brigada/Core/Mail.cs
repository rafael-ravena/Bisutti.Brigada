using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using OutlookApp = Microsoft.Office.Interop.Outlook.Application;
using System.Globalization;
using System.IO;
using System.Windows;

namespace Bisutti.Brigada
{
	public class Mail
	{
		private const string brigadaEventoBody = @"
<p class='brigada-item'>
	<span class='brigada-title'>{Data} das {hIni} &agrave;s {hTer} (Chegar &agrave;s {hChegar})</span><br />
	<b>{TipoBrigada}</b><br />
	{TipoEvento} ({Contratante})<br />
	<b>Produtora:</b> {Produtora}<br />
	<b>Casa:</b> {NomeCasa} ({EnderecoCasa})<br />
	<b>Pagamento:</b> R$ {ValorPaga}
	{Contatos}
</p>
";
		private const string producaoEventoBody = @"
<b>{Data} das {hIni} &agrave;s {hTer}</b> &mdash; {NomeCasa}<br />
Tipo de evento: {TipoEvento} &mdash; {Contratante}<br />
Brigada: {Brigada}<br />
";
		private OutlookApp application;
		private OutlookApp Application
		{
			get
			{
				if (application == null)
					application = new OutlookApp();
				return application;
			}
		}
		private MailItem email;
		private MailItem Email
		{
			get
			{
				if (email == null)
					email = Application.CreateItem(OlItemType.olMailItem);
				return email;
			}
		}
		public static void SendEmail(string subject, string body, string to, bool sendAuto)
		{
			SendEmail(subject, body, to, new List<string>(), sendAuto);
		}
		public static void SendEmail(string subject, string body, string to, List<string> attachments, bool sendAuto)
		{
			try
			{
				Mail me = new Mail();
				me.Email.Subject = subject;
				me.Email.HTMLBody = body;
				for (int i = 0; i < attachments.Count; i++)
					me.Email.Attachments.Add(attachments[i], OlAttachmentType.olByValue, me.Email.Body.Length + 1, attachments[i].Substring(0, attachments[i].LastIndexOf('.')));
				me.Email.To = to;
				me.Email.Display(false);
				if (sendAuto)
					((_MailItem)me.Email).Send();
				me.Kill();
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				MessageBox.Show("Você precisa autorizar o acesso ao Outlook no prompt para que os e-mails sejam enviados ou contenham anexos.\n" + 
				"Marque a caixa \"Permitir acesso por\", selecione a opção \"2 minutos\" ou mais e clique em \"Autorizar\".");
			}
		}
		private void Kill()
		{
			email = null;
			application = null;
		}
		public static string GetDefaultSignature()
		{
			string signaturesDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Signatures";
			string signature = string.Empty;
			DirectoryInfo di = new DirectoryInfo(signaturesDirectory);
			if (di.Exists)
			{
				FileInfo[] signatureFiles = di.GetFiles("*.htm");
				if (signatureFiles.Length > 0)
				{
					StreamReader sr = new StreamReader(signatureFiles[0].FullName, Encoding.Default);
					signature = sr.ReadToEnd();
					if (!string.IsNullOrEmpty(signature))
					{
						string fileName = signatureFiles[0].Name.Replace(signatureFiles[0].Extension, string.Empty);
						signature = signature.Replace(fileName + "_files/", signaturesDirectory + "/" + fileName + "_files/");
					}
				}

			}
			return signature;
		}
		public static string GetBodyBrigada(string nomeDestinatario, string brigada)
		{
			return System.IO.File.ReadAllText(ConfigurationFacade.EmailBrigadaModel).Replace("{Destinatario}", nomeDestinatario).Replace("{Eventos}", brigada).Replace("{Signature}", GetDefaultSignature());
		}
		internal static string GetBodyProducao(string nomeDestinatario, string brigada)
		{
			return System.IO.File.ReadAllText(ConfigurationFacade.EmailProducaoModel).Replace("{Destinatario}", nomeDestinatario).Replace("{Eventos}", brigada).Replace("{Signature}", GetDefaultSignature());
		}
		public static string GetDescricaoEventoBrigada(Model.Evento evento, Model.Brigada brigada)
		{
			string data = evento.Data.ToString("dddd", new CultureInfo("pt-BR")) + ", " + evento.Data.ToString("dd/MM/yyyy") + ",";
			string strRet = brigadaEventoBody.Replace("{TipoBrigada}", brigada.TipoBrigada.Nome);
			strRet = strRet.Replace("{Contratante}", evento.Contratante);
			strRet = strRet.Replace("{TipoEvento}", evento.TipoEvento.ToString());
			strRet = strRet.Replace("{Data}", data);
			strRet = strRet.Replace("{hIni}", new Model.Horario().GetFormattedValue(evento.InicioValue));
			strRet = strRet.Replace("{hTer}", new Model.Horario().GetFormattedValue(evento.TerminoValue));
			strRet = strRet.Replace("{hChegar}", new Model.Horario().GetFormattedValue(evento.InicioValue - 60));
			strRet = strRet.Replace("{Produtora}", evento.Produtora.Nome);
			strRet = strRet.Replace("{NomeCasa}", evento.Localizacao.Nome);
			strRet = strRet.Replace("{EnderecoCasa}", evento.Localizacao.Endereco);
			strRet = strRet.Replace("{ValorPaga}", brigada.TipoBrigada.Paga.ToString("0.00"));
			strRet = strRet.Replace("{Contatos}", GetContactList(evento.Colaboradores));
			return strRet;
		}
		public static string GetDescricaoEventoProducao(Model.Evento evento)
		{
			string data = evento.Data.ToString("dd/MM/yyyy") + " (" + evento.Data.ToString("dddd", new CultureInfo("pt-BR")) + ")";
			string strRet = producaoEventoBody.Replace("{Data}", data);
			strRet = strRet.Replace("{hIni}", new Model.Horario().GetFormattedValue(evento.InicioValue));
			strRet = strRet.Replace("{hTer}", new Model.Horario().GetFormattedValue(evento.TerminoValue));
			strRet = strRet.Replace("{NomeCasa}", evento.Localizacao.Nome);
			strRet = strRet.Replace("{TipoEvento}", evento.TipoEvento.ToString());
			strRet = strRet.Replace("{Contratante}", evento.Contratante);
			string strBrigada = string.Empty;
			foreach (Model.Brigada brigada in evento.Colaboradores)
				strBrigada += string.Format("<br /><b>{0}</b>: {1} ({2})", brigada.TipoBrigada.Nome, brigada.Colaborador.Nome, brigada.Colaborador.Telefone);
			strBrigada = "<p style='margin-left:10px;'>" + strBrigada.Substring(6) + "</p>";
			strRet = strRet.Replace("{Brigada}", strBrigada);
			return strRet;
		}
		public static string GetContactList(List<Model.Brigada> list)
		{
			string ret = string.Empty;
			foreach(Model.Brigada item in list)
			{
				ret += string.Format("<br />{0}: {1} ({2}).", item.TipoBrigada.Nome, item.Colaborador.Nome, item.Colaborador.Telefone);
			}
			return ret;
		}
	}
}
