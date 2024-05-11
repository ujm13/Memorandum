using Memorandum.Repository.Models.DataModels;
using Memorandum.Repository.Models.ParamaterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Repository.Interfaces
{
    public interface IMemberRepository
    {
        /// <summary>
        /// 用戶註冊 將資料存入DB
        /// </summary>
        /// <param name="registerMemberParameter"></param>
        /// <returns></returns>
        Task<bool> InsterAsync(RegisterMemberParameter registerMemberParameter);

        /// <summary>
        /// 會員登入查詢
        /// </summary>
        /// <param name="loginMemberParameter"></param>
        /// <returns></returns>
        Task<LoginMemberDataModel> GetAsync(LoginMemberParameter loginMemberParameter);



    }
}
