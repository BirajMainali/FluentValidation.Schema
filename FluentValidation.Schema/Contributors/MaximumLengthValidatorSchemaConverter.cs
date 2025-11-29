using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class MaximumLengthValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "MaximumLengthValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        var minLengthValidator = validator as IMaximumLengthValidator;
        return new FluentSchemaResult
        {
            Type = Name,
            Value = minLengthValidator!.Max,
        };
    }
}