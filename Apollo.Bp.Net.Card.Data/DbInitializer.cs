using Apollo.Bp.Net.Card.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Bp.Net.Card.Data
{
	public static class DbInitializer
	{
		public static void Initialize(ICardDbContext context)
		{
			context.Database.Migrate();
		}
	}
}