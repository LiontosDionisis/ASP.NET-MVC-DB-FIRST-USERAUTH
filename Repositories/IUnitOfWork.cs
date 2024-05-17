namespace TeachersMVC.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository {get;}
        //public ITeacherRepository TeacherRepository { get;}

        Task<bool> SaveAsync();
    }
}
