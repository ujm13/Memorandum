using Dapper;
using Mapster.Models;
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
            var id=Guid.NewGuid();
            var cheakId = await CheakIdAsync(id);
            while (cheakId != 0) 
            {
                id = Guid.NewGuid();
                cheakId = await CheakIdAsync(id);
            }
            parameterModel.Id = id;
            await using var conn = new SqlConnection(_dbConnectionOptions.Member);
            var result=await conn.ExecuteAsync(sql, parameterModel);
            return result>0;

        }


        /// <summary>
        /// 查詢Id是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<int> CheakIdAsync(Guid id) 
        {
            var sql = @"Select count(*)
                       From Memorandum
                       Where Id=@id";
            await using var conn = new SqlConnection(_dbConnectionOptions.Member);
            var result = await conn.ExecuteScalarAsync<int>(sql, id);
            return result;
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
             await using var conn = new SqlConnection(_dbConnectionOptions.Member);
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

            await using var conn = new SqlConnection(_dbConnectionOptions.Member);
            var parameter = new DynamicParameters();
            parameter.Add("Id", id);
            var result = await conn.QueryFirstOrDefault(sql, parameter);
            return result;
        }

        /// <summary>
        /// 取得所有資料
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MemorandumDataModel>> GetAllAsync()
        {
            var sql = @"SELECT  Id,
                                Title,
                                Description,
                                DueDate,
                                Status,
                                Priority,
                                CreateTime,
                                UpdateTime
                          FROM  Memorandum";

            await using var conn = new SqlConnection(_dbConnectionOptions.Member);
            var result = await conn.QueryAsync<MemorandumDataModel>(sql);
            return result;
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var sql = @"Delete  
                FROM Memorandum
                WHERE Id = @Id
                ";

            await using var conn = new SqlConnection(_dbConnectionOptions.Member);
            var parameter = new DynamicParameters();
            parameter.Add("Id", id);
            var result = await conn.ExecuteAsync(sql, parameter);
            return result > 0;
        }

    }
}
