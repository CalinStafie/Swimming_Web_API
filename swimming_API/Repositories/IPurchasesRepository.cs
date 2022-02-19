using proiect_final_API.Entities;
using proiect_final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Repositories
{
    public interface IPurchasesRepository : IGenericRepository<Purchase>
    {
        Task<List<ClientPurchasesModel>> GetClientPurchases(long clientId);
        Task<List<ReceptionistEmissionsModel>> GetReceptionistEmissions(long receptionistId);
        Task<List<PurchasesInformationModel>> GetPurchasesInformation();
        Task<PurchasesInformationModel> GetPurchaseInformation(long clientId, long receptionistId, long subscriptionId, DateTime startTime);

        Purchase GetByCompositeKeyAsync(object[] id);
    }
}
