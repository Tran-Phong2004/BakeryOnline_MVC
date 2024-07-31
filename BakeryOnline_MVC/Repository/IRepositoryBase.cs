using BakeryOnline_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace BakeryOnline_MVC.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void AddEntity(TEntity entity);
        void UpdateEntity(TEntity entity);
        void DeleteEntity(TEntity entity);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> Sort(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IQueryable<TEntity> Paging(int Page, int PageSize,
                                          Expression<Func<TEntity, bool>> filter,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        Task<Paging<TEntity>> Paging(IQueryable<TEntity> Collection, int Page, int PageSize,
                                   Expression<Func<TEntity, bool>> filter,
                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
    }  
}
