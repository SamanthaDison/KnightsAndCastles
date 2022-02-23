
using KnightsAndCastles.Models;

namespace KnightsAndCastles.Interfaces
{
    public interface ICreated
    {
        string CreatorId { get; set; }
        Profile Creator { get; set; }
    }
}