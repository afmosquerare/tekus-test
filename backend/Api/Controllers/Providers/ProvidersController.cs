using Application.Commands.Providers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[Route("providers")]
public class ProvidersController : ApiController
{
    
    private readonly ISender _mediator;

    public ProvidersController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException( nameof( mediator ) );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProviderCommand command)
    {
        var result = await _mediator.Send( command );
        return result.Match(
            Id => Ok( new { providerId = Id }),
            errors => Problem( errors )
        );
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateProviderCommand command)
    {
        var result = await _mediator.Send( command );
        return result.Match(
            Id => Ok( new { providerId = Id }),
            errors => Problem( errors )
        );
    }
}