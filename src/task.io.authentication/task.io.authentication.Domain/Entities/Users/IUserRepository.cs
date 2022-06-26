using System.Threading.Tasks;
using task.io.authentication.Domain.Entities.Interfaces;

namespace task.io.authentication.Domain.Entities.Users
{
    public interface IUserRepository : IRepositoryBase<User>{
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
        Task<User> GetByEmailAndToken(string email, string token);
    }
}