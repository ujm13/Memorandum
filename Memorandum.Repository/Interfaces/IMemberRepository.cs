using Memorandum.Repository.Models.DataModels;
using Memorandum.Repository.Models.ParamaterModels;

namespace Memorandum.Repository.Interfaces
{
    public interface IMemberRepository
    {
        /// <summary>
        /// 用戶註冊 將資料存入DB
        /// </summary>
        /// <param name="registerMemberParameter"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(RegisterMemberParameterModel registerMemberParameter);

        /// <summary>
        /// 會員登入查詢
        /// </summary>
        /// <param name="loginMemberParameter"></param>
        /// <returns></returns>
        Task<LoginMemberDataModel> GetAsync(LoginMemberParameterModel loginMemberParameter);

        /// <summary>
        /// 查詢Id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistIdAsync(Guid id);

    }
}
