using Demo.BuniessLogicLayer.Interfaces;
using Demo.DataAcessLayer.Context;
using Demo.DataAcessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BuniessLogicLayer.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly MVCAppDbContext _dbContext;

        public GenericRepo(MVCAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T item)
        {
            await _dbContext.AddAsync(item);
        }

        public  void Delete(T item)
        {
            _dbContext.Remove(item);
        }

        public async Task<T> GetAsync(int id)

        => await _dbContext.Set<T>().FindAsync(id);


        public async Task<IEnumerable<T>> GetAllAsync()
        {

            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>) await _dbContext.Employees.Include(E => E.Department).ToListAsync();

            return await _dbContext.Set<T>().ToListAsync();
        }

        public void Update(T item)
        {
            _dbContext.Update(item);
        }
    }
}