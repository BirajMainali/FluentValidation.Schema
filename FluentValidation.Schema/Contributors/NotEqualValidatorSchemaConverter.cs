using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class NotEqualValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "NotEqualValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        var comparisonValidator = validator as IComparisonValidator;
        return new FluentSchemaResult
        {
            Type = Name,
            ValueToCompare = comparisonValidator!.ValueToCompare
        };
    }
}