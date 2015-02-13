using Bisutti.Brigada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bisutti.Brigada.Core
{
	public class Listener
	{
		private Data.Context _context;
		private Data.Context context
		{
			get
			{
				if (_context == null)
					_context = new Data.Context();
				return _context;
			}
		}
		public void FindInconsistency(DateTime inicio, DateTime termino)
		{
			ClearInconsistencies();
			ProcessBrigada(inicio, termino);
			ProcessEventos(inicio, termino);
			ProcessColaboradores(inicio, termino);
			ProcessDJs(inicio, termino);
			ProcessProdutoras(inicio, termino);
		}
		private void ClearInconsistencies()
		{
			context.Database.ExecuteSqlCommand("DELETE FROM Inconsistencias");
		}
		private void ProcessBrigada(DateTime inicio, DateTime termino)
		{
			List<Model.Brigada> brigada =
				context.Brigada
				.Include(b => b.Colaborador)
				.Include(b => b.Evento)
				.Include(b => b.TipoBrigada)
				.Include(b => b.Evento.Localizacao)
				.Include(b => b.Evento.Produtora)
				.Where(b => b.Evento.Data.CompareTo(inicio) >= 0 && b.Evento.Data.CompareTo(termino) <= 0)
				.ToList();
			for (int i = 0; i < brigada.Count; i++)
				for (int j = i + 1; j < brigada.Count; j++)
					if (
						brigada[i].ColaboradorId == brigada[j].ColaboradorId
						&& brigada[i].Evento.Data.ToString("yyyyMMdd") == brigada[j].Evento.Data.ToString("yyyyMMdd")
						&& (brigada[i].Evento.TerminoValue < brigada[i].Evento.InicioValue ? brigada[i].Evento.TerminoValue + 60 * 24 : brigada[i].Evento.TerminoValue) >= brigada[j].Evento.InicioValue
						&& brigada[i].Evento.LocalizacaoId != brigada[j].Evento.LocalizacaoId
						)
						new Data.Inconsistencia().Insert(new Inconsistencia
						{
							Source = "Eventos",
							SourceIds = brigada[i].Evento.Id,
							Message = string.Format(
								"Colaborador {0} está em {1}, na {2}, produzido por {3}, fazendo {6} e na {4}, produzido por {5}, fazendo {7} ao mesmo tempo",
								brigada[i].Colaborador.Nome,
								brigada[i].Evento.Data.ToString("dd/MM/yyyy"),
								brigada[i].Evento.Localizacao.Nome,
								brigada[i].Evento.Produtora.Nome,
								brigada[j].Evento.Localizacao.Nome,
								brigada[j].Evento.Produtora.Nome,
								brigada[i].TipoBrigada.Nome,
								brigada[j].TipoBrigada.Nome)
						});
		}
		private void ProcessEventos(DateTime inicio, DateTime termino)
		{
			ProcessEventosDuplicado(inicio, termino);
			ProcessEventosOS(inicio, termino);
			ProcessEventosAnexo(inicio, termino);
			ProcessEventosContratante(inicio, termino);
		}
		private void ProcessEventosDuplicado(DateTime inicio, DateTime termino)
		{
			List<Evento> eventos =
				context.Evento
				.Include(e => e.Produtora)
				.Include(e => e.Localizacao)
				.Where(e => e.Data.CompareTo(inicio) >= 0 && e.Data.CompareTo(termino) <= 0)
				.ToList();
			for (int i = 0; i < eventos.Count; i++)
				for (int j = i + 1; j < eventos.Count; j++ )
					if(
						eventos[i].Data.ToString("yyyyMMdd") == eventos[j].Data.ToString("yyyyMMdd")
						&& (eventos[i].TerminoValue < eventos[i].InicioValue ? eventos[i].TerminoValue + 60 * 24 : eventos[i].TerminoValue) >= eventos[j].InicioValue
						&& eventos[i].LocalizacaoId == eventos[j].LocalizacaoId
						)
					new Data.Inconsistencia().Insert(new Inconsistencia
					{
						Source = "Eventos",
						SourceIds = eventos[i].Id,
						Message = string.Format("Evento de {0}, na {1}, produzido por {2} ocorre durante outo evento no mesmo local", eventos[i].Data.ToString("dd/MM/yyyy"), eventos[i].Localizacao.Nome, eventos[i].Produtora.Nome)
					});
		}
		private void ProcessEventosOS(DateTime inicio, DateTime termino)
		{
			foreach (Evento e in
				context.Evento
				.Include(e => e.Produtora)
				.Include(e => e.Localizacao)
				.Where(e => e.Data.CompareTo(inicio) >= 0 && e.Data.CompareTo(termino) <= 0)
				)
				if (!System.IO.File.Exists(e.Anexo))
					new Data.Inconsistencia().Insert(new Inconsistencia
					{
						Source = "Eventos",
						SourceIds = e.Id,
						Message = string.Format("Evento de {0}, na {1}, produzido por {2} possui OS inválida (o arquivo foi movido ou excluido)", e.Data.ToString("dd/MM/yyyy"), e.Localizacao.Nome, e.Produtora.Nome)
					});
		}
		private void ProcessEventosAnexo(DateTime inicio, DateTime termino)
		{
			foreach (Evento e in
				context.Evento
				.Include(e => e.Produtora)
				.Include(e => e.Localizacao)
				.Where(e => e.Data.CompareTo(inicio) >= 0 && e.Data.CompareTo(termino) <= 0
					&& (e.Anexo == null || e.Anexo == string.Empty)
					)
				)
				new Data.Inconsistencia().Insert(new Inconsistencia
				{
					Source = "Eventos",
					SourceIds = e.Id,
					Message = string.Format("Evento de {0}, na {1}, produzido por {2} sem OS", e.Data.ToString("dd/MM/yyyy"), e.Localizacao.Nome, e.Produtora.Nome)
				});
		}
		private void ProcessEventosContratante(DateTime inicio, DateTime termino)
		{
			foreach (Evento e in
				context.Evento
				.Include(e => e.Produtora)
				.Include(e => e.Localizacao)
				.Where(e => e.Data.CompareTo(inicio) >= 0 && e.Data.CompareTo(termino) <= 0
					&& (e.Contratante == null || e.Contratante == string.Empty)
					)
				)
				new Data.Inconsistencia().Insert(new Inconsistencia
				{
					Source = "Eventos",
					SourceIds = e.Id,
					Message = string.Format("Evento de {0}, na {1}, produzido por {2} sem contratante", e.Data.ToString("dd/MM/yyyy"), e.Localizacao.Nome, e.Produtora.Nome)
				});
		}
		private void ProcessColaboradores(DateTime inicio, DateTime termino)
		{
			ProcessColaboradoresTelefone(inicio, termino);
			ProcessColaboradoresEmail(inicio, termino);
		}
		private void ProcessColaboradoresTelefone(DateTime inicio, DateTime termino)
		{
			foreach (Colaborador c in
				context.Colaborador
				.Include(c => c.Eventos)
				.Include(c => c.Eventos.Select(e => e.Evento))
				.Where(c => c.Eventos.Where(e => e.Evento.Data.CompareTo(inicio) >= 0 && e.Evento.Data.CompareTo(termino) <= 0).Count() > 0
					&& (c.Telefone == null || c.Telefone == string.Empty))
					)
				new Data.Inconsistencia().Insert(new Inconsistencia { Source = "Colaboradores", SourceIds = c.Id, Message = c.Nome + " sem número de telefone" });
		}
		private void ProcessColaboradoresEmail(DateTime inicio, DateTime termino)
		{
			foreach (Colaborador c in
				context.Colaborador
				.Include(c => c.Eventos)
				.Include(c => c.Eventos.Select(e => e.Evento))
				.Where(c => c.Eventos.Where(e => e.Evento.Data.CompareTo(inicio) >= 0 && e.Evento.Data.CompareTo(termino) <= 0).Count() > 0
					&& (c.Email == null || c.Email == string.Empty))
					)
				new Data.Inconsistencia().Insert(new Inconsistencia { Source = "Colaboradores", SourceIds = c.Id, Message = c.Nome + " sem endereço de e-mail" });
		}
		private void ProcessDJs(DateTime inicio, DateTime termino)
		{
			ProcessDJsTelefone(inicio, termino);
			ProcessDJsEmail(inicio, termino);
		}
		private void ProcessDJsTelefone(DateTime inicio, DateTime termino)
		{
			foreach (DJ d in
				context.DJ
				.Include(d => d.Eventos)
				.Where(d => d.Eventos.Where(e => e.Data.CompareTo(inicio) >= 0 && e.Data.CompareTo(termino) <= 0).Count() > 0
					&& (d.Email == null || d.Email == string.Empty))
					)
				new Data.Inconsistencia().Insert(new Inconsistencia { SourceIds = d.Id, Source = "DJs", Message = d.Nome + " sem número de telefone" });
		}
		private void ProcessDJsEmail(DateTime inicio, DateTime termino)
		{
			foreach (DJ d in
				context.DJ
				.Include(d => d.Eventos)
				.Where(d => d.Eventos.Where(e => e.Data.CompareTo(inicio) >= 0 && e.Data.CompareTo(termino) <= 0).Count() > 0
					&& (d.Email == null || d.Email == string.Empty))
					)
				new Data.Inconsistencia().Insert(new Inconsistencia { SourceIds = d.Id, Source = "DJs", Message = d.Nome + " sem endereço de e-mail" });
		}
		private void ProcessProdutoras(DateTime inicio, DateTime termino)
		{
			foreach (Produtora p in
				context.Produtora
				.Include(p => p.Eventos)
				.Where(p => p.Eventos.Where(e => e.Data.CompareTo(inicio) >= 0 && e.Data.CompareTo(termino) <= 0).Count() > 0
					&& (p.Email == null || p.Email == string.Empty))
				)
				new Data.Inconsistencia().Insert(new Inconsistencia { SourceIds = p.Id, Source = "Produtoras", Message = p.Nome + " sem endereço de e-mail" });
		}
	}
}
