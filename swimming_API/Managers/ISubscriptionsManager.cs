using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Managers
{
    public interface ISubscriptionsManager
    {
        Task<SubscriptionDTO> Insert(SubscriptionDTO subscriptionDTO);

        Task<SubscriptionDTO> Update(long id, SubscriptionDTO subscriptionDTO);

        Task<SubscriptionDTO> UpdateCost(long id, int newCost);

        Task<List<SubscriptionDTO>> GetAll();

        Task<SubscriptionDTO> GetById(long id);

        Task<SubscriptionDTO> Delete(SubscriptionDTO subscriptionDTO);

        Task<bool> Delete(long id);
    }
}
