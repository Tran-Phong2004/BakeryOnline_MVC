using BakeryOnline_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;

namespace BakeryOnline_MVC.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        ApplicationDbContext appContext;
        public DbSet<TEntity> dbSet { get; }

        public RepositoryBase(ApplicationDbContext appContext) 
        {
            this.appContext = appContext;
            dbSet = this.appContext.Set<TEntity>();
        }
        public void AddEntity(TEntity entity)
        {
            appContext.Add(entity);
        }

        public void DeleteEntity(TEntity entity)
        {
            appContext.Remove(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
          appContext.Update(entity);
        }

        public async Task<TEntity> FindByIdAsyn<TKey>(TKey id)
        {
            return await dbSet.FindAsync(id);
        } 
        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public IQueryable<TEntity> Paging(int Page = 1, int PageSize = 4,
                                  Expression<Func<TEntity, bool>> filter = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            IQueryable<TEntity> query = dbSet;
            var page = Page;
            var pageSize = PageSize;

            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (include != null)
            {
                query = include(query);
            }

            query = query.Skip(pageSize * (page - 1))
                         .Take(pageSize);

            return query;
        }

        public async Task<Paging<TEntity>> Paging(IQueryable<TEntity> Collection, int Page, int PageSize,
                                         Expression<Func<TEntity, bool>> filter = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            var query = Collection;
            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }


            var TotalItem = await query.CountAsync();
            var TotalPage = (int)Math.Ceiling((double)TotalItem / PageSize);

            if(Page > TotalPage || Page < 1)
            {
                Page = 1;
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (include != null)
            {
                query = include(query);
            }

            var listProduct = await query.Skip(PageSize * (Page - 1))
                                         .Take(PageSize)
                                         .ToListAsync();

           return new Paging<TEntity>()
           {
               CurrentPage = Page,
               TotalPage = TotalPage,
               Result = listProduct
           };
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, 
                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            if(orderBy != null)
            {
                query = orderBy(query);
            }

            return query;  
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            return query;
        }

        public IQueryable<TEntity> Sort(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}
