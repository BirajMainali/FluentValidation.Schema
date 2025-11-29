using FluentValidation.Schema.Abstractions;
using FluentValidation.Schema.Contributors.Interfaces;
using FluentValidation.Schema.Results;
using FluentValidation.Validators;

namespace FluentValidation.Schema.Console;

// First, dine your own abstraction validator class that inherits from CustomAbstractionValidator
public class YourAbstractionValidator<T> : CustomAbstractionValidator<T>
{
    protected YourAbstractionValidator() : base()
    {
        Converters.Add(new EnumValidatorSchemaConverter());
    }
}

// Then, define a converter for the validation rule that you want to support
// You can add an uncountable number of converters. also can manipulate the existing converters, It's just a list.
// Before adding a new converter, check if there is a built-in converter for the rule you want to support
public class EnumValidatorSchemaConverter : IFluentValidatorConverter
{
    public string Name => "EnumValidator";

    public FluentSchemaResult Convert(IPropertyValidator validator)
    {
        var enumValidator = validator as IEnumValidator;
        return new FluentSchemaResult
        {
            Type = Name,
            Value = enumValidator?.EnumType,
        };
    }
}

// Finally, use your own abstraction validator in your validators
// You should have a validator for each entity that you want to validate
public class CustomerValidator : YourAbstractionValidator<Customer>
{
    public CustomerValidator()
    {
        // Id should be positive
        RuleFor(c => c.Id)
            .GreaterThan(0)
            .WithMessage("Customer ID must be greater than 0.");

        // Surname is required and has a reasonable length
        RuleFor(c => c.Surname)
            .NotEmpty()
            .WithMessage("Surname is required.")
            .MaximumLength(50)
            .WithMessage("Surname cannot exceed 50 characters.");

        // Forename is required and has a reasonable length
        RuleFor(c => c.Forename)
            .NotEmpty()
            .WithMessage("Forename is required.")
            .MaximumLength(50)
            .WithMessage("Forename cannot exceed 50 characters.");

        // Discount should be between 0 and 100
        RuleFor(c => c.Discount)
            .InclusiveBetween(0, 100)
            .WithMessage("Discount must be between 0 and 100.");

        // Address is optional, but if provided, limit the length
        RuleFor(c => c.Address)
            .MaximumLength(200)
            .WithMessage("Address cannot exceed 200 characters.");
    }
}