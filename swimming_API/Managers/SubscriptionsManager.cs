using AutoMapper;
using proiect_final_API.Entities;
using proiect_final_API.Models.DTO_s;
using proiect_final_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Managers
{
    public class SubscriptionsManager : ISubscriptionsManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscriptionsManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubscriptionDTO> Insert(SubscriptionDTO subscriptionDTO)
        {
            var subscriptionToInsert = _mapper.Map<Subscription>(subscriptionDTO);
            var subscription = await _unitOfWork.SubscriptionsRepository.InsertAsync(subscriptionToInsert);
            return _mapper.Map<SubscriptionDTO>(subscription);
        }

        public async Task<SubscriptionDTO> Update(long id, SubscriptionDTO subscriptionDTO)
        {
            if (subscriptionDTO.Id != id)
            {
                throw new ArgumentException("Updated subscription does not have the correct id!");
            }
            var subscription = _mapper.Map<Subscription>(subscriptionDTO);
            _unitOfWork.SubscriptionsRepository.Update(subscription);
            await _unitOfWork.SaveAsync();
            return subscriptionDTO;
        }

        public async Task<SubscriptionDTO> UpdateCost(long id, int newCost)
        {
            _unitOfWork.SubscriptionsRepository.UpdateCost(id, newCost);
            return await GetById(id);
        }

        public async Task<List<SubscriptionDTO>> GetAll()
        {
            var subscriptions = await _unitOfWork.SubscriptionsRepository.GetAllAsync();
            var subscriptionDTOs = _mapper.Map<IEnumerable<Subscription>, List<SubscriptionDTO>>(subscriptions);
            return subscriptionDTOs;
        }

        public async Task<SubscriptionDTO> GetById(long id)
        {
            var subscription = await _unitOfWork.SubscriptionsRepository.GetByIdAsync(id);
            if (subscription == null)
            {
                throw new ArgumentException("No subscription with this id exists!");
            }
            return _mapper.Map<SubscriptionDTO>(subscription);
        }

        public async Task<SubscriptionDTO> Delete(SubscriptionDTO subscriptionDTO)
        {
            var subscription = _mapper.Map<Subscription>(subscriptionDTO);
            _unitOfWork.SubscriptionsRepository.Delete(subscription);
            await _unitOfWork.SaveAsync();
            return subscriptionDTO;
        }

        public async Task<bool> Delete(long id)
        {
            _unitOfWork.SubscriptionsRepository.Delete(id);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}






