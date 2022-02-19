using proiect_final_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Repositories
{
    public interface IReceptionistsRepository : IGenericRepository<Receptionist>
    {
        Task<bool> IsWorking(long receptionistId, DateTime date);
    }
}
