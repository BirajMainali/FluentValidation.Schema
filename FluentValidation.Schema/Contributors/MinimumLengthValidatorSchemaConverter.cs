using FluentValidation.Schema.Abstractions;
using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class MinimumLengthValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "MinimumLengthValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        var minLengthValidator = validator as IMinimumLengthValidator;
        return new FluentSchemaResult
        {
            Type = Name,
            Value = minLengthValidator!.Min,
        };
    }
}