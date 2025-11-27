
using Server.Core.Common;
using Server.Core.Entities.Common;

namespace Server.Core.Interfaces.IServices
{
    public interface IBaseService<TDTO>
        where TDTO : class
    {
        Task<IEnumerable<TDTO>> GetAll(CancellationToken cancellationToken);
        Task<PaginatedDataDTO<TDTO>> GetPaginatedData(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<PaginatedDataDTO<TDTO>> GetPaginatedData(int pageNumber, int pageSize, List<ExpressionFilter> filters, CancellationToken cancellationToken);
        Task<PaginatedDataDTO<TDTO>> GetPaginatedData(int pageNumber, int pageSize, List<ExpressionFilter> filters, string sortBy, string sortOrder, CancellationToken cancellationToken);
        Task<TDTO> GetById<Tid>(Tid id, CancellationToken cancellationToken);
        Task<bool> IsExists<Tvalue>(string key, Tvalue value, CancellationToken cancellationToken);
        Task<bool> IsExistsForUpdate<Tid>(Tid id, string key, string value, CancellationToken cancellationToken);
    }

}
