using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using KnightsAndCastles.Models;
using KnightsAndCastles.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnightsAndCastles.Controllers
{
    public class KnightsController : ControllerBase
    {
        private readonly KnightsService _ks;
        public KnightsController(KnightsService ks)
        {
            _ks = ks;
        }

        [HttpGet]
        public ActionResult<List<Knight>> GetAll()
        {
            try
            {
                List<Knight> knights = _ks.GetAll();
                return Ok(knights);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{knightId}")]
        public ActionResult<Knight> GetById(int knightId)
        {
            try
            {
                Knight foundKnight = _ks.GetById(knightId);
                return Ok(foundKnight);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Knight>> Create([FromBody] Knight newKnight)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newKnight.CreatorId = userInfo.Id;
                Knight createdKnight = _ks.Create(newKnight);
                return Created($"api/knights/{createdKnight.Id}", createdKnight);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int knightId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _ks.Delete(knightId, userInfo.Id);
                return Ok("Knight was deleted successfully");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}