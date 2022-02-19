using AutoMapper;
using Microsoft.EntityFrameworkCore;
using proiect_final_API.Entities;
using proiect_final_API.Models.DTO_s;
using proiect_final_API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Managers
{
    public class NormalWorkDayManager : INormalWorkDayManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NormalWorkDayManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<NormalWorkDayDTO>> GetNormalWorkPlan(long receptionistId)
        {
            var workDayPlans = await _unitOfWork.NormalWorkDaysRepository.GetQueryable()
                .Where(x => x.ReceptionistId == receptionistId)
                .OrderBy(x => x.DayOfWeek)
                .ToListAsync();

            var workDayPlanDTOs = _mapper.Map<List<NormalWorkDay>, List<NormalWorkDayDTO>>(workDayPlans);
            return workDayPlanDTOs;
        }

        public async Task<NormalWorkDayDTO> GetNormalWorkDay(long receptionistId, int day)
        {
            if (day < 0 || day >= 7)
            {
                throw new ArgumentException("Day should be between 0 and 6!");
            }
            var workDayPlan = await _unitOfWork.NormalWorkDaysRepository.GetQueryable()
                .Where(x => x.ReceptionistId == receptionistId)
                .FirstOrDefaultAsync(x => x.DayOfWeek == day);

            var workDayPlanDTO = _mapper.Map<NormalWorkDayDTO>(workDayPlan);
            return workDayPlanDTO;
        }

        public async Task<List<NormalWorkDayDTO>> GetAll()
        {
            var workDayPlans = await _unitOfWork.NormalWorkDaysRepository.GetAllAsync();
            var workDayPlanDTOs = _mapper.Map<IEnumerable<NormalWorkDay>, List<NormalWorkDayDTO>>(workDayPlans);
            return workDayPlanDTOs;
        }

        public async Task<NormalWorkDayDTO> GetById(long id)
        {
            var workDayPlan = await _unitOfWork.NormalWorkDaysRepository.GetByIdAsync(id);
            if (workDayPlan == null)
            {
                throw new ArgumentException("There is no work day plan with this id!");
            }
            var workDayPlanDTO = _mapper.Map<NormalWorkDayDTO>(workDayPlan);
            return workDayPlanDTO;
        }

        public async Task<NormalWorkDayDTO> Insert(NormalWorkDayDTO normalWorkDayDTO)
        {
            if (await _unitOfWork.ReceptionistsRepository.GetByIdAsync(normalWorkDayDTO.ReceptionistId) == null)
            {
                throw new ArgumentException("No receptionist with this id exists!");
            }
            if (normalWorkDayDTO.DayOfWeek < 0 || normalWorkDayDTO.DayOfWeek >= 7)
            {
                throw new ArgumentException("DayOfWeek should be between 0 and 6!");
            }
            var workDayPlan = _mapper.Map<NormalWorkDay>(normalWorkDayDTO);
            await _unitOfWork.NormalWorkDaysRepository.InsertAsync(workDayPlan);
            return normalWorkDayDTO;
        }

        public async Task<NormalWorkDayDTO> Update(long id, NormalWorkDayDTO normalWorkDayDTO)
        {
            var workDayPlan = _mapper.Map<NormalWorkDay>(normalWorkDayDTO);
            _unitOfWork.NormalWorkDaysRepository.Update(workDayPlan);
            await _unitOfWork.SaveAsync();
            return normalWorkDayDTO;
        }

        public async Task<NormalWorkDayDTO> Delete(NormalWorkDayDTO normalWorkDayDTO)
        {
            var workDayPlan = _mapper.Map<NormalWorkDay>(normalWorkDayDTO);
            _unitOfWork.NormalWorkDaysRepository.Delete(workDayPlan);
            await _unitOfWork.SaveAsync();
            return normalWorkDayDTO;
        }
    }
}
