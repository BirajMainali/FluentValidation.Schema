using FluentValidation.Schema.Abstractions;
using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class NotNullValidationSchemaConverter : IFluentValidatorConverter
{
    public string Name => "NotNullValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        return new FluentSchemaResult
        {
            Type = Name
        };
    }
}