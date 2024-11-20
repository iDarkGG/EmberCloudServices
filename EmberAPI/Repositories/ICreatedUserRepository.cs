using EmberAPI.Models;

namespace EmberAPI.Repositories
{
    public interface ICreatedUserRepository : IRepository<CreatedUser>
    {
        public Task UpdateUser(CreatedUser user);
    }
}
