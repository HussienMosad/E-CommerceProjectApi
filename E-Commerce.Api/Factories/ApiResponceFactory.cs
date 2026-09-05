using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens.Experimental;
using Shared.ErrorDtos;

namespace E_Commerce.Api.Factories
{
    public class ApiResponceFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            //context ==> errors , key [Field]
            //context.ModelState ==> string => ModelStateEntry
            //string ==> name of the field
            //ModelStateEntry Errors ==> Error Messages
            //IEnumerable<ValidationError>
            var errors = context.ModelState
                .Where(error => error.Value?.Errors.Any() == true)
                .Select(error => new ValidationErrors()
                {
                    Field = error.Key,
                    Errors = error.Value?.Errors
                        .Select(error => error.ErrorMessage) ?? new List<string>()
                });

            var response = new ValidationErrorResponse()
            {
                Errors = errors,
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "One or more validation error happened"
            };

            return new BadRequestObjectResult(response);
        }
    }
}
