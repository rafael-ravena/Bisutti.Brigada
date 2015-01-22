using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bisutti.Brigada.Model
{
	public class Produtora : IEntityBase
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public List<Evento> Eventos { get; set; }
	}
}
