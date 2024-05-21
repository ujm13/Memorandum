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
    public class MemorandumRepository : IMemorandumRepository
    {
        private readonly DbConnectionOptions _dbConnectionOptions;

        public MemorandumRepository(IOptions<DbConnectionOptions> dbConnectionOptions)
        {
            _dbConnectionOptions = dbConnectionOptions.Value;
        }

        /// <summary>
        /// 新增代辦事項
        /// </summary>
        /// <param name="parameterModel"></param>
        /// <returns></returns>        
        public async Task<bool> InsertAsync(InsertMemorandumParameterModel parameterModel)
        {
            var sql = @"insert into Memorandum
                    Id,
                    Title,
                    Description,
                    DueDate,
                    Status,
                    Priority,
                    CreateTime,
                    UpdateTime
            VALUES (
                    @Id,
                    @Title,
                    @Description,
                    @DueDate,
                    @Status,
                    @Priority,
                    @CreateTime,
                    @UpdateTime 
                    ) ";
            using var conn = new SqlConnection(_dbConnectionOptions.Member);
            var result=await conn.ExecuteAsync(sql, parameterModel);
            return result>0;

        }

        /// <summary>
        /// 修改代辦事項
        /// </summary>
        /// <param name="parameterModel">The parameter model.</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(UpdateMemorandumParameterModel parameterModel)
        {
            var sql = @"UPDATE Memorandum
                SET 
                    Title = @Title,
                    Description = @Description,
                    DueDate = @DueDate,
                    Status = @Status,
                    Priority = @Priority,
                    UpdateTime = @UpdateTime
                WHERE 
                    Id = @Id";
            using var conn = new SqlConnection(_dbConnectionOptions.Member);
            var result = await conn.ExecuteAsync(sql, parameterModel);
            return result > 0;
        }

        /// <summary>
        /// 取得明細
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MemorandumDataModel> GetDetailAsync(Guid id)
        {
            var sql = @"
                SELECT Id,
                       Title,
                       Description,
                       DueDate,
                       Status,
                       Priority,
                       CreateTime,
                       UpdateTime
                FROM Memorandum
                WHERE Id = @Id
                ";

            using var conn = new SqlConnection(_dbConnectionOptions.Member);
            var parameter = new DynamicParameters();
            parameter.Add("Id", id);
            var result = await conn.QueryFirstOrDefault(sql, parameter);
            return result;
        }

    }
}
