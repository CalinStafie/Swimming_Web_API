using proiect_final_API.Models;
using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Managers
{
    public interface IPurchasesManager
    {
        Task<List<ReceptionistEmissionsModel>> GetReceptionistEmissions(long receptionistId);

        Task<List<ClientPurchasesModel>> GetClientPurchases(long clientId);

        Task<List<PurchasesInformationModel>> GetPurchassesInformation();

        Task<PurchasesInformationModel> GetPurchaseInformationById(long clientId, long receptionistId, long subscriptionId, DateTime startTime);

        Task<PurchaseDTO> GetById(long clientId, long receptionistId, long subscriptionId, DateTime startTime);

        Task<PurchasesInformationModel> Insert(PurchaseDTO purchaseDTO);

        Task<PurchaseDTO> UpdateTime(long clientId, long receptionistId, long subscriptionId, DateTime startTime, DateTime newStartTime);

        Task<PurchaseDTO> Delete(PurchaseDTO purchaseDTO);
    }
}
