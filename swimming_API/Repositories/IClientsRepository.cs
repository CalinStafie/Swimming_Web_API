using proiect_final_API.Entities;
using proiect_final_API.Models;
using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Repositories
{
    public interface IClientsRepository : IGenericRepository<Client>
    {
        Task<int> GetNumberOfStillValiblePurchases(long clientId); // numarul de abonamente inca valabile
        Task<Client> getUserData(int userId);
    }
}
