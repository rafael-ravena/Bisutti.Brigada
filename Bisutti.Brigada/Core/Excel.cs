using Bisutti.Brigada.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data = Bisutti.Brigada.Data;

namespace Bisutti.Brigada.Core
{
	public static class Excel
	{
		private const string cnstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR={1}'";
		public static string[] GetSheetNames(string filePath)
		{
			List<string> ret = new List<string>();
			using (OleDbConnection cn = new OleDbConnection(string.Format(cnstr, filePath, "yes")))
			{
				cn.Open();
				DataTable dt = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
				cn.Close();
				cn.Dispose();
				for (int i = 0; i < dt.Rows.Count; i++)
					if (dt.Rows[i][2].ToString().ToLower().Replace("filterdatabase", "") == dt.Rows[i][2].ToString().ToLower())
						ret.Add(dt.Rows[i][2].ToString());
			}
			return ret.ToArray();
		}
		public static DataTable GetTable(string filePath, string sheet, bool FirstRowHeader)
		{
			using (OleDbConnection conn = new OleDbConnection(string.Format(cnstr, filePath, FirstRowHeader ? "yes" : "no")))
			{
				conn.Open();
				DataTable dt = new DataTable();
				using (OleDbCommand cmd = new OleDbCommand("SELECT * from [" + sheet + "$]", conn))
				{
					using (OleDbDataReader rdr = cmd.ExecuteReader())
					{
						dt.Load(rdr);
						return dt;
					}
				}
			}
		}
		public static List<Evento> ImportEventos(string filePath, string sheet, bool FirstRowHeader)
		{
			List<Evento> eventos = new List<Evento>();
			foreach (DataRow dr in GetTable(filePath, sheet, FirstRowHeader).Rows)
				eventos.Add(new Evento
				{
					Data = dr.ColumnExists(ConfigurationFacade.EventosExcelOrder[0]) ?
						DateTime.Parse(dr[ConfigurationFacade.EventosExcelOrder[0]].ToString()) : DateTime.MinValue,
					Contratante = dr.ColumnExists(ConfigurationFacade.EventosExcelOrder[1]) ?
						dr[ConfigurationFacade.EventosExcelOrder[1]].ToString() : string.Empty,
					Inicio = dr.ColumnExists(ConfigurationFacade.EventosExcelOrder[2]) ?
						Horario.Parse(dr[ConfigurationFacade.EventosExcelOrder[2]].ToString()) : new Horario { Hora = 0, Minuto = 0 },
					Termino = dr.ColumnExists(ConfigurationFacade.EventosExcelOrder[3]) ?
						Horario.Parse(dr[ConfigurationFacade.EventosExcelOrder[3]].ToString()) : new Horario { Hora = 0, Minuto = 0 },
					ProdutoraId = dr.ColumnExists(ConfigurationFacade.EventosExcelOrder[4]) ?
						new Data.Produtora().GetByNome(dr[ConfigurationFacade.EventosExcelOrder[4]].ToString()).Id : 0,
					LocalizacaoId = dr.ColumnExists(ConfigurationFacade.EventosExcelOrder[5]) ?
						new Data.Localizacao().GetByName(dr[ConfigurationFacade.EventosExcelOrder[5]].ToString()).Id : 0,
					TipoEvento = dr.ColumnExists(ConfigurationFacade.EventosExcelOrder[6]) ?
						(TipoEvento)Enum.Parse(typeof(TipoEvento), dr[ConfigurationFacade.EventosExcelOrder[6]].ToString(), true) : TipoEvento.Outro,
					Anexo = dr.ColumnExists(ConfigurationFacade.EventosExcelOrder[7]) ?
						dr[ConfigurationFacade.EventosExcelOrder[7]].ToString() : string.Empty
				});
			return eventos;
		}
		public static List<Colaborador> ImportColaboradores(string filePath, string sheet, bool FirstRowHeader)
		{
			List<Colaborador> colaboradores = new List<Colaborador>();
			foreach (DataRow dr in GetTable(filePath, sheet, FirstRowHeader).Rows)
				colaboradores.Add(new Colaborador
				{
					Nome = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[0]) ?
						dr[ConfigurationFacade.BrigadaExcelOrder[0]].ToString() : string.Empty,
					Email = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[1]) ?
						dr[ConfigurationFacade.BrigadaExcelOrder[1]].ToString() : string.Empty,
					Telefone = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[2]) ?
						dr[ConfigurationFacade.BrigadaExcelOrder[2]].ToString() : string.Empty,
					Disponibilidade = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[3]) ?
						GetFromText(dr[ConfigurationFacade.BrigadaExcelOrder[3]].ToString()) : DisponibilidadeDiaria.Seg,
					DisponivelCerimonial = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[4]) ?
						GetCerimonialFromText(dr[ConfigurationFacade.BrigadaExcelOrder[4]].ToString()) : false,
					DisponivelChapelaria = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[4]) ?
						GetChapelariaFromText(dr[ConfigurationFacade.BrigadaExcelOrder[4]].ToString()) : false,
					DisponivelRecepcao = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[4]) ?
						GetRecepcaoFromText(dr[ConfigurationFacade.BrigadaExcelOrder[4]].ToString()) : false,
					DisponivelProducao = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[4]) ?
						GetProducaoFromText(dr[ConfigurationFacade.BrigadaExcelOrder[4]].ToString()) : false,
					Referencia = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[5]) ?
						dr[ConfigurationFacade.BrigadaExcelOrder[5]].ToString() : string.Empty,
					DadosBancarios = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[6]) ?
						dr[ConfigurationFacade.BrigadaExcelOrder[6]].ToString() : string.Empty,
					Observacoes = dr.ColumnExists(ConfigurationFacade.BrigadaExcelOrder[7]) ?
						dr[ConfigurationFacade.BrigadaExcelOrder[7]].ToString() : string.Empty
				});
			return colaboradores;
		}
		private static bool GetCerimonialFromText(string text)
		{
			return text.ToLower().Replace("cerimoial", "") != text.ToLower();
		}
		private static bool GetChapelariaFromText(string text)
		{
			return text.ToLower().Replace("chapelaria", "") != text.ToLower();
		}
		private static bool GetRecepcaoFromText(string text)
		{
			return text.ToLower().Replace("recepção", "") != text.ToLower();
		}
		private static bool GetProducaoFromText(string text)
		{
			return text.ToLower().Replace("produção", "") != text.ToLower();
		}
		private static Model.DisponibilidadeDiaria GetFromText(string text)
		{
			if (text.ToLower().Replace("sab", "") != text.ToLower() && text.ToLower().Replace("dom", "") != text.ToLower())
				return DisponibilidadeDiaria.SabDom;
			if (text.ToLower().Replace("tod", "") != text.ToLower())
				return DisponibilidadeDiaria.Todos;
			if (text.ToLower().Replace("sema", "") != text.ToLower())
				return DisponibilidadeDiaria.Sem;
			if (text.ToLower().Replace("seg", "") != text.ToLower())
				return DisponibilidadeDiaria.Seg;
			if (text.ToLower().Replace("ter", "") != text.ToLower())
				return DisponibilidadeDiaria.Ter;
			if (text.ToLower().Replace("qua", "") != text.ToLower())
				return DisponibilidadeDiaria.Qua;
			if (text.ToLower().Replace("qui", "") != text.ToLower())
				return DisponibilidadeDiaria.Qui;
			if (text.ToLower().Replace("sex", "") != text.ToLower())
				return DisponibilidadeDiaria.Sex;
			if (text.ToLower().Replace("sab", "") != text.ToLower())
				return DisponibilidadeDiaria.Sab;
			if (text.ToLower().Replace("dom", "") != text.ToLower())
				return DisponibilidadeDiaria.Dom;
			return DisponibilidadeDiaria.Fds;
		}
	}
}
