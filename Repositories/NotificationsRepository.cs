using ChooseEvent2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChooseEvent2.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly ApplicationDbContext db;
        public NotificationsRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public List<Notification> GetNotifications(string userId)
        {
            var notifications = db.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications;
        }
    }
}