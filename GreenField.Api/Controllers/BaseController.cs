using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GreenField.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[JwtAuth]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Single<T>(T response)
        {
            if (response == null)
            {
                return NotFound();
            }
            
            return Ok(response);
        }

        protected IActionResult Collection<T>(List<T> response)
        {
            if (response == null)
            {
                return NotFound();
            }
            
            if (response.Count == 0)
            {
                return Ok(Enumerable.Empty<T>());
            }

            return Ok(response);
        }

        protected async Task<IActionResult> ApiCommandResult(HttpResponseMessage responseMessage)
        {
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.NoContent: 
                    return NoContent();
                case HttpStatusCode.Created:
                    return Created("", await responseMessage.Content.ReadAsStringAsync());
                //TODO Fix this
                case HttpStatusCode.BadRequest:
                    var str = await responseMessage.Content.ReadAsStringAsync();
                    return BadRequest(str);
                case HttpStatusCode.Forbidden:
                    return Forbid(responseMessage.ReasonPhrase);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(responseMessage.ReasonPhrase);
                case HttpStatusCode.OK:
                    return Ok();
                default:
                    return StatusCode(Convert.ToInt32(responseMessage.StatusCode.ToString("D")),
                        responseMessage.ReasonPhrase);
            }
        }
        
        protected bool IsAdmin
            => User.IsInRole("admin");

        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ? 
                Guid.Empty : 
                Guid.Parse(User.Identity.Name);
        
        protected Guid OrganisationId
        {
            get
            {
                var organisationId = User.FindFirst("OrganisationId")?.Value;
                return string.IsNullOrWhiteSpace(organisationId) ? Guid.Empty : Guid.Parse(organisationId);
            }
        }
    }
}