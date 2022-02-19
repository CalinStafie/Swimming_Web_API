using Microsoft.EntityFrameworkCore;
using proiect_final_API.Data;
using proiect_final_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Repositories
{
    public class ClientsRepository : GenericRepository<Client>, IClientsRepository
    {
        public ClientsRepository (projectContext context) : base(context) { }

        public async Task<int> GetNumberOfStillValiblePurchases(long clientId)
        {
            var client = await entities
                .Include(x => x.Purchases.Where(a => a.EndTime > DateTime.Now))
                .FirstOrDefaultAsync(x => x.Id == clientId);
            var numberOfPurchases = client.Purchases.Count();
            return numberOfPurchases;
        }

        public async Task<Client> getUserData(int userId)
        {
            return await entities.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
