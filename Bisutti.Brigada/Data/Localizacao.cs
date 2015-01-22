using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bisutti.Brigada.Data
{
	public class Localizacao : DataAccessBase<Model.Localizacao>
	{
		public override void Update(Model.Localizacao entity)
		{
			Model.Localizacao original = context.Localizacoes.FirstOrDefault(l => l.Id == entity.Id);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.Entry(original).State = System.Data.Entity.EntityState.Modified;
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Localizacao entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Localizacao entity)
		{
			context.Localizacoes.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Localizacao> GetCollection()
		{
			return context.Localizacoes.OrderBy(l => l.Nome).ToList();
		}

		public Model.Localizacao GetByName(string sigla)
		{
			return GetCollection().FirstOrDefault(l => l.Sigla.ToLower().Replace(sigla.ToLower(), "") != l.Sigla.ToLower());
		}

		public List<Model.Localizacao> Filter(string sigla)
		{
			if (sigla.Length == 0)
				return GetCollection();
			return GetCollection().Where(l => l.Sigla.ToLower().StartsWith(sigla.ToLower())).ToList();
		}
	}
}
