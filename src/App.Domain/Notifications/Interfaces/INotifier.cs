using System.Collections.Generic;

namespace App.Domain.Notifications.Interfaces
{
    public interface INotifier
    {
        bool HasNotifications();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
