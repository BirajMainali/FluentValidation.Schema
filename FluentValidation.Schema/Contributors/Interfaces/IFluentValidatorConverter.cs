using FluentValidation.Schema.Abstractions;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Contributors.Interfaces;

public interface IFluentValidatorConverter
{
    string Name { get; }
    FluentSchemaResult Convert(IPropertyValidator validator);
}