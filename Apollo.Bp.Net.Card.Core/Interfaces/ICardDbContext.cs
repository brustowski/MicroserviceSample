using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Apollo.Bp.Net.Card.Core.Interfaces
{
	public interface ICardDbContext
	{
		DbSet<Entities.Card> Cards { get; set; }

		DatabaseFacade Database { get; }

		int SaveChanges();

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}