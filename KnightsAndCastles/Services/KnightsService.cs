using System;
using System.Collections.Generic;
using KnightsAndCastles.Repositories;

namespace KnightsAndCastles.Services
{
    public class KnightsService
    {
        private readonly KnightsRepository _repo;

        public KnightsService(KnightsRepository repo)
        {
            _repo = repo;
        }

        internal List<Knight> GetAll()
        {
            return _repo.GetAll();
        }

        internal Knight GetById(int knightId)
        {
            Knight foundKnight = _repo.GetById(knightId);
            if (foundKnight == null)
            {
                throw new Exception("Invalid id");
            }
            return foundKnight;
        }

        internal Knight Create(Knight newKnight)
        {
            return _repo.Create(newKnight);
        }

        internal void Delete(int knightId, string userId)
        {
            Knight foundKnight = GetById(knightId);
            if (foundKnight.CreatorId != userId)
            {
                throw new Exception("Not authorized to delete this knight");
            }
            _repo.Delete(knightId);
        }
    }
}