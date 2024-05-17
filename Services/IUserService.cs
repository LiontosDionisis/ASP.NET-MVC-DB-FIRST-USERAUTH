using TeachersMVC.Data;
using TeachersMVC.DTO;

namespace TeachersMVC.Services
{
    public interface IUserService
    {
        Task SignUpUserAsync(UserSignupDTO request);
        Task<User?> LoginUserAsync(UserLoginDTO credentials);
        Task<User> UpdateUserAccountInfoAsync(UserPatchDTO request, int userId);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
