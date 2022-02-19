using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proiect_final_API.Managers;
using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistsController : ControllerBase
    {
        private readonly IReceptionistsManager _receptionistsManager;

        public ReceptionistsController(IReceptionistsManager receptionistsManager)
        {
            _receptionistsManager = receptionistsManager;
        }

        [HttpGet("test-get")]
        public async Task<IActionResult> GetWorkingReceptionist(long receptionistId, DateTime date)
        {
            return Ok(await _receptionistsManager.IsWorking(receptionistId, date));
        }

        [AllowAnonymous]
        [HttpGet("get-all-receptionists")]
        public async Task<IActionResult> GetAllReceptionists()
        {
            return Ok(await _receptionistsManager.GetAllInformationForUsers());
        }

        [HttpPost("insert-receptionist")]
        public async Task<IActionResult> InsertReceptionist(ReceptionistDTO receptionistDTO)
        {
            try
            {
                var insertedReceptionistDTO = await _receptionistsManager.Insert(receptionistDTO);
                return Ok(insertedReceptionistDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-receptionist-by-id/{id}")]
        public async Task<IActionResult> GetReceptionistById(long id)
        {
            try
            {
                return Ok(await _receptionistsManager.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Admin")]
        [HttpDelete("delete-receptionist/{id}")]
        public async Task<IActionResult> DeleteReceptionist(long id)
        {
            try
            {
                var deletedReceptionistDTO = await _receptionistsManager.Delete(id);
                return Ok(deletedReceptionistDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPut("update-receptionist/{receptionistId}")]
        public async Task<IActionResult> UpdateReceptionist(long receptionistId, ReceptionistDTO receptionistDTO)
        {
            try
            {
                var updatedReceptionist = await _receptionistsManager.Update(receptionistId, receptionistDTO);
                return Ok(updatedReceptionist);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
