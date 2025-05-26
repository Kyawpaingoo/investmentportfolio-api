using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class Repository <T> : IRepository<T> where T : class
{
    private readonly DbContext _dbContext;

    public Repository(DbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public IQueryable<T> GetAll()
    {
        try
        {
            return _dbContext.Set<T>().AsQueryable();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    public IQueryable<T> GetWithoutTracking()
    {
        return _dbContext.Set<T>().AsQueryable().AsNoTracking();
    }
    public IQueryable<T> Query(Expression<Func<T, bool>> filter, bool showDeleted = false)
    {
        return _dbContext.Set<T>().Where(filter).AsQueryable();
    }
    public int Insert(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        return _dbContext.SaveChanges();
    }

    public T InsertReturn(T entity)
    {
        T newEntity = _dbContext.Set<T>().Add(entity).Entity;
        var result = _dbContext.SaveChanges();
        if (result > 0)
        {
            return newEntity;
        }
        else
        {
            return null;
        }
    }

    public async Task<T> InsertReturnAsync(T entity)
    {
        try
        {
            T newEntity = _dbContext.Set<T>().Add(entity).Entity;
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return newEntity;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            return null;
        }

    }

    public int Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return _dbContext.SaveChanges();
    }

    public int Update(T entity)
    {
        return _dbContext.SaveChanges();
    }
    
     public T UpdateComplete(T OldEntity, T NewEntity)
     {
         try
         {
             _dbContext.Entry(OldEntity).State = EntityState.Detached;
             _dbContext.Set<T>().Attach(NewEntity);
             _dbContext.Entry(NewEntity).State = EntityState.Modified;

             var result = _dbContext.SaveChanges();
             if (result > 0)
             {
                 return NewEntity;
             }
             else
             {
                 return null;
             }
         }
         catch (Exception ex)
         {

         }
         return null;

     }

     public async Task<int> RemoveAsync(T entity)
     {
         _dbContext.Set<T>().Remove(entity);
         return await _dbContext.SaveChangesAsync();
     }

     public async Task<T> UpdateCompleteAsync(T OldEntity, T NewEntity)
     {
         _dbContext.Entry(OldEntity).State = EntityState.Detached;
         _dbContext.Set<T>().Attach(NewEntity);
         _dbContext.Entry(NewEntity).State = EntityState.Modified;

         var result = await _dbContext.SaveChangesAsync();
         if (result > 0)
         {
             return NewEntity;
         }
         else
         {
             return null;
         }
     }


     public async Task<T> UpdateAsync(T Entity)
     {
         try
         {
             _dbContext.Entry(Entity).State = EntityState.Detached;
             _dbContext.Set<T>().Attach(Entity);
             _dbContext.Entry(Entity).State = EntityState.Modified;

             var result = await _dbContext.SaveChangesAsync();
             if (result > 0)
             {
                 return Entity;
             }
             else
             {
                 return null;
             }
         }
         catch(Exception e)
         {
             return null;
         }
         
     }

     public async Task<T> UpdateNewAsync(T Entity)
     {
         _dbContext.Set<T>().Update(Entity);
         await _dbContext.SaveChangesAsync();
         return Entity;
     }


     public T GetById(int id)
     {
         return _dbContext.Set<T>().Find(id);
     }

     public T GetByCompositeKey(int id, string key)
     {

         return _dbContext.Set<T>().Find(id, key);

     }
     public int MaxNumber(Expression<Func<T, int>> expression)
     {
         return this._dbContext.Set<T>().Select(expression).Max();
     }


     public IQueryable<T> Take(int count)
     {
         return this._dbContext.Set<T>().Take(count).AsQueryable();
     }
     public IQueryable<T> Skip(int count)
     {
         return this._dbContext.Set<T>().Skip(count).AsQueryable();
     }
     public int Remove(T entity)
     {
         this._dbContext.Set<T>().Remove(entity);
         return this._dbContext.SaveChanges();
     }


     public int Count(Expression<Func<T, bool>> filter, Expression<Func<T, int>> field)
     {

         return _dbContext.Set<T>().Where(filter).Select(field).Count();
     }

     public void Dispose(bool disposing)
     {
         if (disposing)
         {
             _dbContext.Dispose();
         }
         this.Dispose(disposing);
     }

     public IQueryable<T> OrderBy(Expression<Func<T, string>> filter)
     {
         return _dbContext.Set<T>().OrderBy(filter).AsQueryable();
     }

     public async Task InsertListAsync(List<T> entity)
     {
         for (int i = 0; i < entity.Count(); i++)
         {
             _dbContext.Set<T>().Add(entity[i]);

         }
         await _dbContext.SaveChangesAsync();


     }


     public async Task updateListAsync(List<T> entities)
     {

         for (int i = 0; i < entities.Count(); i++)
         {
             _dbContext.Entry(entities[i]).State = EntityState.Modified;
         }
         await _dbContext.SaveChangesAsync();


     }
}