using Memorandum.Service.Models.ParameterDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Service.Interfaces
{
    public interface IMemorandumService
    {
        /// <summary>
        /// 建立代辦事項
        /// </summary>
        /// <returns></returns>
        Task<bool> CreateAsync(CreateMemorandumParameterDto parameterDto);
    }
}
