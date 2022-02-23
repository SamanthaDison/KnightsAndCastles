
using System;
using KnightsAndCastles.Interfaces;
using KnightsAndCastles.Models;

namespace KnightsAndCastles
{
    public class Knight : IRepoItem<int>, ICreated
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CastleId { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}