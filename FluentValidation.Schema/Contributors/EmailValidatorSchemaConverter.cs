using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class EmailValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "EmailValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        return new FluentSchemaResult()
        {
            Type = Name,
        };
    }
}