using EmberAPI.APIContext;
using EmberAPI.Models;

namespace EmberAPI.Repositories
{
    public class DBRoleRepository : Repository<DBRole>, IDBRoleRepository
    {
        private readonly MainContext _context;

        public DBRoleRepository(MainContext context) : base(context)
        {
            _context = context;
        }

        public async Task UpdateRole(DBRole dBRole)
        {
            _context.DBRoles.Update(dBRole);
            await _context.SaveChangesAsync();
        }
    }
}
