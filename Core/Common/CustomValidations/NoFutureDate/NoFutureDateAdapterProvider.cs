using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace SchedulerManagementSystem.Common.CustomValidations.NoFutureDate
{
    // reference : https://ml-software.ch/posts/extending-client-side-validation-with-dataannotations-and-jquery-unobtrusive-in-an-asp-net-core-application
    public class NoFutureDateAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            try
            {
                if (attribute == null || stringLocalizer == null)
                {
                    // TODO Handle null cases
                }

                if (attribute is NoFutureDateAttribute)
                {
                    return new NoFutureDateAttributeAdapter((NoFutureDateAttribute)attribute, stringLocalizer);
                }
            }
            catch (Exception ex)
            {
                // TODO log
            }

            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
