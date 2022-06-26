using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using task.io.authentication.Domain.Entities.Users;

namespace task.io.authentication.InfraStructure.Data
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(EFContext context) : base(context)
        {}
        public async Task<User> GetByEmailAsync(string email){
            return await Query.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password){
            return await Query.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<User> GetByEmailAndToken(string email, string token)
        {
            return await Query.FirstOrDefaultAsync(x => x.Email == email && x.TokenRememberPassword == token);

        }
    }
}