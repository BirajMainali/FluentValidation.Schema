using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors;

public class LessThanOrEqualValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "LessThanOrEqualValidator";

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