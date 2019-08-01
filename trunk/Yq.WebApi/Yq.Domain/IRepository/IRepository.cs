using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Yq.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace Yq.Domain.IRepository
{
    /// <summary>
    /// 仓储接口定义
    /// </summary>
    public interface IRepository
    {

    }
    /// <summary>
    /// 定义泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public interface  IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : Entity<TPrimaryKey>
    {
        #region  同步
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        TEntity Get(TPrimaryKey id);

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        void Insert(TEntity entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name = "entity" > 实体 </ param >
        void Update(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体主键</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        void Delete(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面条目</param>
        /// <param name="rowCount">数据总数</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> order);

        bool Save();

        int ExecuteSql(string sql, SqlParameter parameter = null);
        IEnumerable<T> SqlQuery<T>(string sql, object param = null);

        GridReader QueryMultiple(string sql, object param = null);
        #endregion

        #region 异步
        // 根据lambda表达式条件获取实体集合
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> sp);

        //根据lambda表达式条件获取实体集合
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> sp);

        // 事务性保存
        Task<bool> SaveAsync();

        // 新增实体
        Task<int> InsertAsync(TEntity entity);

        // dapper 查询
        Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, object param = null);

        Task<GridReader> QueryMultipleAsync(string sql, object param = null);
        #endregion


    }

    /// <summary>
    /// 默认Guid主键类型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : Entity
    {

    }

}
