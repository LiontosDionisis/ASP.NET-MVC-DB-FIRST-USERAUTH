using AutoMapper;
using TeachersMVC.Repositories;

namespace TeachersMVC.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public IUserService UserService => new UserService(_unitOfWork, _mapper, _logger);
        //public ITeacherService TeacherService => new TeacherService(_unitOfWork, _mapper, _logger);

    }
}
