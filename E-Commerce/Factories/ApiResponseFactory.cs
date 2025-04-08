using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext actionContext)
        {
            // Get all errors in model state
            var errors = actionContext.ModelState.Where(e => e.Value.Errors.Any())
                .Select(e => new ValidationError
                {
                    Filed = e.Key,
                    Errors = e.Value.Errors.Select(e => e.ErrorMessage)
                });
            // Create Custom Response
            var response = new ValidationErrorResponse
            {
                StatesCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "Validation Failed",
                ValidationErrors = errors
            };
            // Return
            return new BadRequestObjectResult(response);
        }
    }
}
