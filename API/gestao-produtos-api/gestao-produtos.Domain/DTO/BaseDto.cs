using FluentValidation.Results;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gestao_produtos.Domain.DTO
{
    public abstract class BaseDto
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; } = new();

        [JsonIgnore]
        public bool IsValid =>
            ValidationResult.IsValid;

        public abstract Task ValidateAsync();
    }
}
