using BuildMaster.Identity.API.Contracts.Authentication;
using BuildMaster.Identity.Application.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildMaster.Identity.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(
            request.Email,
            request.Password);

        var result =
            await _mediator.Send(
                command,
                cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                Code = result.Error.Code,
                Message = result.Error.Description
            });
        }

        return Ok(new
            {
                AccessToken = result.Value
            }
        );
    }
}
