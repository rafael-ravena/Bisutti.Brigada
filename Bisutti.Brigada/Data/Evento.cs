using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bisutti.Brigada.Data
{
	public class Evento : DataAccessBase<Model.Evento> 
	{
		public override void Update(Model.Evento entity)
		{
			Model.Evento original = context.Evento.Include("Colaboradores").FirstOrDefault(l => l.Id == entity.Id);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.Entry(original).State = System.Data.Entity.EntityState.Modified;
			context.SaveChanges();
		}
		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Evento entity)
		{
			return context.Entry(entity);
		}
		public override void Insert(Model.Evento entity)
		{
			context.Evento.Add(entity);
			context.SaveChanges();
		}
		protected override List<Model.Evento> GetCollection()
		{
			return context.Evento
				.Include("Produtora")
				.Include("Localizacao")
				.Include("Colaboradores").Include("Colaboradores.Colaborador").Include("Colaboradores.TipoBrigada")
				.OrderBy(e => e.Localizacao.Nome)
				.OrderBy(e => e.InicioValue).OrderByDescending(e => e.Data)
				.ToList();
		}
		public List<Model.Evento> Filter(DateTime inicio, DateTime termino, int produtoraId, int localizacaoId)
		{
			return context.Evento
				.Include("Produtora")
				.Include("Localizacao")
				.Include("DJ")
				.Include("Colaboradores").Include("Colaboradores.Colaborador").Include("Colaboradores.TipoBrigada")
				.Where(e => 
					(e.Data.CompareTo(inicio) >= 0 && e.Data.CompareTo(termino) <= 0) &&
					(e.ProdutoraId == produtoraId || produtoraId == 0) &&
					(e.LocalizacaoId == localizacaoId || localizacaoId == 0)
					)
				.OrderBy(e => e.Localizacao.Nome)
				.OrderBy(e => e.InicioValue).OrderByDescending(e => e.Data)
				.ToList();

		}
		public void InsertRange(List<Model.Evento> importar)
		{
			context.Evento.AddRange(importar);
			context.SaveChanges();
		}
	}
}
