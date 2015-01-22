using System.Collections.Generic;
using System.ComponentModel;

namespace Bisutti.Brigada.Model
{
	public class Colaborador : IEntityBase
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Telefone { get; set; }
		public int DisponibilidadeId { get; set; }
		public DisponibilidadeDiaria Disponibilidade 
		{
			get
			{
				return (DisponibilidadeDiaria)DisponibilidadeId;
			}
			set
			{
				DisponibilidadeId = (int)value;
			}
		}
		public bool DisponivelCerimonial { get; set; }
		public bool DisponivelChapelaria { get; set; }
		public bool DisponivelRecepcao { get; set; }
		public bool DisponivelProducao { get; set; }
		public string Referencia { get; set; }
		public string DadosBancarios { get; set; }
		public string Observacoes { get; set; }
		public List<Brigada> Eventos { get; set; }
	}
	public enum DisponibilidadeDiaria
	{
		[Description("Todos os dias")]
		Todos = 0,
		[Description("Domingo")]
		Dom = 1,
		[Description("Segunda-feira")]
		Seg = 2,
		[Description("Terça-feira")]
		Ter = 3,
		[Description("Quarta-feira")]
		Qua = 4,
		[Description("Quinta-feira")]
		Qui = 5,
		[Description("Sexta-feira")]
		Sex = 6,
		[Description("Sábado")]
		Sab = 7,
		[Description("Sábado e domingo")]
		SabDom = 8,
		[Description("Fim de semana")]
		Fds = 9,
		[Description("Dias de semana")]
		Sem = 10
	}
}
