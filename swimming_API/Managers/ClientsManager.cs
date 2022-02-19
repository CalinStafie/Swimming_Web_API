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
    public class ClientsManager : IClientsManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientsManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> GetNumberOfStillValiblePurchases(long clientId)
        {
            if (await _unitOfWork.ClientsRepository.GetByIdAsync(clientId) == null)
            {
                throw new ArgumentException("No client exists!");
            }
            return await _unitOfWork.ClientsRepository.GetNumberOfStillValiblePurchases(clientId);
        }

        public async Task<ClientDTO> Insert(ClientDTO clientDTO)
        {
            var client = _mapper.Map<Client>(clientDTO);
            await _unitOfWork.ClientsRepository.InsertAsync(client);
            return _mapper.Map<ClientDTO>(client);
        }

        public async Task<ClientDTO> Update(long clientId, ClientDTO clientDTO)
        {
            if (clientDTO.Id != clientId)
            {
                throw new ArgumentException("Updated client does not have the correct id!");
            }
            var client = _mapper.Map<Client>(clientDTO);
            _unitOfWork.ClientsRepository.Update(client);
            await _unitOfWork.SaveAsync();
            return clientDTO;
        }

        public async Task<List<ClientDTO>> GetAll()
        {
            var clients = await _unitOfWork.ClientsRepository.GetAllAsync();
            return _mapper.Map<List<Client>, List<ClientDTO>>(clients);
        }

        public async Task<dynamic> GetAllJoined()
        {
            var vacationDays = await _unitOfWork.VacationDaysRepository.GetAllAsync();
            var addresses = await _unitOfWork.AddressesRepository.GetAllAsync();

            var joined = addresses.Join(vacationDays, a => a.VacationId, c => c.Id, (a, c) => new
            {
                a.Id,
                a.Country,
                a.City,
                VacationDate = c.Date
            });
            var joinedAdressesVacationDays = joined.ToList();
            return joinedAdressesVacationDays;
        }

            public async Task<ClientDTO> GetById(long id)
        {
            var client = await _unitOfWork.ClientsRepository.GetByIdAsync(id);
            if (client == null)
            {
                throw new ArgumentException("No client with this id exists!");
            }
            return _mapper.Map<ClientDTO>(client);
        }

        public async Task<ClientDTO> Delete(ClientDTO clientDTO)
        {
            var client = _mapper.Map<Client>(clientDTO);
            _unitOfWork.ClientsRepository.Delete(client);
            await _unitOfWork.SaveAsync();
            return clientDTO;
        }

        public async Task<ClientDTO> GetUserData(int userId)
        {
            var client = await _unitOfWork.ClientsRepository.getUserData(userId);
            return _mapper.Map<ClientDTO>(client);
        }
    }
}
