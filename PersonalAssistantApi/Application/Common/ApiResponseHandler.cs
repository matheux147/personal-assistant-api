using Microsoft.AspNetCore.Mvc;
using PersonalAssistantApi.Application.Common.Interfaces;
using PersonalAssistantApi.Controllers;
using System.Collections;

namespace PersonalAssistantApi.Application.Common;

public class ApiResponseHandler : IApiResponseHandler
{
    public IActionResult Handle(object response)
    {
        if (response == null) return new NoContentResult();

        var type = response.GetType();

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Result<>))
        {
            return HandleResultPattern(response, type);
        }

        return new OkObjectResult(new { Message = response.ToString() });
    }

    private static IActionResult HandleResultPattern(object response, Type type)
    {
        var succeeded = (bool)type.GetProperty("Succeeded")!.GetValue(response)!;
        var error = (string)type.GetProperty("Error")!.GetValue(response)!;
        var data = type.GetProperty("Data")!.GetValue(response);

        if (!succeeded)
        {
            return new BadRequestObjectResult(new BadRequestResponseDto { Error = error });
        }

        if (data == null)
        {
            return new NoContentResult();
        }

        if (data is Guid id)
        {
            return new ObjectResult(new CreatedResponseDto
            {
                Id = id,
                Message = "Recurso criado com sucesso."
            });
        }

        if (data is IEnumerable)
        {
            return new OkObjectResult(data);
        }

        return new OkObjectResult(data);
    }
}
