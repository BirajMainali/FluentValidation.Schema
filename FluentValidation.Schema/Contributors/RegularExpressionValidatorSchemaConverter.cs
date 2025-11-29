using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class RegularExpressionValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "RegularExpressionValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        var expressionValidator = validator as IRegularExpressionValidator;
        return new FluentSchemaResult
        {
            Type = Name,
            Value = expressionValidator!.Expression,
        };
    }
}