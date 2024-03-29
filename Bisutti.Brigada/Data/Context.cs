﻿using Bisutti.Brigada.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bisutti.Brigada.Data
{
	public class Context : DbContext
	{
		public Context()
			: base("BrigadaBisutti")
		{
			Database.SetInitializer<Context>(new SampleData());
		}
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Model.Produtora>()
				.HasMany(p => p.Eventos)
				.WithRequired(e => e.Produtora)
				.WillCascadeOnDelete(true);
			modelBuilder.Entity<Model.Localizacao>()
				.HasMany(p => p.Eventos)
				.WithRequired(e => e.Localizacao)
				.WillCascadeOnDelete(true);
			modelBuilder.Entity<Model.TipoBrigada>()
				.HasMany(t => t.BrigadasAtribuidas)
				.WithRequired(b => b.TipoBrigada)
				.WillCascadeOnDelete(true);
			modelBuilder.Entity<Model.Colaborador>()
				.HasMany(c => c.Eventos)
				.WithRequired(b => b.Colaborador)
				.WillCascadeOnDelete(true);
			modelBuilder.Entity<Model.Evento>()
				.HasMany(e => e.Colaboradores)
				.WithRequired(c => c.Evento)
				.WillCascadeOnDelete(true);
		}
		public DbSet<Model.Brigada> Brigada { get; set; }
		public DbSet<Model.Colaborador> Colaborador { get; set; }
		public DbSet<Model.Evento> Evento { get; set; }
		public DbSet<Model.Localizacao> Localizacao { get; set; }
		public DbSet<Model.Produtora> Produtora { get; set; }
		public DbSet<Model.TipoBrigada> TipoBrigada { get; set; }
		public DbSet<Model.DJ> DJ { get; set; }
		public DbSet<Model.Manutencao> Manutencao { get; set; }
		public DbSet<Model.Inconsistencia> Inconsistencia { get; set; }
	}
}
