using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bisutti.Brigada.Data
{
	public class DJ : DataAccessBase<Model.DJ>
	{
		public override void Update(Model.DJ entity)
		{
			Model.DJ original = context.DJ.FirstOrDefault(d => d.Id == entity.Id);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.Entry(original).State = System.Data.Entity.EntityState.Modified;
			context.SaveChanges();
		}
		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.DJ entity)
		{
			return context.Entry(entity);
		}
		public override void Insert(Model.DJ entity)
		{
			context.DJ.Add(entity);
			context.SaveChanges();
		}
		protected override List<Model.DJ> GetCollection()
		{
			return context.DJ.OrderBy(d => d.Nome).ToList();
		}

		public Model.DJ GetByName(string nome)
		{
			return GetCollection().FirstOrDefault(d => d.Nome.ToLower().Replace(nome.ToLower(), "") != d.Nome.ToLower());
		}
	}
}
