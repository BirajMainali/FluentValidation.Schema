using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class InclusiveBetweenValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "InclusiveBetweenValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        var inclusiveBetweenValidator = validator as IInclusiveBetweenValidator;
        return new FluentSchemaResult 
        {
            Type = Name,
            From = inclusiveBetweenValidator!.From,
            To = inclusiveBetweenValidator.To,
        };
    }
}