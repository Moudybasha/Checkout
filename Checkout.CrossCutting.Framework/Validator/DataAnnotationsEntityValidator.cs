using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Checkout.CrossCutting.Core.Validator;

namespace Checkout.CrossCutting.Framework.Validator
{
    public class DataAnnotationsEntityValidator
        : IEntityValidator
    {
        #region Private Methods

        private void SetValidatableObjectErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            if (typeof (IValidatableObject).IsAssignableFrom(typeof (TEntity)))
            {
                var validationContext = new ValidationContext(item, null, null);

                IEnumerable<ValidationResult> validationResults = ((IValidatableObject) item).Validate(validationContext);

                errors.AddRange(validationResults.Select(vr => vr.ErrorMessage));
            }
        }

        private void SetValidationAttributeErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            //TypeDescriptor.AddProvider(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(TEntity)),
            //                           typeof(TEntity));

            //var result = from property in TypeDescriptor.GetProperties(item).Cast<PropertyDescriptor>()
            //             from attribute in property.Attributes.OfType<ValidationAttribute>()
            //             where !attribute.IsValid(property.GetValue(item))
            //             select attribute.FormatErrorMessage(string.Empty);

            //if (result != null
            //    &&
            //    result.Any())
            //{
            //    errors.AddRange(result);
            //}

            TypeDescriptor.AddProvider(new AssociatedMetadataTypeTypeDescriptionProvider(typeof (TEntity)),
                typeof (TEntity));
            //bool required =
            //    TypeDescriptor.GetProperties(item).Find("Name", true).Attributes.OfType<RequiredAttribute>()
            //        .Count() > 0;                  

            IEnumerable<string> result = from property in TypeDescriptor.GetProperties(item).Cast<PropertyDescriptor>()
                from attribute in property.Attributes.OfType<ValidationAttribute>()
                where !attribute.IsValid(property.GetValue(item))
                select attribute.FormatErrorMessage(property.DisplayName);

            if (result != null
                &&
                result.Any())
            {
                errors.AddRange(result);
            }
        }

        #endregion

        #region IEntityValidator Members

        public bool IsValid<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
                return false;

            var validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);
            SetValidationAttributeErrors(item, validationErrors);

            return !validationErrors.Any();
        }

        public ICollection<string> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
                return null;

            var validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);
            SetValidationAttributeErrors(item, validationErrors);

            return validationErrors;
        }

        #endregion
    }
}