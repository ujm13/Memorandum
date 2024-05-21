using Mapster;
using Memorandum.Service.Models.ParamaterModelDto;
using Memorandum.Service.Models.ParameterDto;
using Memorandum.Service.Models.ResultModelDto;
using Memorandum.WebApplication.Models.Parameters;
using Memorandum.WebApplication.Models.ViewModels;

namespace Memorandum.WebApplication.infrastructure.MapperRegisters
{
    public class WebApplicationMapperRegister: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterMemberParameter, RegisterMemberParameterDto>();
            config.NewConfig<LoginMemberParameter, LoginMemberParameterDto>();
            config.NewConfig<CreateMemorandumParameter, CreateMemorandumParameterDto>()
                .Map(a => a.CreateTime, b => DateTime.Now)
                .Map(a => a.UpdateTime, b => DateTime.Now)
                .Map(a=>a.Id,b=>Guid.NewGuid());
            config.NewConfig<UpdateMemorandumParameter, UpdateMemorandumParameterDto>()
                .Map(a => a.UpdateTime, b => DateTime.Now);
            config.NewConfig<MemorandumResultModelDto, MemorandumResultViewModel>();
            




        }
    }
}
