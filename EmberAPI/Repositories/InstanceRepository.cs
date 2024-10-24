using EmberAPI.APIContext;
using EmberAPI.Models;

namespace EmberAPI.Repositories;

public class InstanceRepository: Repository<Instance>, IInstanceRepository
{
    private readonly MainContext _context;

    public InstanceRepository(MainContext context) : base(context)
    {
        _context = context;
    }
    
}