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

			context.Produtoras.Add(new Model.Produtora { Nome = "Andrea Sanches", Email = "andrea@villabisutti.com.br" });
			context.Produtoras.Add(new Model.Produtora { Nome = "Camila Gabrielle", Email = "camila@villabisutti.com.br" });
			context.Produtoras.Add(new Model.Produtora { Nome = "Fernanda Maura", Email = "fernanda@villabisutti.com.br" });
			context.Produtoras.Add(new Model.Produtora { Nome = "Graziele Zandarim", Email = "graziele@villabisutti.com.br" });
			context.Produtoras.Add(new Model.Produtora { Nome = "Juliene Campos", Email = "juliene@villabisutti.com.br" });
			context.Produtoras.Add(new Model.Produtora { Nome = "Priscila Silveira", Email = "priscila@villabisutti.com.br" });
			context.Produtoras.Add(new Model.Produtora { Nome = "Sônia Leite", Email = "sonia@villabisutti.com.br" });
			context.Produtoras.Add(new Model.Produtora { Nome = "Tamirys de Azevedo", Email = "tamirys@villabisutti.com.br" });
			context.Produtoras.Add(new Model.Produtora { Nome = "Verônica Helena Tatto", Email = "veronica@villabisutti.com.br" });
			context.SaveChanges();
			context.TiposBrigada.Add(new Model.TipoBrigada { Nome = "Chapelaria Integral", Paga = 80, IncluiAnexo = true });
			context.TiposBrigada.Add(new Model.TipoBrigada { Nome = "Chapelaria Integral", Paga = 100, IncluiAnexo = true });
			context.TiposBrigada.Add(new Model.TipoBrigada { Nome = "Chapelaria Integral", Paga = 120, IncluiAnexo = true });
			context.TiposBrigada.Add(new Model.TipoBrigada { Nome = "Recepção Meia", Paga = 120, IncluiAnexo = false });
			context.TiposBrigada.Add(new Model.TipoBrigada { Nome = "Recepção Meia", Paga = 150, IncluiAnexo = false });
			context.TiposBrigada.Add(new Model.TipoBrigada { Nome = "Recepção Integral", Paga = 150, IncluiAnexo = false });
			context.TiposBrigada.Add(new Model.TipoBrigada { Nome = "Recepção Integral", Paga = 180, IncluiAnexo = false });
			context.SaveChanges();
			context.Localizacoes.Add(new Model.Localizacao { Nome = "Berrini", Endereco = "R. James Joule, 40" });
			context.Localizacoes.Add(new Model.Localizacao { Nome = "Casa do Ator", Endereco = "R. Casa do Ator, 642" });
			context.Localizacoes.Add(new Model.Localizacao { Nome = "Gomes de Carvalho", Endereco = "R. Gomes de carvalho, 420" });
			context.Localizacoes.Add(new Model.Localizacao { Nome = "Quatá", Endereco = "R. Quatá, 567" });
			context.Localizacoes.Add(new Model.Localizacao { Nome = "Tenerife", Endereco = "R. Tenerife, 140" });
			context.Localizacoes.Add(new Model.Localizacao { Nome = "011", Endereco = "R. Alvorada, 180" });
			context.SaveChanges();
			int colaboradorCount = context.Colaboradores.Count();
			List<Model.Evento> eventos = new List<Model.Evento>();
			foreach (Model.Localizacao local in context.Localizacoes)
			{
				for (int i = 0; i < 14; i++)
				{
					int hora = random.Next(17, 21);
					eventos.Add(new Model.Evento { Data = DateTime.Today.AddDays(i + 5), InicioValue = hora * 60, TerminoValue = (hora + 9 - 24) * 60, ProdutoraId = random.Next(1, 10), TipoEventoId = random.Next(1, 8), LocalizacaoId = local.Id, Anexo = "D:\\Planilha Brigada.xlsx" });
					eventos[eventos.Count - 1].Colaboradores = new List<Model.Brigada>();
					eventos[eventos.Count - 1].Colaboradores.Add(new Model.Brigada { ColaboradorId = random.Next(1, colaboradorCount + 1), TipoBrigadaId = 2 });
					eventos[eventos.Count - 1].Colaboradores.Add(new Model.Brigada { ColaboradorId = random.Next(1, colaboradorCount + 1), TipoBrigadaId = 5 });
					eventos[eventos.Count - 1].Colaboradores.Add(new Model.Brigada { ColaboradorId = random.Next(1, colaboradorCount + 1), TipoBrigadaId = 7 });
				}
			}
			context.Eventos.AddRange(eventos);
			context.SaveChanges();
		}
	}
}
