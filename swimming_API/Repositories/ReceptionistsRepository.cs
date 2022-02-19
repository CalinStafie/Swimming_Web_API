using Microsoft.EntityFrameworkCore;
using proiect_final_API.Data;
using proiect_final_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Repositories
{
    public class ReceptionistsRepository : GenericRepository<Receptionist>, IReceptionistsRepository
    {
        public ReceptionistsRepository (projectContext context) : base(context) { }

        public async Task<bool> IsWorking(long receptionistId, DateTime date)
        {
            var receptionistWorkData =
                await entities
                .Include(x => x.VacationDays)
                .Include(x => x.NormalWorkDays.Where(x => x.DayOfWeek == ((int)date.DayOfWeek)))
                .FirstOrDefaultAsync(x => x.Id == receptionistId);

            if (receptionistWorkData.VacationDays.Where(x => x.Date == date).Count() == 0 && receptionistWorkData.NormalWorkDays.Count != 0) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
