using Bisutti.Brigada.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Bisutti.Brigada.Data
{
	public class SampleData : DropCreateDatabaseIfModelChanges<Context>
	{
		Random random = new Random();
		protected override void Seed(Context context)
		{
			base.Seed(context);
			if (!ConfigurationFacade.Debug)
			{
				return;
			}
			string[] lipsum = {
								"Vivamus hendrerit ornare commodo. Praesent pellentesque magna sit amet libero interdum venenatis. Suspendisse blandit ultrices orci, ac ultrices nulla. Proin pellentesque leo vehicula, tincidunt erat a, molestie metus. Nunc convallis, lorem in tincidunt vestibulum, felis justo finibus mi, eget tempus purus nibh vitae eros. Nulla sit amet nibh dapibus, pellentesque lacus vel, aliquam nunc. Cras ultricies massa ut tincidunt accumsan. Sed id augue eu lacus interdum porta. Aliquam varius ante suscipit metus interdum maximus. Sed lacus erat, hendrerit eu urna in, semper auctor tortor. Vestibulum luctus augue in diam sollicitudin, eget elementum nulla posuere. Maecenas a leo a nisi sollicitudin aliquam id ac erat. Curabitur faucibus tincidunt ipsum, auctor tempor risus luctus nec. Nulla euismod nibh elit.",
								"Nunc interdum arcu sit amet turpis tincidunt, sit amet dictum nisi sollicitudin. Duis et blandit odio, sed blandit turpis. Donec dignissim sollicitudin lectus ut varius. Nam iaculis volutpat nunc at bibendum. Cras sed pretium ligula. Proin sit amet leo nulla. Nunc malesuada dolor sit amet justo dapibus rutrum. Nullam egestas tempor diam vel vulputate. Sed nunc risus, porta vel fringilla id, efficitur quis ipsum. Integer vulputate sem nec arcu luctus gravida. Phasellus nec efficitur enim, ut varius odio. Vestibulum nec mattis mi, at consectetur nunc. Mauris magna risus, finibus at lectus ac, fermentum mollis lorem. Mauris orci mi, vulputate id justo id, pulvinar molestie quam.",
								"Mauris non gravida mauris, mollis euismod turpis. Fusce lorem eros, viverra in dignissim vel, ullamcorper at enim. Nulla efficitur at elit at sollicitudin. Suspendisse pharetra sem at lacus hendrerit pharetra. Nullam eget porttitor risus, malesuada accumsan mauris. Proin purus lectus, faucibus eget velit a, consequat faucibus velit. Sed quis dapibus felis. Nullam suscipit et magna ut mollis. Suspendisse ac consequat mauris. Fusce sollicitudin augue nec tellus semper posuere. Proin justo enim, aliquet et euismod sit amet, porta vitae ex. Sed rhoncus elementum orci, vel maximus urna tincidunt ac. Fusce bibendum placerat lobortis. In hac habitasse platea dictumst. Maecenas efficitur imperdiet euismod. In hac habitasse platea dictumst.",
								"Integer tortor eros, mattis a mi finibus, interdum tincidunt quam. Aliquam ut iaculis lacus. Proin non risus suscipit, pharetra dolor nec, malesuada sem. Nulla a ante fringilla, commodo enim vitae, dapibus est. Proin ante tortor, mattis ut sagittis eu, finibus eu massa. Donec interdum faucibus tellus eget tristique. Vivamus rhoncus auctor pellentesque. Pellentesque eu massa convallis, aliquam dui quis, vehicula ipsum. Quisque fringilla lobortis libero, at commodo est malesuada in.",
								"Nam consequat lobortis mi, vel ornare nunc tempor sed. Aliquam in elit efficitur, fermentum nisl id, tincidunt purus. Fusce euismod euismod enim sit amet semper. Fusce vitae tortor convallis, imperdiet quam eget, ornare purus. Nam quis elit eget nisi blandit tristique. Aenean rhoncus eget ligula nec pharetra. Pellentesque pretium justo ac mattis imperdiet. Sed viverra ex id turpis sollicitudin, eu placerat eros sollicitudin.",
								"Curabitur porttitor posuere est eget hendrerit. In hac habitasse platea dictumst. Sed fringilla nec est in auctor. Aliquam dignissim ultrices neque id vulputate. In faucibus, purus lacinia mattis laoreet, purus nunc rhoncus massa, eu bibendum tellus odio quis ligula. Sed cursus elit nec nulla condimentum, viverra consectetur erat mollis. Nulla ac erat nec nibh pellentesque facilisis non at augue. Nunc congue velit sit amet ipsum mattis pellentesque. Nulla maximus ipsum erat. Fusce mollis erat varius felis pharetra, quis maximus mi placerat.",
								"Vivamus vitae ligula eget nunc consectetur mattis. Sed interdum volutpat libero, ut scelerisque ligula facilisis nec. In et rhoncus ex. Quisque facilisis gravida est in vehicula. Mauris at arcu vitae velit pellentesque malesuada. Ut faucibus, risus id sagittis pretium, eros enim tristique lorem, sed volutpat nisi justo in diam. Donec quis auctor orci. Proin fermentum lacinia lobortis. Nulla tincidunt a ipsum et ultrices. Pellentesque consectetur turpis vitae urna varius dignissim.",
								"Fusce ante nunc, aliquam finibus quam at, dapibus scelerisque diam. Duis laoreet sapien malesuada volutpat lacinia. Sed sit amet congue neque. Sed luctus ipsum quis vestibulum mollis. Ut consequat nisi vitae iaculis scelerisque. Fusce faucibus ac nibh sed varius. Proin augue mauris, feugiat sit amet molestie sit amet, semper eu eros. Cras ultrices vitae metus vel tempus. Maecenas viverra ullamcorper ultricies. Praesent vehicula nibh a felis bibendum, ut semper ex mollis. Vestibulum consectetur ligula ut eros lacinia, in feugiat sapien cursus. Sed convallis, orci quis dictum hendrerit, risus turpis euismod purus, non lacinia urna lectus eu purus. Aenean ultrices nibh sit amet ex ultrices, id consectetur lectus egestas. Proin porta felis sit amet odio interdum malesuada. Pellentesque vulputate volutpat tortor a lacinia.",
								"Vivamus laoreet justo ipsum, a volutpat ligula porta a. Phasellus lectus urna, semper non ultrices non, suscipit eget magna. In tristique eget nulla vitae egestas. Sed egestas luctus mi, nec tincidunt lectus fermentum placerat. Nulla ut sapien leo. Ut vitae turpis velit. Vestibulum vitae dolor nunc. Sed tempus interdum felis vel porta. Praesent rutrum sapien at eros scelerisque, sed faucibus metus iaculis. Suspendisse lacus tortor, efficitur sed justo a, pellentesque sagittis augue. Nunc fermentum porttitor odio sit amet fringilla. Integer orci lectus, porta sit amet vestibulum eget, eleifend at turpis. Duis ornare rutrum nulla sed porta. Donec faucibus sit amet lacus non rhoncus. Integer tincidunt urna at eleifend auctor.",
								"Vestibulum mollis arcu eget lectus dapibus posuere. Pellentesque porttitor tempus lacus a bibendum. Aliquam elementum massa sem, nec viverra metus placerat ac. Etiam eu leo sem. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec et ex quis est elementum maximus dapibus vitae enim. In hac habitasse platea dictumst. Maecenas vitae condimentum nisi, id vehicula mi. Morbi sodales, risus et vulputate bibendum, purus turpis elementum orci, in ultrices orci ligula sit amet nunc."
							  };
			ConfigurationFacade.LastExcelFile = "D:\\Planilha Brigada.xlsx";
			Forms.Importacoes imp = new Forms.Importacoes();
			imp.Sheets = new List<Forms.Importacoes.MyDictionaryItem>();
			imp.txbArquivoExcel.Text = ConfigurationFacade.LastExcelFile;
			imp.Sheets.Add(new Forms.Importacoes.MyDictionaryItem { Key = "Plan3", Value = true });
			ConfigurationFacade.BrigadaExcelOrder = new int[8] {0,2,1,3,4,5,6,8};
			imp.ImportarBrigada(new object(), new System.Windows.RoutedEventArgs());
			imp.Sheets[0].Key = "Plan2";
			ConfigurationFacade.BrigadaExcelOrder = new int[8] {0,3,1,4,5,6,7,9};
			imp.ImportarBrigada(new object(), new System.Windows.RoutedEventArgs());
			imp.Sheets[0].Key = "Plan1";
			ConfigurationFacade.BrigadaExcelOrder = new int[8] {0,3,1,4,5,2147483647,2147483647,6};
			imp.ImportarBrigada(new object(), new System.Windows.RoutedEventArgs());

			context.Produtora.Add(new Model.Produtora { Nome = "Andrea Sanches", Email = "andrea@villabisutti.com.br" });
			context.Produtora.Add(new Model.Produtora { Nome = "Camila Gabrielle", Email = "camila@villabisutti.com.br" });
			context.Produtora.Add(new Model.Produtora { Nome = "Fernanda Maura", Email = "fernanda@villabisutti.com.br" });
			context.Produtora.Add(new Model.Produtora { Nome = "Graziele Zandarim", Email = "graziele@villabisutti.com.br" });
			context.Produtora.Add(new Model.Produtora { Nome = "Juliene Campos", Email = "juliene@villabisutti.com.br" });
			context.Produtora.Add(new Model.Produtora { Nome = "Priscila Silveira", Email = "priscila@villabisutti.com.br" });
			context.Produtora.Add(new Model.Produtora { Nome = "Sônia Leite", Email = "sonia@villabisutti.com.br" });
			context.Produtora.Add(new Model.Produtora { Nome = "Tamirys de Azevedo", Email = "tamirys@villabisutti.com.br" });
			context.Produtora.Add(new Model.Produtora { Nome = "Verônica Helena Tatto", Email = "veronica@villabisutti.com.br" });
			context.SaveChanges();
			context.DJ.Add(new Model.DJ { Nome = "Marcos", Email = "marcos@villabisutti.com.br", Telefone = "?" });
			context.SaveChanges();
			context.TipoBrigada.Add(new Model.TipoBrigada { Nome = "Chapelaria Integral", Paga = 80, IncluiAnexo = true });
			context.TipoBrigada.Add(new Model.TipoBrigada { Nome = "Chapelaria Integral", Paga = 100, IncluiAnexo = true });
			context.TipoBrigada.Add(new Model.TipoBrigada { Nome = "Chapelaria Integral", Paga = 120, IncluiAnexo = true });
			context.TipoBrigada.Add(new Model.TipoBrigada { Nome = "Recepção Meia", Paga = 120, IncluiAnexo = false });
			context.TipoBrigada.Add(new Model.TipoBrigada { Nome = "Recepção Meia", Paga = 150, IncluiAnexo = false });
			context.TipoBrigada.Add(new Model.TipoBrigada { Nome = "Recepção Integral", Paga = 150, IncluiAnexo = false });
			context.TipoBrigada.Add(new Model.TipoBrigada { Nome = "Recepção Integral", Paga = 180, IncluiAnexo = false });
			context.SaveChanges();
			context.Localizacao.Add(new Model.Localizacao { Nome = "Berrini", Endereco = "R. James Joule, 40" });
			context.Localizacao.Add(new Model.Localizacao { Nome = "Casa do Ator", Endereco = "R. Casa do Ator, 642" });
			context.Localizacao.Add(new Model.Localizacao { Nome = "Gomes de Carvalho", Endereco = "R. Gomes de carvalho, 420" });
			context.Localizacao.Add(new Model.Localizacao { Nome = "Quatá", Endereco = "R. Quatá, 567" });
			context.Localizacao.Add(new Model.Localizacao { Nome = "Tenerife", Endereco = "R. Tenerife, 140" });
			context.Localizacao.Add(new Model.Localizacao { Nome = "011", Endereco = "R. Alvorada, 180" });
			context.SaveChanges();
			int colaboradorCount = context.Colaborador.Count();
			List<Model.Evento> eventos = new List<Model.Evento>();
			foreach (Model.Localizacao local in context.Localizacao)
			{
				for (int i = 0; i < 14; i++)
				{
					int hora = random.Next(17, 21);
					eventos.Add(new Model.Evento {
						Data = DateTime.Today.AddDays(i + 5),
						InicioValue = hora * 60,
						TerminoValue = (hora + 9 - 24) * 60,
						Comentarios = lipsum[random.Next(0, lipsum.Length)],
						DJId = 1,
						ProdutoraId = random.Next(1, 10),
						TipoEventoId = random.Next(1, 8),
						LocalizacaoId = local.Id,
						Anexo = ConfigurationFacade.LastExcelFile
					});
					eventos[eventos.Count - 1].Colaboradores = new List<Model.Brigada>();
					eventos[eventos.Count - 1].Colaboradores.Add(new Model.Brigada { ColaboradorId = random.Next(1, colaboradorCount + 1), TipoBrigadaId = 2 });
					eventos[eventos.Count - 1].Colaboradores.Add(new Model.Brigada { ColaboradorId = random.Next(1, colaboradorCount + 1), TipoBrigadaId = 5 });
					eventos[eventos.Count - 1].Colaboradores.Add(new Model.Brigada { ColaboradorId = random.Next(1, colaboradorCount + 1), TipoBrigadaId = 7 });
				}
			}
			context.Evento.AddRange(eventos);
			context.SaveChanges();
		}
	}
}
