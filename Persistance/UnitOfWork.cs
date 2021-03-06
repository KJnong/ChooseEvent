﻿using ChooseEvent2.Models;
using ChooseEvent2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseEvent2.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext db;

        public IGigRepository gigRepository { get; private set; }
        public IApplicationUserRepository applicationUserRepository { get; private set; }
        public IGenreRepository genreRepository { get; private set; }
        public IRelationshipRepository relationshipRepository { get; private set; }
        public IAttendancesRepository attendancesRepository { get; private set; }
        public INotificationsRepository notificationsRepository { get; private set; }
        public IUserNotificationRepository userNotificationRepository { get; private set; }




        public UnitOfWork(ApplicationDbContext _db)
        {
            db = _db;
            gigRepository = new GigRepository(db);
            applicationUserRepository = new ApplicationUserRepository(db);
            genreRepository = new GenreRepository(db);
            relationshipRepository = new RelationshipRepository(db);
            attendancesRepository = new AttendancesRepository(db);
            notificationsRepository = new NotificationsRepository(db);
            userNotificationRepository = new UserNotificationRepository(db);
        }

        public void Complete()
        {
            db.SaveChanges();
        }

    }
}