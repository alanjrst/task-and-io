using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using task.io.authentication.Application.DTOs.Users;

namespace task.io.authentication.api.Controller
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger) {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequestLogin userRequestLogin) {
            var result = await _userService.CheckLoginAsync(userRequestLogin);
            if (result.Valid)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> changePassword([FromBody] UserRequestChangePassword userRequestChangePassword)
        {
            var result = await _userService.ChangePasswordAsync(userRequestChangePassword);
            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] UserRequest userRequest) {
            try
            {
                return Ok(await _userService.AddNewAsync(userRequest));
            }
            catch (Exception exception)
            {
                _logger.LogError("Error creating user", exception);
                return BadRequest();
            }
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> Put([FromRoute] int Id,[FromBody] UserRequest userRequest)
        {
            try
            {
                if (Id != userRequest.Id)
                    return BadRequest();

                return Ok(await _userService.EditAsync(userRequest));
            }
            catch (Exception exception)
            {
                _logger.LogError("Error in update user", exception);
                return BadRequest();
            }
        }

        [HttpPost("requestResetPassword")]
        public async Task<IActionResult> RequestResetPassword([FromBody] UserRequest userRequest)
        {
            try
            {
                bool result = await _userService.RequestResetPassword(userRequest);
                if (result)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception exception)
            {
                _logger.LogError("Error creating user", exception);
                return BadRequest();
            }
        }

    }
}