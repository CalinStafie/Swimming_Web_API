using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Managers
{
    public interface IClientsManager
    {
        Task<int> GetNumberOfStillValiblePurchases(long clientId);

        Task<ClientDTO> Insert(ClientDTO clientDTO);

        Task<ClientDTO> Update(long clientId, ClientDTO clientDTO);

        Task<List<ClientDTO>> GetAll();

        Task<dynamic> GetAllJoined();

        Task<ClientDTO> GetById(long id);

        Task<ClientDTO> GetUserData(int userId);

        Task<ClientDTO> Delete(ClientDTO clientDTO);
    }
}
