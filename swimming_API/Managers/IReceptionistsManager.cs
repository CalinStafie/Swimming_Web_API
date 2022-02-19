using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Managers
{
    public interface IReceptionistsManager
    {
        Task<bool> IsWorking(long receptionistId, DateTime date);

        Task<ReceptionistDTO> Insert(ReceptionistDTO receptionistDTO);

        Task<List<ReceptionistDTO>> GetAll();

        Task<List<ReceptionistInformationForUsersDTO>> GetAllInformationForUsers();

        Task<ReceptionistDTO> GetById(long id);

        Task<bool> Delete(long id);

        Task<ReceptionistDTO> Update(long receptionistId, ReceptionistDTO receptionistDTO);

    }
}
