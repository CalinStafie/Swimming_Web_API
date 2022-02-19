using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proiect_final_API.Managers;
using proiect_final_API.Models;
using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NormalWorkDaysController : ControllerBase
    {
        private readonly INormalWorkDayManager _normalWorkDayManager;

        public NormalWorkDaysController(INormalWorkDayManager normalWorkDayManager)
        {
            _normalWorkDayManager = normalWorkDayManager;
        }

        [HttpGet("get-receptionist-work-plan")]
        public async Task<IActionResult> GetWorkPlan(long receptionistId)
        {
            try
            {
                var workPlan = await _normalWorkDayManager.GetNormalWorkPlan(receptionistId);
                return Ok(workPlan);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-receptionist-work-day-plan")]
        public async Task<IActionResult> GetWorkDayPlan(long receptionistId, int dayOfWeek)
        {
            try
            {
                var workDayPlan = await _normalWorkDayManager.GetNormalWorkDay(receptionistId, dayOfWeek);
                return Ok(workDayPlan);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-receptionist-work-day-plan-by-id")]
        public async Task<IActionResult> GetWorkDayPlanById(long id)
        {
            try
            {
                var workDayPlan = await _normalWorkDayManager.GetById(id);
                return Ok(workDayPlan);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert-work-day-plan")]
        public async Task<IActionResult> InsertWorkDayPlan(NormalWorkDayPostModel postModel)
        {
            try
            {
                var workDayPlanDTO = new NormalWorkDayDTO
                {
                    DayOfWeek = postModel.DayOfWeek,
                    ReceptionistId = postModel.ReceptionistId,
                    StartHour = TimeSpan.Parse(postModel.StartHour),
                    EndHour = TimeSpan.Parse(postModel.EndHour),
                    BreakStartHour = TimeSpan.Parse(postModel.BreakStartHour),
                    BreakEndHour = TimeSpan.Parse(postModel.BreakEndHour),
                };
                var insertedWorkDayPlan = await _normalWorkDayManager.Insert(workDayPlanDTO);
                return Ok(insertedWorkDayPlan);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update-work-day-plan")]
        public async Task<IActionResult> UpdateWorkDayPlan(long id, NormalWorkDayPostModel postModel)
        {
            try
            {
                var workDayPlanDTO = new NormalWorkDayDTO
                {
                    Id = postModel.Id,
                    DayOfWeek = postModel.DayOfWeek,
                    ReceptionistId = postModel.ReceptionistId,
                    StartHour = TimeSpan.Parse(postModel.StartHour),
                    EndHour = TimeSpan.Parse(postModel.EndHour),
                    BreakStartHour = TimeSpan.Parse(postModel.BreakStartHour),
                    BreakEndHour = TimeSpan.Parse(postModel.BreakEndHour),
                };
                var updatedWorkDayPlan = await _normalWorkDayManager.Update(id, workDayPlanDTO);
                return Ok(updatedWorkDayPlan);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete-work-day-plan")]
        public async Task<IActionResult> DeleteWorkDayPlan(NormalWorkDayDTO normalWorkDayDTO)
        {
            try
            {
                var deletedNormalWorkDayDto = await _normalWorkDayManager.Delete(normalWorkDayDTO);
                return Ok(deletedNormalWorkDayDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
