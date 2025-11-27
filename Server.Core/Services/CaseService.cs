using Microsoft.Extensions.Logging;
using Server.Core.Entities.DTO;
using Server.Core.Entities.Records;
using Server.Core.Interfaces.IMapper;
using Server.Core.Interfaces.IRepositories;
using Server.Core.Interfaces.IServices;

namespace Server.Core.Services
{
    public class CaseService : BaseService<Case, CaseDTO>, ICaseService
    {
        private readonly IBaseMapper<Case, CaseDTO> _caseDTOMapper;
        private readonly IBaseMapper<CaseCreateDTO, Case> _caseCreateMapper;
        //private readonly IBaseMapper<CaseUpdateDTO, Case> _caseUpdateMapper;
        private readonly ICaseRepository _caseRepository;
        //private readonly IUserContext _userContext;
        private readonly IScopingService _scopingService;

        private readonly ILogger<ScopingService> _logger;

        public CaseService(
            IBaseMapper<Case, CaseDTO> caseDTOMapper,
            IBaseMapper<CaseCreateDTO, Case> caseCreateMapper,
            //IBaseMapper<CaseUpdateDTO, Case> caseUpdateMapper,
            ICaseRepository caseRepository,
            //IUserContext userContext
            IScopingService scopingService,
             ILogger<ScopingService> logger
            )
            //: base(caseDTOMapper, caseRepository)
            : base(caseDTOMapper, caseRepository)
        {
            _caseCreateMapper = caseCreateMapper;
            //_caseUpdateMapper = caseUpdateMapper;
            _caseDTOMapper = caseDTOMapper;
            _caseRepository = caseRepository;
            //_userContext = userContext;
            _scopingService = scopingService;
            _logger = logger;
        }



        public async Task<CaseDTO> Create(CaseCreateDTO model, CancellationToken cancellationToken)
        {
            //model.Resolutions.Add(new CaseResolutionCreateDTO
            //{
            //    Content = "{\"test222\":\"value\"}",
            //});
            // Mapping through AutoMapper
            var entity = _caseCreateMapper.MapModel(model);

            // OpenAI
            //string result = await _scopingService.Scope("What's the weather today?");
            //_logger.LogInformation("AI Result :", result);

            return _caseDTOMapper.MapModel(await _caseRepository.Create(entity, cancellationToken));
        }

        public async Task Update(CaseUpdateDTO model, CancellationToken cancellationToken)
        {
            //var existingData = await _caseRepository.GetById(model.Id, cancellationToken);

            //// Mapping through AutoMapper
            //_caseUpdateMapper.MapModel(model, existingData);

            //// Use object initializer to set init-only properties
            //existingData = existingData with
            //{
            //    UpdatedAt = DateTime.UtcNow,
            //    UpdatedBy = "System"
            //    //UpdatedBy = _userContext.UserId.ToString()
            //};

            //await _caseRepository.Update(existingData, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var entity = await _caseRepository.GetById(id, cancellationToken);
            await _caseRepository.Delete(entity, cancellationToken);
        }

        //public async Task<double> PriceCheck(int caseId, CancellationToken cancellationToken)
        //{
        //    return await _caseRepository.PriceCheck(caseId, cancellationToken);
        //}

    }
}
