using System;
using System.Collections.Generic;
using KnightsAndCastles.Models;
using KnightsAndCastles.Repositories;

namespace KnightsAndCastles.Services
{
    public class CastlesService
    {
        private readonly CastlesRepository _repo;

        public CastlesService(CastlesRepository repo)
        {
            _repo = repo;
        }

        internal List<Castle> GetAll()
        {
            return _repo.GetAll();
        }

        internal Castle GetById(int castleId)
        {
            Castle foundCastle = _repo.GetById(castleId);
            if (foundCastle == null)
            {
                throw new Exception("Invalid id");
            }
            return foundCastle;
        }

        internal Castle Create(Castle newCastle)
        {
            return _repo.Create(newCastle);
        }

        internal void Delete(int castleId, string userId)
        {
            Castle foundCastle = GetById(castleId);
            if (foundCastle.CreatorId != userId)
            {
                throw new Exception("You are not the owner of this castle");
            }
            _repo.Delete(castleId);
        }
    }
}