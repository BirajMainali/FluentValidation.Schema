// See https://aka.ms/new-console-template for more information

using FluentValidation;
using FluentValidation.Schema.Abstractions;
using Microsoft.Extensions.Hosting;

// Resolve what you need
var validator = new CustomerValidator();
// validator.Validate(new Customer());
Console.WriteLine(validator.GetJsonSchema());

Console.WriteLine("Hello, World!");


public class CustomerValidator : CustomAbstractionValidator<Customer>
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

public class Customer
{
    public int Id { get; set; }
    public string Surname { get; set; }
    public string Forename { get; set; }
    public decimal Discount { get; set; }
    public string Address { get; set; }
}