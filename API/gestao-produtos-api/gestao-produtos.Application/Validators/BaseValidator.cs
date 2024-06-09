using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace gestao_produtos.Application.Validators
{
    internal class BaseValidator
    {
        private static readonly ConcurrentDictionary<string, Lazy<IValidator>> ValidadorLookUp = new();

        public static ValidationResult Validate<TValidator, TInstance>(TInstance instanceToValidate)
            where TValidator : IValidator
        {
            var context = new ValidationContext<TInstance>(instanceToValidate);
            var validator = CreateOrGetValidatorInstance<TValidator>();
            return validator.Value.Validate(context);
        }

        public static async Task<ValidationResult> ValidateAsync<TValidator, TInstance>(TInstance instanceToValidate)
            where TValidator : IValidator
        {
            var context = new ValidationContext<TInstance>(instanceToValidate);
            var validator = CreateOrGetValidatorInstance<TValidator>();
            return await validator.Value.ValidateAsync(context);
        }

        private static Lazy<IValidator> CreateOrGetValidatorInstance<TValidator>() where TValidator : IValidator
        {
            var validator = new Lazy<IValidator>(() =>
                Activator.CreateInstance<TValidator>(), true);

            return ValidadorLookUp.GetOrAdd(typeof(TValidator).Name, validator);
        }
    }
}
