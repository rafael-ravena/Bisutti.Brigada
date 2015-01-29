using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Bisutti.Brigada
{
	public static class ConfigurationFacade
	{
		public static string EmailBrigadaModel
		{
			get
			{
				if (Properties.Settings.Default.BrigadaEmailFile == string.Empty)
				{
					Properties.Settings.Default.BrigadaEmailFile = "C:\\BrigadaBisutti\\email.htm";
					Properties.Settings.Default.Save();
				}
				return Properties.Settings.Default.BrigadaEmailFile;
			}
			set
			{
				Properties.Settings.Default.BrigadaEmailFile = value;
				Properties.Settings.Default.Save();
			}
		}
		public static string SubjectBrigada
		{
			get
			{
				if (Properties.Settings.Default.BrigadaSubject == string.Empty)
				{
					Properties.Settings.Default.BrigadaSubject = "Eventos de {0} a {1}";
					Properties.Settings.Default.Save();
				}
				return Properties.Settings.Default.BrigadaSubject;
			}
			set
			{
				Properties.Settings.Default.BrigadaSubject = value;
				Properties.Settings.Default.Save();
			}
		}
		public static string EmailProducaoModel
		{
			get
			{
				if (Properties.Settings.Default.ProducaoEmailFile == string.Empty)
				{
					Properties.Settings.Default.ProducaoEmailFile = "C:\\BrigadaBisutti\\email.htm";
					Properties.Settings.Default.Save();
				}
				return Properties.Settings.Default.ProducaoEmailFile;
			}
			set
			{
				Properties.Settings.Default.ProducaoEmailFile = value;
				Properties.Settings.Default.Save();
			}
		}
		public static string SubjectProducao
		{
			get
			{
				if (Properties.Settings.Default.ProducaoSubject == string.Empty)
				{
					Properties.Settings.Default.ProducaoSubject = "Eventos de {0} a {1}";
					Properties.Settings.Default.Save();
				}
				return Properties.Settings.Default.ProducaoSubject;
			}
			set
			{
				Properties.Settings.Default.ProducaoSubject = value;
				Properties.Settings.Default.Save();
			}
		}
		public static bool SendAutomatically
		{
			get
			{
				return Properties.Settings.Default.SendAutomatically;
			}
			set
			{
				Properties.Settings.Default.SendAutomatically = value;
				Properties.Settings.Default.Save();
			}
		}
		public static DateTime Inicio
		{
			get
			{
				if (Properties.Settings.Default.Inicio == null)
				{
					Properties.Settings.Default.Inicio = DateTime.Now;
					Properties.Settings.Default.Save();
				}
				return Properties.Settings.Default.Inicio;
			}
			set
			{
				Properties.Settings.Default.Inicio = value;
				Properties.Settings.Default.Save();
			}
		}
		public static DateTime Termino
		{
			get
			{
				if (Properties.Settings.Default.Termino == null)
				{
					Properties.Settings.Default.Termino = DateTime.Now;
					Properties.Settings.Default.Save();
				}
				return Properties.Settings.Default.Termino;
			}
			set
			{
				Properties.Settings.Default.Termino = value;
				Properties.Settings.Default.Save();
			}
		}
		public static bool Debug
		{
			get
			{
				return Properties.Settings.Default.Debug;
			}
		}
		public static string DevelopersEmail
		{
			get
			{
				if (Properties.Settings.Default.DevelopersEmail == string.Empty)
				{
					Properties.Settings.Default.DevelopersEmail = "rafael.ravena@gmail.com";
					Properties.Settings.Default.Save();
				}
				return Properties.Settings.Default.DevelopersEmail;
			}
			set
			{
				Properties.Settings.Default.DevelopersEmail = value;
				Properties.Settings.Default.Save();
			}
		}
		public static bool SaveLastScreen
		{
			get
			{
				return Properties.Settings.Default.SaveLastScreen;
			}
			set
			{
				Properties.Settings.Default.SaveLastScreen = value;
				Properties.Settings.Default.Save();
			}
		}
		public static string LastScreen
		{
			get
			{
				if (Properties.Settings.Default.LastScreen == string.Empty)
				{
					Properties.Settings.Default.LastScreen = "Casas";
					Properties.Settings.Default.Save();
				}
				return Properties.Settings.Default.LastScreen;
			}
			set
			{
				Properties.Settings.Default.LastScreen = value;
				Properties.Settings.Default.Save();
			}
		}
		public static int[] BrigadaExcelOrder
		{
			get
			{
				if (Properties.Settings.Default.BrigadaExcelOrder == null)
				{
					Properties.Settings.Default.BrigadaExcelOrder = "0,1,2,3,4,5,6,7";
					Properties.Settings.Default.Save();
				}
				int[] ret = new int[Properties.Settings.Default.BrigadaExcelOrder.Split(',').Count()];
				string[] vals = Properties.Settings.Default.BrigadaExcelOrder.Split(',');
				for (int i = 0; i < vals.Length; i++)
					ret[i] = int.Parse(vals[i]);
				return ret;
			}
			set
			{
				string val = string.Empty;
				foreach (int pos in value)
					val += "," + pos.ToString();
				Properties.Settings.Default.BrigadaExcelOrder = val.Substring(1);
				Properties.Settings.Default.Save();
			}
		}
		public static int[] EventosExcelOrder
		{
			get
			{
				if (Properties.Settings.Default.EventosExcelOrder == null)
				{
					Properties.Settings.Default.EventosExcelOrder = "0,1,2,3,4,5,6,7";
					Properties.Settings.Default.Save();
				}
				int[] ret = new int[Properties.Settings.Default.EventosExcelOrder.Split(',').Count()];
				string[] vals = Properties.Settings.Default.EventosExcelOrder.Split(',');
				for (int i = 0; i < vals.Length; i++)
					ret[i] = int.Parse(vals[i]);
				return ret;
			}
			set
			{
				string val = string.Empty;
				foreach (int pos in value)
					val += "," + pos.ToString();
				Properties.Settings.Default.EventosExcelOrder = val.Substring(1);
				Properties.Settings.Default.Save();
			}
		}
		public static string FormatedBrigadaSubject
		{
			get
			{
				return string.Format(SubjectBrigada, Inicio.ToString("dd/MM/yyyy"), Termino.ToString("dd/MM/yyyy"));
			}
		}
		public static string FormatedProducaoSubject
		{
			get
			{
				return string.Format(SubjectProducao, Inicio.ToString("dd/MM/yyyy"), Termino.ToString("dd/MM/yyyy"));
			}
		}
		public static string LastExcelFile
		{
			get
			{
				return Properties.Settings.Default.LastExcelFile;
			}
			set
			{
				Properties.Settings.Default.LastExcelFile = value;
				Properties.Settings.Default.Save();
			}
		}
	}
}
