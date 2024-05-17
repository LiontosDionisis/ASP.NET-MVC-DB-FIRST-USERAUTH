
using Microsoft.EntityFrameworkCore;
using TeachersMVC.Data;

namespace TeachersMVC.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {

        private readonly TeachersMvcdbContext _context;
        private readonly DbSet<T> _table;
        //private readonly DbSet<T> _dbSet;

        public BaseRepository(TeachersMvcdbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }
        public virtual async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? existing = await _table.FindAsync(id);
            if (existing is not null)
            {
                _table.Remove(existing);
                return true;
            }
            return false;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _table.ToListAsync();
            return entities;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            var entity = await _table.FindAsync(id);
            return entity!;
        }
        
        public virtual async Task<int> GetCountAsync()
        {
            var count = await _table.CountAsync();
            return count;
        }

        public virtual void UpdateAsync(T entity)
        {
            _table.Attach(entity);
            _table.Entry(entity).State = EntityState.Modified;
        }
    }
}
