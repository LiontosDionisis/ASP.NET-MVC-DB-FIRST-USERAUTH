using TeachersMVC.Data;
using TeachersMVC.DTO;

namespace TeachersMVC.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TeachersMvcdbContext context) : base(context) { }

        public Task<User?> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SignUpUserAsync(UserSignupDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(int userId, UserPatchDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
