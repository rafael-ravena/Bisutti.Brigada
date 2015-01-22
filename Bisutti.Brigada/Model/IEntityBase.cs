using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bisutti.Brigada.Model
{
	public interface IEntityBase
	{
		int Id { get; set; }
		//PropertyChanged.ChangeAndNotify(ref id, value, () => Id);
	}
}
