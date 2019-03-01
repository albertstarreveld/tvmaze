using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rtl.Data.Sql;
using Rtl.Domain;

namespace Rtl.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowRepository _showRepository;

        public ShowsController(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        // todo: implement swagger
        // todo: make endpoint discoverable
        // todo: need a waaaaay more flexible way to sort the results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Show>>> Get(int page = 1, int pageSize = 10)
        {
            var result = await _showRepository.FindAll(pageSize, page);

            return Ok(result);
        }
    }
}
