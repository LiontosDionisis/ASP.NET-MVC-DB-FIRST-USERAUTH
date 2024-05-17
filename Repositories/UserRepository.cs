using Microsoft.EntityFrameworkCore;
using TeachersMVC.Data;
using TeachersMVC.DTO;
using TeachersMVC.Security;

namespace TeachersMVC.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly TeachersMvcdbContext context;
        public UserRepository(TeachersMvcdbContext context) : base(context)
        {

        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await context.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == username || x.Email == username);
            if (user == null)
            {
                return null;
            }
            if (!EncryptionUtil.IsValidPassword(password, user.Password!))
            {
                return null;
            }
            return user;
        }

        public async Task<bool> SignUpUserAsync(UserSignupDTO request)
        {
            var exisingUser = await context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
            if (exisingUser != null) return false;

            var user = new User()
            {
                Username = request.Username,
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Password = EncryptionUtil.Encrypt(request.Password!),
                PhoneNumber = request.PhoneNumber,
                UserRole = request.UserRole
            };

            await context.Users.AddAsync(user);
            return true;
           
        }

        public async Task<User> UpdateUserAsync(int userId, UserPatchDTO request)
        {
            var user = await context.Users.Where(x => x.Id == userId).FirstAsync();

            user.Email = request.Email;
            user.Password = EncryptionUtil.Encrypt(request.Password!);
            user.PhoneNumber = request.PhoneNumber;

            context.Users.Update(user);
            return user;
        }
    }
}
