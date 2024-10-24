using EmberAPI.APIContext;
using EmberAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Repositories;

public class ClientRepository : Repository<Client>, IClientRepository
{
    private readonly MainContext _context;

    public ClientRepository(MainContext context) : base(context)
    {
        _context = context;
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

   
}