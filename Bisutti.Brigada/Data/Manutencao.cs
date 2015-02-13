using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Bisutti.Brigada.Data
{
	public class Manutencao : DataAccessBase<Model.Manutencao>
	{
		public override void Update(Model.Manutencao entity)
		{
			Model.Manutencao original = context.Manutencao.FirstOrDefault(l => l.Id == entity.Id);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.Entry(original).State = System.Data.Entity.EntityState.Modified;
			context.SaveChanges();
		}
		public override DbEntityEntry GetCurrent(Model.Manutencao entity)
		{
			return context.Entry(entity);
		}
		public override void Insert(Model.Manutencao entity)
		{
			context.Manutencao.Add(entity);
			context.SaveChanges();
		}
		protected override List<Model.Manutencao> GetCollection()
		{
			return context.Manutencao.OrderBy(c => c.Nome).ToList();
		}
	}
}
