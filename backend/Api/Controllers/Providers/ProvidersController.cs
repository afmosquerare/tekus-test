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
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProviderCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match(
            next => Ok(new { providerId = next }),
            errors => Problem(errors)
        );
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateProviderCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match(
            next => Ok(next),
            errors => Problem(errors)
        );
    }

    [HttpPost("{id}/custom-fields")]
    public async Task<IActionResult> CreateCustomField(Guid id, [FromBody] CreateCustomFieldCommand command)
    {
        command = command with { ProviderId = id };
        var result = await _mediator.Send(command);
        return result.Match(
            next => Ok(next),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id}/custom-fields/{fieldName}")]
    public async Task<IActionResult> RemoveCustomField(Guid id, string fieldName)
    {
        var command = new RemoveCustomFieldCommand(id, fieldName);
        var result = await _mediator.Send(command);
        return result.Match(
            next => Ok(next),
            errors => Problem(errors)
        );
    }

    [HttpPost("{id}/services")]
    public async Task<IActionResult> CreateService(Guid id, [FromBody] CreateServiceCommand command)
    {
        command = command with { ProviderId = id };

        var result = await _mediator.Send(command);
        return result.Match(
            next => Ok(next),
            errors => Problem(errors)
        );
    }

    [HttpPatch("{id}/services/{serviceId}")]
    public async Task<IActionResult> UpdateService(Guid id, int serviceId, [FromBody] UpdateServiceCommand command)
    {
        command = command with { ProviderId = id, ServiceId = serviceId };
        var result = await _mediator.Send(command);
        return result.Match(
            next => Ok(next),
            errors => Problem(errors)
        );
    }

    // [HttpDelete("{id}/services/{serviceId}")]
    // public async Task<IActionResult> RemoveService(Guid id, Guid serviceId)
    // {
    //     var command = new RemoveServiceCommand(id, serviceId);

    //     var result = await _mediator.Send(command);
    //     return result.Match(
    //         updated => Ok(updated),
    //         errors => Problem(errors)
    //     );
    // }
}
