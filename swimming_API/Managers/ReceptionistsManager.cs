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
    public class ReceptionistsManager : IReceptionistsManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReceptionistsManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> IsWorking(long receptionistId, DateTime date)
        {
            return await _unitOfWork.ReceptionistsRepository.IsWorking(receptionistId, date);
        }

        public async Task<ReceptionistDTO> Insert(ReceptionistDTO receptionistDTO)
        {
            var doctor = _mapper.Map<Receptionist>(receptionistDTO);
            await _unitOfWork.ReceptionistsRepository.InsertAsync(doctor);
            return receptionistDTO;
        }

        public async Task<List<ReceptionistDTO>> GetAll()
        {
            var receptionists = await _unitOfWork.ReceptionistsRepository.GetAllAsync();
            return _mapper.Map<List<Receptionist>, List<ReceptionistDTO>>(receptionists);
        }

        public async Task<List<ReceptionistInformationForUsersDTO>> GetAllInformationForUsers()
        {
            var receptionists = await _unitOfWork.ReceptionistsRepository.GetAllAsync();
            return _mapper.Map<List<Receptionist>, List<ReceptionistInformationForUsersDTO>>(receptionists);
        }

        public async Task<ReceptionistDTO> GetById(long id)
        {
            var receptionist = await _unitOfWork.ReceptionistsRepository.GetByIdAsync(id);
            if (receptionist == null)
            {
                throw new ArgumentException("No receptionist with this id exists!");
            }
            return _mapper.Map<ReceptionistDTO>(receptionist);
        }

        public async Task<bool> Delete(long id)
        {
            _unitOfWork.ReceptionistsRepository.Delete(id);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<ReceptionistDTO> Update(long receptionistId, ReceptionistDTO receptionistDTO)
        {
            if (receptionistDTO.Id != receptionistId)
            {
                throw new ArgumentException("Updated receptionist does not have the correct id!");
            }
            var receptionist = _mapper.Map<Receptionist>(receptionistDTO);
            _unitOfWork.ReceptionistsRepository.Update(receptionist);
            await _unitOfWork.SaveAsync();
            return receptionistDTO;
        }
    }
}
