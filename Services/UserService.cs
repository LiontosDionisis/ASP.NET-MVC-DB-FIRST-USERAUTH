using AutoMapper;
using TeachersMVC.Data;
using TeachersMVC.DTO;
using TeachersMVC.Repositories;

namespace TeachersMVC.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _unitOfWork.UserRepository.GetByUsernameAsync(username);
        }

        public async Task<User?> LoginUserAsync(UserLoginDTO credentials)
        {
            var user = await _unitOfWork.UserRepository.GetUserAsync(credentials.Username!, credentials.Password!);
            if (user == null) return null;
            return user;
        }

        public async Task SignUpUserAsync(UserSignupDTO request)
        {
            if (!await _unitOfWork.UserRepository.SignUpUserAsync(request))
            {
                throw new ApplicationException("User already exists!");
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task<User> UpdateUserAccountInfoAsync(UserPatchDTO request, int userId)
        {
            var user = await _unitOfWork.UserRepository.UpdateUserAsync(userId, request);
            await _unitOfWork.SaveAsync();
            return user;
        }
    }
}
