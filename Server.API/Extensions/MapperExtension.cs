using AutoMapper;
using Server.Core.Entities.DTO;
using Server.Core.Entities.Records;
using Server.Core.Interfaces.IMapper;
using Server.Core.Mapper;


namespace Server.API.Extensions
{
    public static class MapperExtension
    {
        public static IServiceCollection RegisterMapperService(this IServiceCollection services)
        {
            #region Mapper

            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Case, CaseDTO>();
                cfg.CreateMap<CaseCreateDTO, Case>();
                cfg.CreateMap<CaseUpdateDTO, Case>();

                cfg.CreateMap<CaseAttachment, CaseAttachmentDTO>();
                cfg.CreateMap<CaseAttachmentCreateDTO, CaseAttachment>();
                cfg.CreateMap<CaseAttachmentUpdateDTO, CaseAttachment>();

                cfg.CreateMap<CaseResolution, CaseResolutionDTO>();
                cfg.CreateMap<CaseResolutionCreateDTO, CaseResolution>();
                cfg.CreateMap<CaseResolutionUpdateDTO, CaseResolution>();

                cfg.CreateMap<CaseStatement, CaseStatementDTO>();
                cfg.CreateMap<CaseStatementCreateDTO, CaseStatement>();
                cfg.CreateMap<CaseStatementUpdateDTO, CaseStatement>();
            }).CreateMapper());

            // Register the IMapperService implementation with your dependency injection container
            services.AddSingleton<IBaseMapper<Case, CaseDTO>, BaseMapper<Case, CaseDTO>>();
            services.AddSingleton<IBaseMapper<CaseCreateDTO, Case>, BaseMapper<CaseCreateDTO, Case>>();
            services.AddSingleton<IBaseMapper<CaseUpdateDTO, Case>, BaseMapper<CaseUpdateDTO, Case>>();

            services.AddSingleton<IBaseMapper<CaseAttachment, CaseAttachmentDTO>, BaseMapper<CaseAttachment, CaseAttachmentDTO>>();
            services.AddSingleton<IBaseMapper<CaseAttachmentCreateDTO, CaseAttachment>, BaseMapper<CaseAttachmentCreateDTO, CaseAttachment>>();
            services.AddSingleton<IBaseMapper<CaseAttachmentUpdateDTO, CaseAttachment>, BaseMapper<CaseAttachmentUpdateDTO, CaseAttachment>>();

            services.AddSingleton<IBaseMapper<CaseResolution, CaseResolutionDTO>, BaseMapper<CaseResolution, CaseResolutionDTO>>();
            services.AddSingleton<IBaseMapper<CaseResolutionCreateDTO, CaseResolution>, BaseMapper<CaseResolutionCreateDTO, CaseResolution>>();
            services.AddSingleton<IBaseMapper<CaseResolutionUpdateDTO, CaseResolution>, BaseMapper<CaseResolutionUpdateDTO, CaseResolution>>();

            services.AddSingleton<IBaseMapper<CaseStatement, CaseStatementDTO>, BaseMapper<CaseStatement, CaseStatementDTO>>();
            services.AddSingleton<IBaseMapper<CaseStatementCreateDTO, CaseStatement>, BaseMapper<CaseStatementCreateDTO, CaseStatement>>();
            services.AddSingleton<IBaseMapper<CaseStatementUpdateDTO, CaseStatement>, BaseMapper<CaseStatementUpdateDTO, CaseStatement>>();


            #endregion

            return services;
        }
    }
}
