using FluentValidation.Schema.Abstractions;
using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class LengthValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "LengthValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        var lengthValidator = validator as ILengthValidator;
        return new FluentSchemaResult
        {
            Type = Name,
            Min = lengthValidator!.Min,
            Max = lengthValidator.Max,
        };
    }
}