using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TelecomCRM.Application;
using TelecomCRM.Application.Commands;
using TelecomCRM.Application.Commands.Customer;
using TelecomCRM.Application.DTOs;

namespace TelecomCRM.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateCustomerDTO dto)
        {
            var customerCommand = new AddCustomerCommand()
            {
                Address = dto.Address,
                Email = dto.Email,
                FullName = dto.FullName, 
                Password = dto.Password,    
                PhoneNumber = dto.PhoneNumber,
            };
            var result = await _mediator.Send(customerCommand);

            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.ErrorName });
            }

            return Ok(new { token = result.Value.Token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var loginCommand = new LoginCommand
            {
                Email = dto.Email,
                Password = dto.Password
            };
            var result = await _mediator.Send(loginCommand);

            if (!result.IsSuccess)
            {
                return BadRequest(new { error = result.ErrorName });
            }

            return Ok(new { token = result.Value.Token });
        }
    }

}
