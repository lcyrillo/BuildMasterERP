using BuildMaster.Identity.API.Contracts.Users;
using BuildMaster.Identity.Application.Users.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuildMaster.Identity.API.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(
            request.Name,
            request.Email,
            request.Password);

        var result = await _mediator.Send(
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

        return Created(
            $"/api/users/{result.Value}",
            new
            {
                Id = result.Value
            });
    }
}
