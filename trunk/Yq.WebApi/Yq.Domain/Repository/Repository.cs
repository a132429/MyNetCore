using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Yq.EntityFrameworkCore;
using Yq.Domain.IRepository;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using static Dapper.SqlMapper;

namespace Yq.Domain.Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public  class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        //定义数据访问上下文对象
        protected readonly WdDbContext _dbContext;
        protected readonly DapperContext _dapper;
        /// <summary>
        /// 通过构造函数注入得到数据上下文对象实例
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dapper"></param>
        public Repository(WdDbContext dbContext, DapperContext dapper)
        {
            _dbContext = dbContext;
            _dapper = dapper;
        }
        #region 同步
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAllList()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public TEntity Get(TPrimaryKey id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
        private void EntityToEntity<T>(T pTargetObjSrc, T pTargetObjDest)
        {
            foreach (var mItem in typeof(T).GetProperties())
            {
                mItem.SetValue(pTargetObjDest, mItem.GetValue(pTargetObjSrc, new object[] { }), null);
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体主键</param>
        public void Delete(TPrimaryKey id)
        {
            _dbContext.Set<TEntity>().Remove(Get(id));
        }

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            _dbContext.Set<TEntity>().Where(where).ToList().ForEach(it => _dbContext.Set<TEntity>().Remove(it));
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage">页码</param>
        /// <param name="pageSize">单页数据数</param>
        /// <param name="rowCount">行数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null)
        {
            var result = from p in _dbContext.Set<TEntity>()
                         select p;
            if (where != null)
                result = result.Where(where);
            if (order != null)
                result = result.OrderBy(order);
            else
                result = result.OrderBy(m => m.Id);
            rowCount = result.Count();
            return result.Skip((startPage - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// 事务性保存
        /// </summary>
        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据主键构建判断表达式
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
        /// <summary>
        /// sql脚本
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql, SqlParameter parameter = null)
        {
            if (parameter == null)
                return _dbContext.Database.ExecuteSqlCommand(sql);
            else
                return _dbContext.Database.ExecuteSqlCommand(sql, parameter);
        }
        public IEnumerable<T> SqlQuery<T>(string sql, object param = null)
        {
            using (var conn = _dapper.GetConnection)
            {
                if (param == null)
                    return conn.Query<T>(sql);
                else
                    return conn.Query<T>(sql, param);
            }

        }
        public GridReader QueryMultiple(string sql, object param = null)
        {
            var conn = _dapper.GetConnection;
            if (param == null)
                    return conn.QueryMultiple(sql);
             else
              return conn.QueryMultiple(sql, param);
        }
        #endregion
        #region 异步
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
             await _dbContext.Set<TEntity>().AddAsync(entity);
            return 0;
        }
        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> sp)
        {
            return await _dbContext.Set<TEntity>().Where(sp).ToListAsync();
        }
        /// <summary>
        /// 查询第一条
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> sp)
        {
            return await _dbContext.Set<TEntity>().Where(sp).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 事务性保存
        /// </summary>
        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
       
        /// <summary>
        /// dapper 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, object param = null)
        {
            using (var conn = _dapper.GetConnection)
            {
                if (param == null)
                    return await conn.QueryAsync<T>(sql);
                else
                    return await conn.QueryAsync<T>(sql, param);
            }

        }

        public async Task<GridReader> QueryMultipleAsync(string sql, object param = null)
        {
            var conn = _dapper.GetConnection;
            if (param == null)
                return conn.QueryMultiple(sql);
            else
                return conn.QueryMultiple(sql, param);
        }
        #endregion

    }

    /// <summary>
    /// 主键为Guid类型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public  class Repository<TEntity> : Repository<TEntity, Guid> where TEntity : Entity
    {
        public Repository(WdDbContext dbContext, DapperContext dapper) : base(dbContext, dapper)
        {
        }
    }

}
