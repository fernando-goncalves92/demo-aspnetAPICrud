﻿using App.Domain.Notifications;
using App.Domain.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace App.Api.Controllers.Base
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        
        protected MainController(INotifier notifier)
        {
            _notifier = notifier;           
        }

        protected bool IsValidOperation()
        {
            return !_notifier.HasNotifications();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new { success = true, data = result });
            }

            return BadRequest(new { success = false, errors = _notifier.GetNotifications().Select(n => n.Message) });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                NotifyErrorModelStateInvalid(modelState);
            }
            
            return CustomResponse();
        }

        protected void NotifyErrorModelStateInvalid(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                var message = error.Exception == null ? error.ErrorMessage : error.Exception.Message;

                NotifyError(message);
            }
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}
