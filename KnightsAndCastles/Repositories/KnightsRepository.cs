using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using KnightsAndCastles.Models;

namespace KnightsAndCastles.Repositories
{
    public class KnightsRepository
    {
        private readonly IDbConnection _db;

        public KnightsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Knight> GetAll()
        {
            string sql = @"
           SELECT
           k.*,
           a.*
           FROM knights k
           JOIN account a ON a.id =k.creatorId;";
            return _db.Query<Knight, Profile, Knight>(sql, (k, p) =>
            {
                k.Creator = p;
                return k;
            }).ToList();
        }

        internal Knight GetById(int knightId)
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM knights k
            JOIN account a ON a.id = k.creatorId
            WHERE k.id = @knightId;";
            return _db.Query<Knight, Profile, Knight>(sql, (k, p) =>
            {
                k.Creator = p;
                return k;
            }, new { knightId }).FirstOrDefault();
        }


        internal Knight Create(Knight newKnight)
        {
            string sql = @"
           INSERT INTO knights
           (name, creatorId)
           VALUES
           (@Name, @CreatorId);
           SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newKnight);
            newKnight.Id = id;
            return newKnight;
        }

        internal void Delete(int knightId)
        {
            string sql = @"
            DELETE FROM knights
            WHERE id = @knightId
            LIMIT 1";
            _db.Execute(sql, new { knightId });

        }
    }
}