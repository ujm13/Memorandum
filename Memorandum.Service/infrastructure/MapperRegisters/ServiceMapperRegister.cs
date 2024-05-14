using Mapster;
using Memorandum.Repository.Models.DataModels;
using Memorandum.Repository.Models.ParamaterModels;
using Memorandum.Service.Models.ParameterDto;
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
            config.NewConfig<LoginMemberParameterDto, LoginMemberParameterModel>();
            config.NewConfig<CreateMemorandumParameterDto, InsertMemorandumParameterModel>();
        }
    }
}
