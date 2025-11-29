using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class ExactLengthValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "ExactLengthValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        var exactLengthValidator = validator as IExactLengthValidator;
        return new FluentSchemaResult()
        {
            Type = Name,
            Value = Math.Max(exactLengthValidator!.Min, exactLengthValidator.Max),
        };
    }
}