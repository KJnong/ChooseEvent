using ChooseEvent2.Models;
using System.Collections.Generic;

namespace ChooseEvent2.Repositories
{
    public interface IUserNotificationRepository
    {
        List<UserNotification> GetUserNotifications(string userId);
    }
}