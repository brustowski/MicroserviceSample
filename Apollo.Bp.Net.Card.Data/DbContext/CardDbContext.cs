using Apollo.Bp.Net.Card.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Bp.Net.Card.Data.DbContext
{
	public class CardDbContext : Microsoft.EntityFrameworkCore.DbContext, ICardDbContext
	{
		public CardDbContext(DbContextOptions<CardDbContext> options) : base(options)
		{
		}

		public DbSet<Core.Entities.Card> Cards { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CardDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}
	}
}