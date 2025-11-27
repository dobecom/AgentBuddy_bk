
using Server.Core.Common;
using Server.Core.Entities.Common;
using Server.Core.Interfaces.IMapper;
using Server.Core.Interfaces.IRepositories;
using Server.Core.Interfaces.IServices;

namespace Server.Core.Services
{
    public class BaseService<T, TDTO> : IBaseService<TDTO>
        where T : class
        where TDTO : class
    {
        private readonly IBaseMapper<T, TDTO> _viewModelMapper;
        private readonly IBaseRepository<T> _repository;

        public BaseService(
            IBaseMapper<T, TDTO> viewModelMapper,
            IBaseRepository<T> repository)
        {
            _viewModelMapper = viewModelMapper;
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TDTO>> GetAll(CancellationToken cancellationToken)
        {
            return _viewModelMapper.MapList(await _repository.GetAll(cancellationToken));
        }

        public virtual async Task<PaginatedDataDTO<TDTO>> GetPaginatedData(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var paginatedData = await _repository.GetPaginatedData(pageNumber, pageSize, cancellationToken);
            var mappedData = _viewModelMapper.MapList(paginatedData.Data);
            var paginatedDataDTO = new PaginatedDataDTO<TDTO>(mappedData.ToList(), paginatedData.TotalCount);
            return paginatedDataDTO;
        }

        public virtual async Task<PaginatedDataDTO<TDTO>> GetPaginatedData(int pageNumber, int pageSize, List<ExpressionFilter> filters, CancellationToken cancellationToken)
        {
            var paginatedData = await _repository.GetPaginatedData(pageNumber, pageSize, filters, cancellationToken);
            var mappedData = _viewModelMapper.MapList(paginatedData.Data);
            var paginatedDataDTO = new PaginatedDataDTO<TDTO>(mappedData.ToList(), paginatedData.TotalCount);
            return paginatedDataDTO;
        }

        public virtual async Task<PaginatedDataDTO<TDTO>> GetPaginatedData(int pageNumber, int pageSize, List<ExpressionFilter> filters, string sortBy, string sortOrder, CancellationToken cancellationToken)
        {
            var paginatedData = await _repository.GetPaginatedData(pageNumber, pageSize, filters, sortBy, sortOrder, cancellationToken);
            var mappedData = _viewModelMapper.MapList(paginatedData.Data);
            var paginatedDataDTO = new PaginatedDataDTO<TDTO>(mappedData.ToList(), paginatedData.TotalCount);
            return paginatedDataDTO;
        }

        public virtual async Task<TDTO> GetById<Tid>(Tid id, CancellationToken cancellationToken)
        {
            return _viewModelMapper.MapModel(await _repository.GetById(id, cancellationToken));
        }

        public virtual async Task<bool> IsExists<Tvalue>(string key, Tvalue value, CancellationToken cancellationToken)
        {
            return await _repository.IsExists(key, value?.ToString(), cancellationToken);
        }

        public virtual async Task<bool> IsExistsForUpdate<Tid>(Tid id, string key, string value, CancellationToken cancellationToken)
        {
            return await _repository.IsExistsForUpdate(id, key, value, cancellationToken);
        }

    }

}
