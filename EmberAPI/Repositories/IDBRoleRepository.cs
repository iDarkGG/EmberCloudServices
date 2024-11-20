using EmberAPI.Models;

namespace EmberAPI.Repositories
{
    public interface IDBRoleRepository : IRepository<DBRole>
    {
        Task UpdateRole(DBRole role);
    }
}
