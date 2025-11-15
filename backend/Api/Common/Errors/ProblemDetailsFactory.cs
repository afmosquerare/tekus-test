using System.Diagnostics;
using Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Common.Errors;

public class AppProblemDetailsFactory(ApiBehaviorOptions options) : ProblemDetailsFactory
{

    private readonly ApiBehaviorOptions _options = options ?? throw new ArgumentNullException(nameof(options));

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
         int? statusCode = null, 
         string? title = null, 
         string? type = null, 
         string? detail = null, 
         string? instance = null)
    {
        statusCode ??= 500;
        var problemaDetail = new ProblemDetails
        {
          Status = statusCode, 
          Title = title,
          Type = type,
          Detail = detail,
          Instance = detail,  
        };
        ApplyProblemDetailsDefaults( httpContext, problemaDetail, statusCode.Value );
        return problemaDetail;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext, 
        ModelStateDictionary modelStateDictionary, 
        int? statusCode = null, 
        string? title = null, 
        string? type = null, 
        string? detail = null, 
        string? instance = null)
    {
        if(modelStateDictionary is null) throw new ArgumentNullException( nameof(modelStateDictionary) );
        statusCode??=400;

        var validationProblemDetails = new ValidationProblemDetails
        {
            Detail = detail,
            Status = statusCode,
            Type = type,
            Instance = instance,
        };
        
        if( title is not null )
        {
            validationProblemDetails.Title = title;
        }
        ApplyProblemDetailsDefaults(httpContext, validationProblemDetails, statusCode.Value );

        return validationProblemDetails;
    }

    private void ApplyProblemDetailsDefaults( HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;
        if( _options.ClientErrorMapping.TryGetValue( statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        if( traceId is not null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }
        var errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;
        if(errors is not null)
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select( e=> e.Code) );
        }
    }
}