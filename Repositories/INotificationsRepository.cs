using ChooseEvent2.Models;
using System.Collections.Generic;

namespace ChooseEvent2.Repositories
{
    public interface INotificationsRepository
    {
        List<Notification> GetNotifications(string userId);
    }
}