using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    public class ApiController : ControllerBase
    {

    }
}
