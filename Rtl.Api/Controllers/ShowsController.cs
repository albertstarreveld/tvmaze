using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rtl.Data.Sql;
using Rtl.Domain;
using Rtl.Services;

namespace Rtl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly ITvGuideService _tvGuideService;

        public ShowsController(ITvGuideService tvGuideService)
        {
            _tvGuideService = tvGuideService;
        }
        
        // todo: need a more flexible way to sort the results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Show>>> Get(int page = 1, int pageSize = 10)
        {
            return Ok(await _tvGuideService.ListShows(page, pageSize));
        }
    }
}
