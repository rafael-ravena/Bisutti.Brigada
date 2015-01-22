using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bisutti.Brigada.Model
{
	public class TipoBrigada : IEntityBase
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public decimal Paga { get; set; }
		public bool IncluiAnexo { get; set; }
		public List<Brigada> BrigadasAtribuidas { get; set; }
	}
}
