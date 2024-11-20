
using EmberAPI.APIContext;
using EmberAPI.Dtos;
using EmberAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Repositories
{
    public class CreatedUserRepository: Repository<CreatedUser>, ICreatedUserRepository
    {
        private readonly MainContext _context;

        public CreatedUserRepository(MainContext context) : base (context)
        {
            _context = context;
        }


        public async Task UpdateUser(CreatedUser user)
        {
            _context.CreatedUsers.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<CreatedUser>> GetAllUsersByInstanceIDAsync(int instanceId)
        {
            return await _context.CreatedUsers.Where(u => u.InstanceID == instanceId).ToListAsync();
        }

        public async Task DeleteAllUserByInstanceId(int instanceId)
        {
           var result = await GetAllUsersByInstanceIDAsync(instanceId);
           foreach (var user in result) 
               await DeleteAsync(user);
        }
        

    }
}
