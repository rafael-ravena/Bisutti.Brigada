using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bisutti.Brigada.Data
{
	public class TipoBrigada : DataAccessBase<Model.TipoBrigada>
	{
		public override void Update(Model.TipoBrigada entity)
		{
			Model.TipoBrigada original = context.TiposBrigada.FirstOrDefault(l => l.Id == entity.Id);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.Entry(original).State = System.Data.Entity.EntityState.Modified;
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.TipoBrigada entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.TipoBrigada entity)
		{
			context.TiposBrigada.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.TipoBrigada> GetCollection()
		{
			return context.TiposBrigada.OrderBy(t => t.Nome).ToList();
		}
	}
}
