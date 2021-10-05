using App.Domain.Notifications.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace App.Domain.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }
    }
}
