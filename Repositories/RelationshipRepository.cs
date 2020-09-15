using ChooseEvent2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseEvent2.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private readonly ApplicationDbContext db;

        public RelationshipRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public List<Relationship> GetRelationships()
        {
            return db.Relationships.ToList();
        }
    }
}