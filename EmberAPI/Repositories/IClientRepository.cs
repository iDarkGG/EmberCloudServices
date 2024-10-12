using EmberAPI.Models;

namespace EmberAPI.Repositories;

public interface IClientRepository : IRepository<Client>
{
    Task UpdateAsync(Client client);
}