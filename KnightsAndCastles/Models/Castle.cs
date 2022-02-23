
using System;
using KnightsAndCastles.Interfaces;

namespace KnightsAndCastles.Models
{
    public class Castle : IRepoItem<int>, ICreated
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}