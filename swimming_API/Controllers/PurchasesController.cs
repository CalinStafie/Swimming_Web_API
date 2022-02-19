using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proiect_final_API.Managers;
using proiect_final_API.Models.DTO_s;
using proiect_final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace proiect_final_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchasesManager _purchasesManager;

        private readonly IClientsManager _clientsManager;

        public PurchasesController(IPurchasesManager purchasesManager, IClientsManager clientsManager)
        {
            _purchasesManager = purchasesManager;
            _clientsManager = clientsManager;
        }

        [Authorize("Admin")]
        [HttpGet("get-receptionist-purchases/{id}")]
        public async Task<IActionResult> GetReceptionistPurchases(long id)
        {
            return Ok(await _purchasesManager.GetReceptionistEmissions(id));
        }

        [Authorize("Client")]
        [HttpGet("get-client-purchases/{clientId}")]
        public async Task<IActionResult> GetClientPurchases(long clientId)
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
                return Ok(await _purchasesManager.GetClientPurchases(clientId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Authorize("Admin")]
        [HttpGet("get-all-purchases")]
        public async Task<IActionResult> GetAllPurchases()
        {
            return Ok(await _purchasesManager.GetPurchassesInformation());
        }

        [Authorize("Admin")]
        [HttpGet("get-purchase")]
        public async Task<IActionResult> GetPurchaseById(long clientId, long receptionistId, long subscriptionId, DateTime startTime)
        {
            try
            {
                var purchase = await _purchasesManager.GetById(clientId, receptionistId, subscriptionId, startTime);
                return Ok(purchase);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Admin")]
        [HttpPost("get-purchase-information")]
        public async Task<IActionResult> GetPurchaseInformationById(PurchaseDTO purchase)
        {
            try
            {
                var purchaseInfo = await _purchasesManager.GetPurchaseInformationById(purchase.ClientId, purchase.ReceptionistId, purchase.SubscriptionId, purchase.StartTime);
                return Ok(purchaseInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Admin")]
        [HttpPost("insert-purchase")]
        public async Task<IActionResult> PostPurchase(PurchaseDTO purchaseDTO)
        {
            try
            {
                var insertedPurchaseDTO = await _purchasesManager.Insert(purchaseDTO);
                return Ok(insertedPurchaseDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Admin")]
        [HttpPut("update-purchase-time")]
        public async Task<IActionResult> UpdatePurchase(NewStartTimeSubscriptionModel startTimePurchaseModel)
        {
            try
            {
                var updatedPurchaseDTO = await _purchasesManager.UpdateTime(startTimePurchaseModel.ClientId, startTimePurchaseModel.ReceptionistId, startTimePurchaseModel.SubscriptionId, startTimePurchaseModel.StartTime, startTimePurchaseModel.NewStartTime);
                return Ok(updatedPurchaseDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize("Admin")]
        [HttpDelete("delete-purchase")]
        public async Task<IActionResult> DeletePurchase(PurchaseDTO purchaseDTO)
        {
            try
            {
                var deletedPurchaseDTO = await _purchasesManager.Delete(purchaseDTO);
                return Ok(deletedPurchaseDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
