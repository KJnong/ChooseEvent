using ChooseEvent2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseEvent2.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly ApplicationDbContext db;
        public UserNotificationRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public List<UserNotification> GetUserNotifications(string userId)
        {
            var userNotifications = db.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();

            return userNotifications;
        }
    }
}