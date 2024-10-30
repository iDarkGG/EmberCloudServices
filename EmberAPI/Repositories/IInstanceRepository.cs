using EmberAPI.Models;

namespace EmberAPI.Repositories;

public interface IInstanceRepository : IRepository<Instance>
{
    Task<IEnumerable<Instance>> GetAllInstancesByClientIDAsync(int clientId);
}