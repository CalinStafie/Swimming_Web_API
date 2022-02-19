using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proiect_final_API.Managers;
using proiect_final_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proiect_final_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IClientsManager _clientsManager;
        private readonly IReceptionistsManager _receptionistsManager;

        public AuthController(IAuthManager authManager, IClientsManager clientsManager, IReceptionistsManager receptionistsManager)
        {
            _authManager = authManager;
            _clientsManager = clientsManager;
            _receptionistsManager = receptionistsManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var result = await _authManager.Register(registerModel);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("register-as-client")]
        public async Task<IActionResult> RegisterAsClient([FromBody] ClientUserPostModel clientUserPostModel)
        {
            var userId = await _authManager.RegisterAsClient(clientUserPostModel.registerModel);
            if (userId == null)
            {
                return Ok(false);
            }
            clientUserPostModel.clientDTO.UserId = userId.Value;
            var insertedClientDTO = await _clientsManager.Insert(clientUserPostModel.clientDTO);
            return Ok(true);
        }

        [HttpPost("register-as-receptionist")]
        public async Task<IActionResult> RegisterAsReceptionist([FromBody] ReceptionistUserPostModel receptionistUserPostModel)
        {
            var userId = await _authManager.RegisterAsReceptionist(receptionistUserPostModel.registerModel);
            if (userId == null)
            {
                return Ok(false);
            }
            receptionistUserPostModel.receptionistDTO.UserId = userId.Value;
            var insertedReceptionistDTO = await _receptionistsManager.Insert(receptionistUserPostModel.receptionistDTO);
            return Ok(true);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var result = await _authManager.Login(loginModel);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshModel refreshModel)
        {
            var result = await _authManager.Refresh(refreshModel);
            return Ok(result);
        }

    }
}
