using Dapper;
using Memorandum.Common.Options;
using Memorandum.Repository.Interfaces;
using Memorandum.Repository.Models.DataModels;
using Memorandum.Repository.Models.ParamaterModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorandum.Repository.Implement
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DbConnectionOptions _dbConnectionOptions;

        public MemberRepository(IOptions<DbConnectionOptions> dbConnectionOptions)
        {
            _dbConnectionOptions = dbConnectionOptions.Value;
        }



        /// <summary>
        /// 用戶註冊 將資料存入DB
        /// </summary>
        /// <param name="registerMemberParameter"></param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(RegisterMemberParameterModel registerMemberParameter)
        {
            var sql = @"insert into  Members (
                    Id,
                    UserName ,
                    Account ,
                    Password ,
                    Email,
                    Phone ,
                    Birthday 
                    )  
            VALUES (
                    @Id,
                    @UserName,
                    @Account,
                    @Password,
                    @Email,
                    @Phone ,
                    @Birthday 
                    ) 
            ";
            await using var conn = new SqlConnection(_dbConnectionOptions.Member);
            var result = await conn.ExecuteAsync(sql, registerMemberParameter);
            return result > 0;
        }

        /// <summary>
        /// 會員登入查詢
        /// </summary>
        /// <param name="loginMemberParameter"></param>
        /// <returns></returns>
        public async Task<LoginMemberDataModel> GetAsync(LoginMemberParameterModel loginMemberParameter)
        {
            var sql = @"Select Account , Password , UserName ,Email 
                        From Members
                        Where Account=@Account";

            await using var conn= new SqlConnection(_dbConnectionOptions.Member);
            var result=await conn.QueryFirstOrDefaultAsync<LoginMemberDataModel>(sql, loginMemberParameter);
            return result;
        }
    }


}
