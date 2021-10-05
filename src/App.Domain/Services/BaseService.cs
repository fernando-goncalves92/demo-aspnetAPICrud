using App.Domain.Entities;
using App.Domain.Notifications;
using App.Domain.Notifications.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace App.Domain.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)            
                Notify(error.ErrorMessage);
        }

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool RunValidator<TValidator, TEntity>(TValidator validator, TEntity entity) 
            where TValidator : AbstractValidator<TEntity> 
            where TEntity : Entity
        {
            var validationResult = validator.Validate(entity);
            if (validationResult.IsValid) 
                return true;

            Notify(validationResult);

            return false;
        }
    }
}
