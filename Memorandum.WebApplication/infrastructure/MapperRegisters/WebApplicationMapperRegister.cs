using Mapster;
using Memorandum.Service.Models.ParamaterModelDto;
using Memorandum.WebApplication.Models.Parameters;

namespace Memorandum.WebApplication.infrastructure.MapperRegisters
{
    public class WebApplicationMapperRegister: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterMemberParameter, RegisterMemberParameterDto>();
        }
    }
}
