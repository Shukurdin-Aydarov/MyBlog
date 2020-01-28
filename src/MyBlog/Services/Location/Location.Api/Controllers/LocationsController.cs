using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Location.Api.Models;
using Location.Api.Infrastructure.Services;

namespace Location.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService locationService;
        private readonly IIdentityService identityService;

        public LocationsController(ILocationsService locationService, IIdentityService identityService)
        {
            this.locationService = locationService;
            this.identityService = identityService;
        }

        [HttpGet, Route("user/{userId:guid}")]
        [ProducesResponseType(typeof(UserLocation), (int)HttpStatusCode.OK)]
        public async ValueTask<ActionResult<UserLocation>> GetUserLocationAsync(Guid userId)
        {
            return await locationService.GetUserLocationAsync(userId.ToString());
        }

        [HttpGet, Route("")]
        [ProducesResponseType(typeof(IList<Locations>), (int)HttpStatusCode.OK)]
        public async ValueTask<ActionResult<IList<Locations>>> GetAllLocationsAsync()
        {
            return (await locationService.GetAllLocationsAsync()).ToList();
        }

        [HttpGet, Route("{locationId:int}")]
        [ProducesResponseType(typeof(Locations), (int)HttpStatusCode.OK)]
        public async ValueTask<ActionResult<Locations>> GetLocationAsync(int locationId)
        {
            return await locationService.FindLocationAsync(locationId);
        }

        [HttpPost, Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOrUpdateUserLocationAsync([FromBody] Point newUserLocation)
        {
            var userId = identityService.GetUserIdentity();
            var result = await locationService.AddOrUpdateUserLocationAsync(userId, newUserLocation);

            if (!result) return BadRequest();

            return Ok();
        }
    }
}
