
using System;
using KnightsAndCastles.Interfaces;

namespace KnightsAndCastles.Models
{
    public class Profile : IRepoItem<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime UpdatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}