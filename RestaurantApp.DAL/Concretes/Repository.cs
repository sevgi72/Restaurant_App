using Microsoft.EntityFrameworkCore;
using RestaurantApp.DAL.Data;
using RestaurantApp.DAL.Interfaces;
using RestaurantApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestaurantApp.DAL.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly RestaurantAppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(RestaurantAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // elave: CRUD metodları
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<T> GetAll(
            bool isTracking = false,
            int page = 1,
            int take = 10,
            params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (!isTracking)
                query = query.AsNoTracking();

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return query.Skip((page - 1) * take).Take(take);
        }

        public async Task<List<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // date araligi ucun order servicede istifade edecik elave metod
        public async Task<List<T>> GetByDateIntervalAsync(Expression<Func<T, DateTime>> dateSelector, DateTime start, DateTime end)
        {
            return await _dbSet.Where(e => dateSelector.Compile()(e) >= start && dateSelector.Compile()(e) <= end)
                               .ToListAsync();
        }

        // price araligi ucun menuitemde istifade edecik elave metood
        public async Task<List<T>> GetByPriceIntervalAsync(Expression<Func<T, decimal>> priceSelector, decimal min, decimal max)
        {
            return await _dbSet.Where(e => priceSelector.Compile()(e) >= min && priceSelector.Compile()(e) <= max)
                               .ToListAsync();
        }

       
    }
}
