using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelListing.Data;
using HotelListing.Service.interfaces;
using Microsoft.EntityFrameworkCore;


namespace HotelListing.Service
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
                query = query.Where(expression);


            if (includes != null)
                foreach (var dbSet in includes)
                    query.Include(dbSet);

            if (orderBy != null)
                query = orderBy(query);

            return await query.AsNoTracking().ToListAsync();

        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
                foreach (var dbSet in includes)
                {
                    query.Include(dbSet);
                }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);

        }

        public async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);

        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }
    }
}
