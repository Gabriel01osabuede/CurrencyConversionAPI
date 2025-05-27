using CurrencyConvertion.Enums;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConvertion.ViewModels
{
    public class ResultModel<T>
    {
        public ResultModel()
        {

        }
        public List<ValidationResult> ValidationErrors { get; set; } = new List<ValidationResult>();

        public List<string> ErrorMessages
        {
            get
            {
                return ValidationErrors.Select(c => c.ErrorMessage).ToList();
            }
        }

        public string Message { get; set; }

        public int TotalCount { get; set; }
        public ApiResponseCodes ApiCode { get; set; }

        public T Data { get; set; } = default;

        public string this[string columnName]
        {
            get
            {
                var validatioResult = ValidationErrors.FirstOrDefault(r => r.MemberNames.FirstOrDefault() == columnName);
                return validatioResult == null ? string.Empty : validatioResult.ErrorMessage;
            }
        }

        public bool HasError
        {
            get
            {
                if (ValidationErrors.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public object ErrorMessage { get; set; }

        public void AddError(string error)
        {
            ValidationErrors.Add(new ValidationResult(error));
        }

        public void AddError(ValidationResult validationResult)
        {
            ValidationErrors.Add(validationResult);
        }

        public void AddError(IEnumerable<ValidationResult> validationResults)
        {
            ValidationErrors.AddRange(validationResults);
        }

    }
}
