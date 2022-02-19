using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Managers
{
    public interface INormalWorkDayManager
    {
        Task<List<NormalWorkDayDTO>> GetNormalWorkPlan(long receptionistId);

        Task<NormalWorkDayDTO> GetNormalWorkDay(long receptionistId, int day);

        Task<List<NormalWorkDayDTO>> GetAll();

        Task<NormalWorkDayDTO> GetById(long id);

        Task<NormalWorkDayDTO> Insert(NormalWorkDayDTO normalWorkDayDTO);

        Task<NormalWorkDayDTO> Update(long id, NormalWorkDayDTO normalWorkDayDTO);

        Task<NormalWorkDayDTO> Delete(NormalWorkDayDTO normalWorkDayDTO);

    }
}
