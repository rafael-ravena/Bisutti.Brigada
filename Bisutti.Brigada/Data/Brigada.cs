using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bisutti.Brigada.Data
{
	public class Brigada : DataAccessBase<Model.Brigada>
	{
		public override void Update(Model.Brigada entity)
		{
			throw new NotImplementedException();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Brigada entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Brigada entity)
		{
			context.Brigada.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Brigada> GetCollection()
		{
			return context.Brigada.ToList();
		}
		public int[] GetBrigadasId(int eventoId)
		{
			return context.Brigada.Where(b => b.EventoId == eventoId).Select(b => b.Id).ToArray<int>();
		}
	}
}
