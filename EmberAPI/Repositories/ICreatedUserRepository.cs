using EmberAPI.Dtos;
using EmberAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmberAPI.Repositories
{
    public interface ICreatedUserRepository : IRepository<CreatedUser>
    {
        public Task UpdateUser(CreatedUser user);
        
        Task<IEnumerable<CreatedUser>> GetAllUsersByInstanceIDAsync(int instanceID);
        
        Task DeleteAllUserByInstanceId(int instanceID);

    }
}
