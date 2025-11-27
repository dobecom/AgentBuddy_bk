using Server.Core.Entities.DTO;

namespace Server.Core.Interfaces.IServices
{
    public interface ICaseService : IBaseService<CaseDTO>
    {
        Task<CaseDTO> Create(CaseCreateDTO model, CancellationToken cancellationToken);
        Task Update(CaseUpdateDTO model, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
    }
}
