using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TelecomCRM.Application.Commands;
using TelecomCRM.Application.DTOs;

namespace TelecomCRM.Api.Controllers
{
    [Route("api/[controller]")]
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
                // Например, возвращаем 400 с ошибкой
                return BadRequest(new { error = result.ErrorName });
            }

            return Ok(new { token = result.Value.Token });
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginDTO dto)
        //{
        //    var user = await _userManager.FindByEmailAsync(dto.Email);
        //    if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
        //        return Unauthorized();

        //    return Ok(new { token = _jwtService.GenerateToken(user) });
        //}
    }

}
