using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bisutti.Brigada.Model
{
	public class Inconsistencia : IEntityBase
	{
		public int Id { get; set; }
		public string Source { get; set; }
		public string Message { get; set; }
		public int SourceIds { get; set; }
	}
}
