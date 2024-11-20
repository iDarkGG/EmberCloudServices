using EmberAPI.APIContext;
using EmberAPI.Models;

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

    }
}
