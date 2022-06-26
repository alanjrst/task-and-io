using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using task.io.authentication.Domain.Base;

namespace task.io.authentication.Domain.Entities.Interfaces
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetById(long id);
        Task<IList<T>> GetAll();
        Task CommitAsync();
        IQueryable<T> GetQueryable();
        Task ExecuteCommand(string command);
    }
}