using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bisutti.Brigada.Model
{
	public class Localizacao : IEntityBase
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Sigla { get; set; }
		public string Endereco { get; set; }
		public List<Evento> Eventos { get; set; }
	}
}
