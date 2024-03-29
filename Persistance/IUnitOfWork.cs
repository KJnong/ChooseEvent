﻿using ChooseEvent2.Repositories;

namespace ChooseEvent2.Persistance
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository applicationUserRepository { get; }
        IGenreRepository genreRepository { get; }
        IGigRepository gigRepository { get; }
        IRelationshipRepository relationshipRepository { get; }
        IAttendancesRepository attendancesRepository { get; }
        INotificationsRepository notificationsRepository { get; }
        IUserNotificationRepository userNotificationRepository { get; }

        void Complete();
    }
}