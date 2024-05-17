using TeachersMVC.Data;

namespace TeachersMVC.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TeachersMvcdbContext _context;
        public IUserRepository UserRepository => new UserRepository(_context);
        //public ITeacherRepository TeacherRepository => new TeacherRepository(_context);

        public UnitOfWork(TeachersMvcdbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        
    }
}
