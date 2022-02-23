
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using KnightsAndCastles.Models;
using KnightsAndCastles.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnightsAndCastles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CastlesController : ControllerBase
    {
        private readonly CastlesService _cs;

        public CastlesController(CastlesService cs)
        {
            _cs = cs;
        }

        [HttpGet]
        public ActionResult<List<Castle>> GetAll()
        {
            try
            {
                List<Castle> castles = _cs.GetAll();
                return Ok(castles);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{castleId}")]
        public ActionResult<Castle> GetById(int castleId)
        {
            try
            {
                Castle foundCastle = _cs.GetById(castleId);
                return Ok(foundCastle);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Castle>> Create([FromBody] Castle newCastle)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newCastle.CreatorId = userInfo.Id;
                Castle createdCastle = _cs.Create(newCastle);
                return Created($"api/castles/{createdCastle.Id}", createdCastle);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{castleId}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int castleId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _cs.Delete(castleId, userInfo.Id);
                return Ok("Castle successfully deleted");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}