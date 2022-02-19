using AutoMapper;
using proiect_final_API.Entities;
using proiect_final_API.Models;
using proiect_final_API.Models.DTO_s;
using proiect_final_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Managers
{
    public class PurchasesManager : IPurchasesManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchasesManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<List<ReceptionistEmissionsModel>> GetReceptionistEmissions(long receptionistId)
        {
            return _unitOfWork.PurchasesRepository.GetReceptionistEmissions(receptionistId);
        }

        public Task<List<ClientPurchasesModel>> GetClientPurchases(long clientId)
        {
            return _unitOfWork.PurchasesRepository.GetClientPurchases(clientId);
        }

        public Task<List<PurchasesInformationModel>> GetPurchassesInformation()
        {
            return _unitOfWork.PurchasesRepository.GetPurchasesInformation();
        }

        public Task<PurchasesInformationModel> GetPurchaseInformationById(long clientId, long receptionistId, long subscriptionId, DateTime startTime)

        {
            return _unitOfWork.PurchasesRepository.GetPurchaseInformation(clientId, receptionistId, subscriptionId, startTime);
        }

        public async Task<PurchaseDTO> GetById(long clientId, long receptionistId, long subscriptionId, DateTime startTime)
        {
            Object[] compositeKey = { clientId, receptionistId, subscriptionId, startTime };
            var purchase = _unitOfWork.PurchasesRepository.GetByCompositeKeyAsync(compositeKey);

            if (purchase == null)
            {
                throw new ArgumentException("There is no purchase with this id!");
            }

            var purchaseDTO = _mapper.Map<PurchaseDTO>(purchase);
            await _unitOfWork.SaveAsync();
            return purchaseDTO;
        }

        public async Task<PurchasesInformationModel> Insert(PurchaseDTO purchaseDTO)
        {
            if (await _unitOfWork.ReceptionistsRepository.GetByIdAsync(purchaseDTO.ReceptionistId) == null)
            {
                throw new ArgumentException("No receptionist with this id exists!");
            }
            if (await _unitOfWork.SubscriptionsRepository.GetByIdAsync(purchaseDTO.SubscriptionId) == null)
            {
                throw new ArgumentException("No subscription with this id exists!");
            }
            if (await _unitOfWork.ClientsRepository.GetByIdAsync(purchaseDTO.ClientId) == null)
            {
                throw new ArgumentException("No client with this id exists!");
            }

            var purchase = _mapper.Map<Purchase>(purchaseDTO);
            await _unitOfWork.PurchasesRepository.InsertAsync(purchase);
            return new PurchasesInformationModel
            {
                receptionistDTO = _mapper.Map<ReceptionistDTO>(purchase.Receptionist),
                clientDTO = _mapper.Map<ClientDTO>(purchase.Client),
                subscriptionDTO = _mapper.Map<SubscriptionDTO>(purchase.Subscription),
                StartTime = purchase.StartTime,
                EndTime = purchase.EndTime
            };
        }

        public async Task<PurchaseDTO> UpdateTime(long clientId, long receptionistId, long subscriptionId, DateTime startTime, DateTime newStartTime)
        {
            var currentPurchaseDTO = await GetById(clientId, receptionistId, subscriptionId, startTime);
            var updatedPurchaseDTO = createNewPurchaseDTO(currentPurchaseDTO, newStartTime);
            await _unitOfWork.SaveAsync();

            var currentPurchase = _mapper.Map<Purchase>(currentPurchaseDTO);
            var updatedPurchase = _mapper.Map<Purchase>(updatedPurchaseDTO);

            _unitOfWork.PurchasesRepository.Delete(currentPurchase);
            _unitOfWork.PurchasesRepository.Insert(updatedPurchase);
            await _unitOfWork.SaveAsync();

            return updatedPurchaseDTO;
        }

        public async Task<PurchaseDTO> Delete(PurchaseDTO purchaseDTO)
        {
            var purchase = _mapper.Map<Purchase>(purchaseDTO);
            _unitOfWork.PurchasesRepository.Delete(purchase);
            await _unitOfWork.SaveAsync();
            return purchaseDTO;
        }

        private PurchaseDTO createNewPurchaseDTO(PurchaseDTO purchaseDTO, DateTime newStartTime)
        {
            return new PurchaseDTO
            {
                ClientId = purchaseDTO.ClientId,
                ReceptionistId = purchaseDTO.ReceptionistId,
                SubscriptionId = purchaseDTO.SubscriptionId,
                StartTime = newStartTime,
                EndTime = newStartTime + (purchaseDTO.EndTime - purchaseDTO.StartTime)
            };
        }
    }
}
