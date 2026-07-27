using E_Commerce_Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public static ActionResult<T> ToActionResult<T>(Result<T> result)
        {

            // success
            if (result.IsSuccess)
            {

                return new OkObjectResult(result.data);
            }
            else
            {
                //fail
                return ToProblem(result.Errors);
            }

        }
        public static ActionResult ToActionResult(Result result)
        {
            if (result.IsSuccess)
                return new OkResult();
            else
                return ToProblem(result.Errors);
        }






        protected static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {

            var firsterror = errors[0];
            var statuscode = firsterror.ErrorType switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };


            var problem = new ProblemDetails()
            {

                Status = statuscode,
                Title = firsterror.code,
                Detail = firsterror.Description,
                Extensions = { ["errors"] = errors }
            };
            return new ObjectResult(problem);












        }



    }
}
