using EmberAPI.APIContext;
using EmberAPI.Models;
using EmberCloudServices.Utilities;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Repositories;

public class InstanceRepository: Repository<Instance>, IInstanceRepository
{
    private readonly MainContext _context;

    public InstanceRepository(MainContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Instance>> GetAllInstancesByClientIDAsync(int clientId)
    {
        return await _context.Instances.Where(i => i.ClientID == clientId).ToListAsync();
    }


}