using System.Threading;
using System.Threading.Tasks;

namespace BaseDotnet.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
