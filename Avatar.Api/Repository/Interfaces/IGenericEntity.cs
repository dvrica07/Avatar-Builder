using Avatar.Api.Repository.Entities;
using Avatar.Framework.Common;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace Avatar.Api.Repository.Interfaces;

public interface IGenericEntity<TTarget> where TTarget : BaseEntity
{
    Task<AppResult<TTarget>> GetByIdAsync(int id);
    Task<AppResult<IEnumerable<TTarget>>> GetAllAsync();
    // take and skip for pagination
    Task<AppResult<IEnumerable<TTarget>>> FindAsync(Expression<Func<TTarget, bool>> expression,
        int? take = 100, int? skip = 0, IEnumerable<Expression<Func<TTarget, object>>>? includes = null,
        Expression<Func<TTarget, object>>? orderBy = null, Expression<Func<TTarget, object>>? orderByDesc = null);
    Task<AppResult<TTarget>> FindFirstAsync(Expression<Func<TTarget, bool>> expression,
        IEnumerable<Expression<Func<TTarget, object>>>? includes = null);
    Task<AppResult<TTarget>> Add(TTarget entity);
    Task<AppResult<IEnumerable<TTarget>>> AddRange(IEnumerable<TTarget> entities);
    Task<AppResult<TTarget>> Remove(TTarget entity);
    Task<AppResult<IEnumerable<TTarget>>> RemoveRange(IEnumerable<TTarget> entities);
    Task<AppResult<TTarget>> Update(TTarget entity);
    Task<AppResult<IEnumerable<TTarget>>> UpdateRange(IEnumerable<TTarget> entities);
    Task<AppResult<IEnumerable<TTarget>>> ExecuteStoredProcAsync(string storedProcName, params SqlParameter[] parameters);
    Task<AppResult<IEnumerable<TTarget>>> QueryViewAsync();
    Task<AppResult<IEnumerable<TResult>>> CallFunctionAsync<TResult>(string sql, params object[] parameters) where TResult : class;
}
