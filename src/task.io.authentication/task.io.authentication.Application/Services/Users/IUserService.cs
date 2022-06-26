using System.Threading.Tasks;
using task.io.authentication.Application.DTOs.Users;

public interface IUserService
{
    Task<UserResponseLogin> CheckLoginAsync(UserRequestLogin userRequestLogin);
    Task<bool> ChangePasswordAsync(UserRequestChangePassword userRequestChangePassword);
    Task<UserResponse> AddNewAsync(UserRequest userRequest);
    Task<UserResponse> EditAsync(UserRequest userRequest);
    Task<bool> RequestResetPassword(UserRequest userRequest);
    Task<bool> RestorePassword(string email, string token, string password);

}