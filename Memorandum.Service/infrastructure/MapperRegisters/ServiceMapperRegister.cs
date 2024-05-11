using Mapster;
using Memorandum.Repository.Models.ParamaterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Repository.infrastructure.MapperRegisters
{
    public  class ServiceMapperRegister: IRegister
    {
        public void Register(TypeAdapterConfig config) 
        {
            config.NewConfig<RegisterMemberParameterModel, RegisterMemberParameterModel>();
        }
    }
}
