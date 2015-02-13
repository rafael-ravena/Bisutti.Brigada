using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Bisutti.Brigada.Data
{
	public class Inconsistencia : DataAccessBase<Model.Inconsistencia>
	{
		public override void Update(Model.Inconsistencia entity)
		{
			Model.Inconsistencia original = context.Inconsistencia.FirstOrDefault(l => l.Id == entity.Id);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.Entry(original).State = System.Data.Entity.EntityState.Modified;
			context.SaveChanges();
		}
		public override DbEntityEntry GetCurrent(Model.Inconsistencia entity)
		{
			return context.Entry(entity);
		}
		public override void Insert(Model.Inconsistencia entity)
		{
			context.Inconsistencia.Add(entity);
			context.SaveChanges();
		}
		protected override List<Model.Inconsistencia> GetCollection()
		{
			return context.Inconsistencia.OrderBy(c => c.Source).ToList();
		}
	}
}
