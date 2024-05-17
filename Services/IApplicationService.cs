namespace TeachersMVC.Services
{
    public interface IApplicationService
    {
        IUserService UserService { get; }
        // ITeacherService TeacherService { get; }
    }
}
