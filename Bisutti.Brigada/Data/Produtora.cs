using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bisutti.Brigada.Data
{
	public class Produtora : DataAccessBase<Model.Produtora>
	{
		public override void Update(Model.Produtora entity)
		{
			Model.Produtora original = context.Produtoras.FirstOrDefault(l => l.Id == entity.Id);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.Entry(original).State = System.Data.Entity.EntityState.Modified;
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Produtora entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Produtora entity)
		{
			context.Produtoras.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Produtora> GetCollection()
		{
			return context.Produtoras.OrderBy(p => p.Nome).ToList();
		}
		public Model.Produtora GetByNome(string nome)
		{
			return GetCollection().FirstOrDefault(p => p.Nome.ToLower().Replace(nome.ToLower(), string.Empty) != p.Nome.ToLower());
		}

		internal List<Model.Produtora> GetBrigada(DateTime dataInicio, DateTime dataTermino)
		{
			List<Model.Produtora> produtoras =
				context.Produtoras
				.Include(p => p.Eventos)
				.Include(p => p.Eventos.Select(e => e.Localizacao))
				.Include(p => p.Eventos.Select(e => e.Colaboradores))
				.Include(p => p.Eventos.Select(e => e.Colaboradores.Select(c => c.Colaborador)))
				.Include(p => p.Eventos.Select(e => e.Colaboradores.Select(c => c.TipoBrigada)))
				.Where(p => p.Eventos.Where(e => e.Data.CompareTo(dataInicio) >= 0 && e.Data.CompareTo(dataTermino) <= 0).Count() > 0)
				.OrderBy(p => p.Nome)
				.ToList();
			foreach (Model.Produtora p in produtoras)
				p.Eventos = p.Eventos.Where(e => e.Data.CompareTo(dataInicio) >= 0 && e.Data.CompareTo(dataTermino) <= 0).OrderBy(e => e.Data).ToList();
			return produtoras;
		}
	}
}
