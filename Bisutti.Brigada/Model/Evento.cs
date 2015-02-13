using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bisutti.Brigada.Model
{
	public class Evento : IEntityBase
	{
		public int Id { get; set; }
		public string Anexo { get; set; }
		public string Contratante { get; set; }
		public DateTime Data { get; set; }
		public int InicioValue { get; set; }
		public int TerminoValue { get; set; }
		public string Comentarios { get; set; }
		public int LocalizacaoId { get; set; }
		public Localizacao Localizacao { get; set; }
		public int ProdutoraId { get; set; }
		public Produtora Produtora { get; set; }
		public int DJId { get; set; }
		public DJ DJ { get; set; }
		public int TipoEventoId { get; set; }
		public TipoEvento TipoEvento
		{
			get { return (TipoEvento)TipoEventoId; }
			set { TipoEventoId = (int)value; }
		}
		public List<Brigada> Colaboradores { get; set; }
		public Horario Inicio
		{
			get
			{

				return Horario.FromInt(InicioValue); 
			}
			set
			{
				InicioValue = value.ToInt();
			}
		}
		public Horario Termino
		{
			get
			{
				return Horario.FromInt(TerminoValue);
			}
			set
			{
				TerminoValue = value.ToInt();
			}
		}
		public DateTime DataInicio
		{
			get
			{
				return Data.AddMinutes(InicioValue);
			}
			set
			{
				InicioValue = (value.Hour * 60) + value.Minute;
			}
		}
		public DateTime DataTermino
		{
			get
			{
				return Data.AddMinutes(TerminoValue);
			}
			set
			{
				TerminoValue = (value.Hour * 60) + value.Minute;
			}
		}
		public bool HasOS
		{
			get
			{
				return Anexo != null && Anexo.Length > 0 && System.IO.File.Exists(Anexo);
			}
		}
	}
	public enum TipoEvento
	{
		[Description("Aniversário")]
		Aniversario = 1, Barmitzva = 2, Batmitzva = 3, Casamento = 4, Corporativo = 5, Debutante = 6, Outro = 7
	}
	public class Horario
	{
		public int Hora { get; set; }
		public int Minuto { get; set; }
		public static Horario Parse(string horario)
		{
			string[] args = horario.Split(':');
			return new Horario { Hora = int.Parse(args[0]), Minuto = int.Parse(args[1]) };
		}
		public string ToString(int timeToAdd)
		{
			return Horario.FromInt(this.ToInt() + timeToAdd).ToString();
		}
		public string GetFormattedValue(int value)
		{
			Hora = (int)Math.Round((decimal)(value / 60), 0);
			Minuto = value - (Hora * 60);
			string h = Hora.ToString();
			if (h.Length < 2)
				h = "0" + h;
			string m = Minuto.ToString();
			if (m.Length < 2)
				m = "0" + m;
			return h + ":" + m;
		}
		public int ToInt()
		{
			return (Hora * 60) + Minuto;
		}
		public static Horario FromInt(int value)
		{
			int h = (int)Math.Round((decimal)(value / 60), 0); 
			int m = value - (h * 60);
			return new Horario { Hora = h, Minuto = m };
		}
	}
}
