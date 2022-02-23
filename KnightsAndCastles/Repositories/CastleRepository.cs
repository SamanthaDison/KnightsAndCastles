using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using KnightsAndCastles.Models;

namespace KnightsAndCastles.Repositories
{
    public class CastlesRepository
    {
        private readonly IDbConnection _db;

        public CastlesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Castle> GetAll()
        {
            string sql = @"
            SELECT
            c.*,
            a.*
            FROM castles c
            JOIN accounts a ON a.id = c.creatorId;";
            return _db.Query<Castle, Profile, Castle>(sql, (c, p) =>
            {
                c.Creator = p;
                return c;
            }).ToList();
        }


        internal Castle GetById(int castleId)
        {
            string sql = @"
            SELECT
            c.*,
            a.*
            FROM castles c
            JOIN accounts a ON a.id = c.creatorId
            WHERE id=@castleId;";
            return _db.Query<Castle, Profile, Castle>(sql, (c, p) =>
            {
                c.Creator = p;
                return c;
            }, new { castleId }).FirstOrDefault();
        }


        internal Castle Create(Castle newCastle)
        {
            string sql = @"
           INSERT INTO castles
           (name, location, creatorId)
           VALUES
           (@Name, @Location, @CreatorId);
           SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newCastle);
            newCastle.Id = id;
            return newCastle;
        }
        internal void Delete(int castleId)
        {
            string sql = @"
            DELETE FROM castles
            WHERE id =@castleId
            LIMIT 1";
            _db.Execute(sql, new { castleId });
        }
    }
}