using ChooseEvent2.Models;
using ChooseEvent2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseEvent2.Persistance
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext db;

        public GigRepository gigRepository { get; private set; }
        public ApplicationUserRepository applicationUserRepository { get; private set; }
        public GenreRepository genreRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext _db)
        {
            db = _db;
            gigRepository = new GigRepository(db);
            applicationUserRepository = new ApplicationUserRepository(db);
            genreRepository = new GenreRepository(db);
        }

        public void Complete()
        {
            db.SaveChanges();
        }
        
    }
}