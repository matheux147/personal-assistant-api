using Microsoft.AspNetCore.Mvc;

namespace PersonalAssistantApi.Application.Common.Interfaces;

public interface IApiResponseHandler
{
    IActionResult Handle(object response);
}
