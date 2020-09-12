using ChooseEvent2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseEvent2.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext db;
        public GenreRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IEnumerable<Genre> Genres()
        {
            return db.Genres.ToList();
        }
    }
}