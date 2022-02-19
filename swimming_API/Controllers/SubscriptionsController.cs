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
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionsManager _subscriptionsManager;

        public SubscriptionsController(ISubscriptionsManager subscriptionsManager)
        {
            _subscriptionsManager = subscriptionsManager;
        }

        [HttpGet("get-all-subscriptions")]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            return Ok(await _subscriptionsManager.GetAll());
        }

        [HttpGet("get-subscription-by-id/{id}")]
        public async Task<IActionResult> GetSubscriptionById(long id)
        {
            try
            {
                return Ok(await _subscriptionsManager.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert-subscription")]
        public async Task<IActionResult> InsertSubscription(SubscriptionDTO subscriptionDTO)
        {
            try
            {
                var insertedSubscriptionDTO = await _subscriptionsManager.Insert(subscriptionDTO);
                return Ok(insertedSubscriptionDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update-subscription")]
        public async Task<IActionResult> UpdateSubscription(long id, SubscriptionDTO subscriptionDTO)
        {
            try
            {
                var updatedSubscription = await _subscriptionsManager.Update(id, subscriptionDTO);
                return Ok(updatedSubscription);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update-subscription-cost")]
        public async Task<IActionResult> UpdateSubscriptionCost(long id, int newCost)
        {
            try
            {
                var updatedSubscription = await _subscriptionsManager.UpdateCost(id, newCost);
                return Ok(updatedSubscription);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete-subscription/{id}")]
        public async Task<IActionResult> DeleteSubscription(long id)
        {
            try
            {
                var result = await _subscriptionsManager.Delete(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}

