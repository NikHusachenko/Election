using Election.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Election.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult MapResponse(ServiceResponse response) => response.IsError ? AsBadRequest(response) : AsOk(response);
    protected IActionResult MapResponse<T>(ServiceResponse<T> response) => response.IsError ? AsBadRequest(response) : AsOk(response);
    protected IActionResult MapResponse<T>(IEnumerable<T> response) => response.Any() ? Ok(response) : NoContent();

    protected IActionResult AsOk(ServiceResponse response) => NoContent();
    protected IActionResult AsOk<T>(ServiceResponse<T> response) => Ok(response.Value);

    protected IActionResult AsBadRequest(ServiceResponse response) => BadRequest(new { errorMessage = response.ErrorMessage });
    protected IActionResult AsBadRequest<T>(ServiceResponse<T> response) => BadRequest(new { errorMessage = response.ErrorMessage });
}