using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Bisutti.Brigada.Model
{
	public class Brigada : IEntityBase
	{
		public int Id { get; set; }
		public int ColaboradorId { get; set; }
		public Colaborador Colaborador { get; set; }
		public int EventoId { get; set; }
		public Evento Evento { get; set; }
		public int TipoBrigadaId { get; set; }
		public TipoBrigada TipoBrigada { get; set; }

	}
}
