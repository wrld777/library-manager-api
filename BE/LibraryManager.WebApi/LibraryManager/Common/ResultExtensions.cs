using LibraryManager.ApplicationCore.ResultPattern;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace LibraryManager.Common
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller)
        {
            if (result.IsSuccess)
                return controller.Ok(result.Value);

            return ToProblem(result.Error!, controller);
        }

        public static IActionResult ToNoContentResult<T>(this Result result, ControllerBase controller)
        {
            if (result.IsSuccess)
                return controller.NoContent();

            return ToProblem(result.Error!, controller);
        }


        //Metido statico per la POST 

        public static IActionResult ToCreatedResult<T>(this Result<T> result, ControllerBase controller, string actionName, Func<T,Object> routeValues)
        {
            if (result.IsSuccess)
                return controller.CreatedAtAction(actionName, routeValues(result.Value!), result.Value);

            return ToProblem(result.Error!, controller);
        }








        //metodo per mappare da categoria del errore al codice
        private static IActionResult ToProblem(Error error, ControllerBase controller)
        {
            var status = error.Type switch
            {
                ErrorType.Validation    => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized  => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden     => StatusCodes.Status403Forbidden,
                ErrorType.NotFound      => StatusCodes.Status404NotFound,
                ErrorType.Conflict      => StatusCodes.Status409Conflict,
                _                       => StatusCodes.Status500InternalServerError
            };

            return controller.Problem(title: error.code, detail: error.message, statusCode: status);
        }
    }
}
