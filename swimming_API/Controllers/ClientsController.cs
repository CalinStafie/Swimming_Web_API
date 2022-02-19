using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proiect_final_API.Managers;
using proiect_final_API.Models.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace proiect_final_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsManager _clientsManager;

        public ClientsController(IClientsManager clientsManager)
        {
            _clientsManager = clientsManager;
        }

        [HttpPost("insert-client")]
        public async Task<IActionResult> InsertClient(ClientDTO clientDTO)
        {
            try
            {
                var insertedClientDTO = await _clientsManager.Insert(clientDTO);
                return Ok(insertedClientDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Admin")]
        [HttpGet("get-all-clients")]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _clientsManager.GetAll());
        }

        [Authorize("Client")]
        [HttpGet("get-user-data/{userId}")]
        public async Task<IActionResult> GetUserData(int userId)
        {
            try
            {
                int authentifiedUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                if (authentifiedUserId != userId)
                {
                    return Unauthorized("Cannot acces data that isn't bound to this account");
                }
                var client = await _clientsManager.GetUserData(userId);
                return Ok(client);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Client")]
        [HttpGet("get-number-of-sill-valible-purchases/{userId}")]
        public async Task<IActionResult> GetNumberOfStillValiblePurchases(long clientId)
        {
            try
            {
                int authentifiedUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                ClientDTO authentifiedClient = await _clientsManager.GetUserData(authentifiedUserId);
                if (clientId != authentifiedClient.Id)
                {
                    return Unauthorized("Cannot acces data that isn't bound to this account");
                }
                var numberOfStillValiblePurchases = await _clientsManager.GetNumberOfStillValiblePurchases(clientId);
                return Ok(numberOfStillValiblePurchases);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Client")]
        [HttpPut("update-client/{clientId}")]
        public async Task<IActionResult> UpdateClient(long clientId, ClientDTO clientDTO)
        {
            try
            {
                int authentifiedUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                ClientDTO authentifiedPacient = await _clientsManager.GetUserData(authentifiedUserId);
                if (clientId != authentifiedPacient.Id)
                {
                    return Unauthorized("Cannot acces data that isn't bound to this account");
                }
                var updatedClient = await _clientsManager.Update(clientId, clientDTO);
                return Ok(updatedClient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete-client")]
        public async Task<IActionResult> DeleteClient(ClientDTO clientDTO)
        {
            try
            {
                var deletedClientDto = await _clientsManager.Delete(clientDTO);
                return Ok(deletedClientDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
