using Dapper;
using Memorandum.Common.Options;
using Memorandum.Repository.Interfaces;
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
    }
}
