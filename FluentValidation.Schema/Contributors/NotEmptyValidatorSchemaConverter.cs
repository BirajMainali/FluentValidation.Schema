using FluentValidation.Schema.Abstractions;
using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class NotEmptyValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "NotEmptyValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        return new FluentSchemaResult
        {
            Type = Name
        };
    }
}