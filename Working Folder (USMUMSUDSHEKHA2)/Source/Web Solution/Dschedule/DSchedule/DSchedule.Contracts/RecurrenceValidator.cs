using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DSchedule.Contracts
{
    public class RecurrenceValidator : ValidationAttribute,IClientValidatable
    {
        string otherPropertyName;

        public RecurrenceValidator(string otherPropertyName, string errorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
        }
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    var property = validationContext.ObjectInstance.Equals("IsRecurrence");//.ObjectType.GetProperty("IsRecurrence");
        //    if (property && (value == null))
        //    {
        //        return new ValidationResult("Please select the Recurrence Pattern.");
        //    }
        //    else
        //    {
        //        return ValidationResult.Success;
        //    }
        //}
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                var property = validationContext.ObjectInstance.Equals(this.otherPropertyName);
                if (property && value == null)
                {
                validationResult = new ValidationResult(ErrorMessageString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return validationResult;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string errorMessage = ErrorMessageString;
            ModelClientValidationRule rule = new ModelClientValidationRule();
            rule.ErrorMessage = errorMessage;
            rule.ValidationType = "recurrencepatternrequired"; 
            rule.ValidationParameters.Add("otherpropertyname", otherPropertyName);
             yield return rule;
        }
       
    }
}
