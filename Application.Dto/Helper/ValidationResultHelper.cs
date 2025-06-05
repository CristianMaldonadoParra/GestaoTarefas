using FluentValidation.Results;

namespace Application.Dto.Helper
{
    public static class ValidationResultHelper
    {
        public static ValidationResult NotFound(string propertyName, string message)
        {
            return new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure(propertyName, message)
            });
        }

        public static ValidationResult Required(string propertyName, string message = "Campo obrigatório")
        {
            return new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure(propertyName, message)
            });
        }

        public static ValidationResult InvalidFormat(string propertyName, string message = "Formato inválido")
        {
            return new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure(propertyName, message)
            });
        }

        public static ValidationResult BusinessRule(string propertyName, string message)
        {
            return new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure(propertyName, message)
            });
        }

        public static ValidationResult Multiple(params ValidationFailure[] failures)
        {
            return new ValidationResult(failures);
        }
    }
}
