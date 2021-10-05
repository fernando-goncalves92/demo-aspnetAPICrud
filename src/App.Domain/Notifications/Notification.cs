using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Notifications
{
    public class Notification
    {
        public string Message { get; }

        public Notification(string message)
        {
            Message = message;
        }
    }
}
