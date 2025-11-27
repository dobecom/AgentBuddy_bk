
using Server.Core.Entities.DTO;
using Server.Core.Entities.Records;

namespace Server.Core.Interfaces.IRepositories
{
    public interface ICaseRepository : IBaseRepository<Case>
    {
        //Task<double> PriceCheck(int productId, CancellationToken cancellationToken);
        Task<Case> CreateCase(CaseCreateDTO model, CancellationToken cancellationToken);
    }
}
