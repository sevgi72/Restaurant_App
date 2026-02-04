using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantApp.DAL.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        // CRUD metodlari ucun
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task RemoveAsync(T entity);
        Task SaveChangesAsync();

        // Queryler ucun
        IQueryable<T> GetAll(bool isTracking = false, int page = 1, int take = 10, params string[] includes);
        Task<List<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);

        // Check existence
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        // Special queries (Order / MenuItem üçün)
        Task<List<T>> GetByDateIntervalAsync(Expression<Func<T, DateTime>> dateSelector, DateTime start, DateTime end);
        Task<List<T>> GetByPriceIntervalAsync(Expression<Func<T, decimal>> priceSelector, decimal min, decimal max);
    }
}
