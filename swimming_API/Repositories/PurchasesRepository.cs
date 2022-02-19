using AutoMapper;
using Microsoft.EntityFrameworkCore;
using proiect_final_API.Data;
using proiect_final_API.Entities;
using proiect_final_API.Models;
using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Repositories
{
    public class PurchasesRepository : GenericRepository<Purchase>, IPurchasesRepository
    {
        private readonly IMapper _mapper;

        public PurchasesRepository(projectContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public Purchase GetByCompositeKeyAsync(object[] id)
        {
            var purchase = entities.Find(id);
            _context.Entry(purchase).State = EntityState.Detached;
            return purchase;
        }

        public async Task<PurchasesInformationModel> GetPurchaseInformation(long clientId, long receptionistId, long subscriptionId, DateTime startTime)
        {
            var purchase = await entities
                .Include(x => x.Receptionist)
                .Include(x => x.Client)
                .Include(x => x.Subscription)
                .FirstOrDefaultAsync(a => a.ClientId == clientId && a.ReceptionistId == receptionistId && a.SubscriptionId == subscriptionId && a.StartTime == startTime);
            var purchaseInformation = new PurchasesInformationModel
            {
                receptionistDTO = _mapper.Map<ReceptionistDTO>(purchase.Receptionist),
                clientDTO = _mapper.Map<ClientDTO>(purchase.Client),
                subscriptionDTO = _mapper.Map<SubscriptionDTO>(purchase.Subscription),
                StartTime = purchase.StartTime,
                EndTime = purchase.EndTime
            };

            _context.Entry(purchase).State = EntityState.Detached;
            return purchaseInformation;
        }

        public async Task<List<ReceptionistEmissionsModel>> GetReceptionistEmissions(long receptionistId)
        {
            var clientPurchasesModels = await entities
                .Include(x => x.Receptionist)
                .Include(x => x.Client)
                .Include(x => x.Subscription)
                .Where(x => x.ReceptionistId == receptionistId)
                .Select(x => new ReceptionistEmissionsModel
                {
                    SubscriptionName = x.Subscription.SubscriptionName,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    ClientName = x.Client.LastName + " " + x.Client.FirstName
                })
                .OrderBy(x => x.StartTime)
                .ToListAsync();
            return clientPurchasesModels;
        }

        public async Task<List<ClientPurchasesModel>> GetClientPurchases(long clientId)
        {
            var clientPurchasesModels = await entities
                .Include(x => x.Receptionist)
                .Include(x => x.Client)
                .Include(x => x.Subscription)
                .Where(x => x.ClientId == clientId)
                .Select(x => new ClientPurchasesModel
                {
                    SubscriptionName = x.Subscription.SubscriptionName,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    ReceptionistName = x.Receptionist.LastName + " " + x.Receptionist.FirstName
                })
                .OrderBy(x => x.StartTime)
                .ToListAsync();
            return clientPurchasesModels;
        }

        public async Task<List<PurchasesInformationModel>> GetPurchasesInformation()
        {
            var purchasesInformationModels = await entities
                .Include(x => x.Receptionist)
                .Include(x => x.Client)
                .Include(x => x.Subscription)
                .Select(x => new PurchasesInformationModel
                {
                    receptionistDTO = _mapper.Map<ReceptionistDTO>(x.Receptionist),
                    clientDTO = _mapper.Map<ClientDTO>(x.Client),
                    subscriptionDTO = _mapper.Map<SubscriptionDTO>(x.Subscription),
                    StartTime = x.StartTime,
                    EndTime = x.EndTime
                })
                .OrderBy(x => x.StartTime)
                .ToListAsync();
            return purchasesInformationModels;
        }
    }
}
