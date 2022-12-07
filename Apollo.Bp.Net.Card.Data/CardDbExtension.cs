using Apollo.Bp.Net.Card.Core.Interfaces;
using Apollo.Bp.Net.Card.Data.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apollo.Bp.Net.Card.Data
{
	public static class CardDbExtension
	{
		public static void AddPaymentDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			var postgresOptions = configuration.GetSection("PostgresOptions").Get<PostgresOptions>();
			services.AddSingleton(postgresOptions);

			var name = typeof(CardDbContext).Assembly.FullName;

			services.AddDbContext<ICardDbContext, CardDbContext>(
				opt => opt.UseNpgsql(postgresOptions.ConnectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(name)));
		}

		public static void RunDbContextMigration(this IApplicationBuilder app)
		{
			using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var dbContext = scope.ServiceProvider.GetService<ICardDbContext>();

			DbInitializer.Initialize(dbContext);
		}
	}
}