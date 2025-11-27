
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Core.Entities.DTO;
using Server.Core.Entities.Records;
using Server.Core.Interfaces.IRepositories;
using Server.Infrastructure.Data;

namespace Server.Infrastructure.Repositories
{
    public class CaseRepository : BaseRepository<Case>, ICaseRepository
    {
        private readonly IMapper _autoMapper; // AutoMapper 주입
        public CaseRepository(ApplicationDbContext dbContext, IMapper autoMapper) : base(dbContext)
        {
            _autoMapper = autoMapper;
        }

        public async Task<Case> CreateCase(CaseCreateDTO model, CancellationToken cancellationToken)
        {
            // 트랜잭션 시작: Case와 CaseResolution 저장이 원자적으로 이루어지도록 합니다.
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // 1. Case 엔티티 생성 및 기본 필드 설정
                var caseEntity = _autoMapper.Map<Case>(model); // DTO의 기본 Case 필드 매핑

                caseEntity = caseEntity with
                {
                    //CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    //UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    //Statements = new List<CaseStatement>(), // Statements 컬렉션 초기화
                    //Resolutions = new List<CaseResolution>() // Resolutions 컬렉션 초기화
                };

                //// 2. DbContext에 Case 엔티티 추가 (아직 SaveChanges 호출 안 함)
                //_dbContext.Cases.Add(caseEntity);

                //// 3. DTO에 포함된 Resolutions를 CaseResolution 엔티티로 변환하고 연결합니다.
                //if (model.Resolutions != null && model.Resolutions.Any())
                //{
                //    foreach (var resolutionDto in model.Resolutions)
                //    {
                //        var resolutionEntity = _autoMapper.Map<CaseResolution>(resolutionDto);

                //        // CaseId를 부모 Case의 Id로 설정합니다. (매우 중요!)
                //        resolutionEntity = resolutionEntity with
                //        {
                //            CaseId = caseEntity.Id, // 새롭게 생성된 Case의 Id를 참조
                //            //CreatedAt = DateTime.UtcNow,
                //            CreatedBy = "System",
                //            //UpdatedAt = DateTime.UtcNow,
                //            UpdatedBy = "System"
                //        };

                //        // Case 엔티티의 Resolutions 컬렉션에 추가합니다.
                //        // EF Core는 이 관계를 통해 자식 엔티티도 함께 추적하고 저장합니다.
                //        caseEntity.Resolutions.Add(resolutionEntity);

                //        // 명시적으로 DbContext에 CaseResolution 엔티티를 추가할 수도 있습니다.
                //        // _dbContext.CaseResolutions.Add(resolutionEntity);
                //    }
                //}

                //if (model.Statements != null && model.Statements.Any())
                //{
                //    foreach (var statementDTO in model.Statements)
                //    {
                //        var statementEntity = _autoMapper.Map<CaseStatement>(statementDTO);

                //        // CaseId를 부모 Case의 Id로 설정합니다. (매우 중요!)
                //        statementEntity = statementEntity with
                //        {
                //            CaseId = caseEntity.Id, // 새롭게 생성된 Case의 Id를 참조
                //            //CreatedAt = DateTime.UtcNow,
                //            CreatedBy = "System",
                //            //UpdatedAt = DateTime.UtcNow,
                //            UpdatedBy = "System"
                //        };

                //        // Case 엔티티의 Resolutions 컬렉션에 추가합니다.
                //        // EF Core는 이 관계를 통해 자식 엔티티도 함께 추적하고 저장합니다.
                //        caseEntity.Statements.Add(statementEntity);

                //        // 명시적으로 DbContext에 CaseResolution 엔티티를 추가할 수도 있습니다.
                //        // _dbContext.CaseResolutions.Add(resolutionEntity);
                //    }
                //}

                //if (model.Statements != null && model.Statements.Any())
                //{
                //    foreach (var statementDTO in model.Statements)
                //    {
                //        var statementEntity = _autoMapper.Map<CaseStatement>(statementDTO);

                //        // CaseId를 부모 Case의 Id로 설정합니다. (매우 중요!)
                //        statementEntity = statementEntity with
                //        {
                //            CaseId = caseEntity.Id, // 새롭게 생성된 Case의 Id를 참조
                //            //CreatedAt = DateTime.UtcNow,
                //            CreatedBy = "System",
                //            //UpdatedAt = DateTime.UtcNow,
                //            UpdatedBy = "System"
                //        };

                //        // Case 엔티티의 Resolutions 컬렉션에 추가합니다.
                //        // EF Core는 이 관계를 통해 자식 엔티티도 함께 추적하고 저장합니다.
                //        caseEntity.Statements.Add(statementEntity);

                //        // 명시적으로 DbContext에 CaseResolution 엔티티를 추가할 수도 있습니다.
                //        // _dbContext.CaseResolutions.Add(resolutionEntity);
                //    }
                //}

                // 4. 모든 변경 사항을 한 번에 데이터베이스에 저장합니다.
                // EF Core는 관계를 고려하여 Case를 먼저 삽입한 후 CaseResolution을 삽입합니다.
                await _dbContext.SaveChangesAsync(cancellationToken);

                // 5. 트랜잭션 커밋
                await transaction.CommitAsync(cancellationToken);

                return caseEntity; // 생성된 Case 엔티티 (Resolutions 포함) 반환
            }
            catch (Exception ex)
            {
                // 오류 발생 시 트랜잭션 롤백
                await transaction.RollbackAsync(cancellationToken);
                // 로깅 등 추가 오류 처리
                throw new Exception("Error creating Case and Resolutions within repository.", ex); // 자세한 예외를 포함하여 다시 던집니다.
            }
        }

    }
}
