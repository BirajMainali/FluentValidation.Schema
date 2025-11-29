# FluentValidation.Schema

**FluentValidation.Schema** generates JSON schemas from FluentValidation rules, ensuring consistent validation on both client- and server-side. Highly customizable and extensible, it bridges the gap between backend and frontend validation logic. For more on FluentValidation, see [FluentValidation.net](https://fluentvalidation.net/).

Source Code: https://github.com/BirajMainali/FluentValidation.Schema/tree/master/FluentValidation.Schema

Example: https://github.com/BirajMainali/FluentValidation.Schema/tree/master/FluentValidation.Schema.Console

---

## Features

* Converts FluentValidation rules to JSON Schema automatically
* Supports a wide range of validators out-of-the-box
* Ensures consistent input validation across client and server
* Extensible for custom converters
* Reduces duplication and validation errors

---

## Installation

Using .NET CLI:

```bash
dotnet add package Extensions.FluentValidation.Schema
```

Or via NuGet Package Manager:

```
Install-Package Extensions.FluentValidation.Schema
```

---

## Usage

### Define a Validator

```csharp
public class CustomerValidator : CustomAbstractionValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0)
            .WithMessage("Customer ID must be greater than 0.");

        RuleFor(c => c.Surname)
            .NotEmpty()
            .WithMessage("Surname is required.")
            .MaximumLength(50)
            .WithMessage("Surname cannot exceed 50 characters.");

        RuleFor(c => c.Forename)
            .NotEmpty()
            .WithMessage("Forename is required.")
            .MaximumLength(50)
            .WithMessage("Forename cannot exceed 50 characters.");

        RuleFor(c => c.Discount)
            .InclusiveBetween(0, 100)
            .WithMessage("Discount must be between 0 and 100.");

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
```

### Generate JSON Schema

```csharp
var validator = new CustomerValidator();
Console.WriteLine(validator.GetJsonSchema());
```

#### Example Output

```json
{
  "Id": [
    {
      "ErrorMessage": "Customer ID must be greater than 0.",
      "Type": "GreaterThan",
      "ValueToCompare": 0
    }
  ],
  "Surname": [
    {
      "ErrorMessage": "Surname is required.",
      "Type": "NotEmpty"
    },
    {
      "ErrorMessage": "Surname cannot exceed 50 characters.",
      "Type": "MaximumLength",
      "Value": 50
    }
  ],
  "Forename": [
    {
      "ErrorMessage": "Forename is required.",
      "Type": "NotEmpty"
    },
    {
      "ErrorMessage": "Forename cannot exceed 50 characters.",
      "Type": "MaximumLength",
      "Value": 50
    }
  ],
  "Discount": [
    {
      "ErrorMessage": "Discount must be between 0 and 100.",
      "Type": "InclusiveBetween",
      "From": 0,
      "To": 100
    }
  ],
  "Address": [
    {
      "ErrorMessage": "Address cannot exceed 200 characters.",
      "Type": "MaximumLength",
      "Value": 200
    }
  ]
}

```

---

## Supported Converters

The library supports these FluentValidation converters out-of-the-box:

* EmailValidatorSchemaConverter
* ExactLengthValidatorSchemaConverter
* InclusiveBetweenValidatorSchemaConverter
* LengthValidatorSchemaConverter
* MaximumLengthValidatorSchemaConverter
* MinimumLengthValidatorSchemaConverter
* NotEmptyValidatorSchemaConverter
* NotNullValidationSchemaConverter
* RegularExpressionValidatorSchemaConverter
* EqualValidatorSchemaConverter
* NotEqualValidatorSchemaConverter
* LessThanValidatorSchemaConverter
* GreaterThanValidatorSchemaConverter
* GreaterThanOrEqualValidatorSchemaConverter
* LessThanOrEqualValidatorSchemaConverter

**Important:** Supported converters **only convert rules to JSON schema**. They do **not execute validation** at runtime. They are purely for generating a consistent representation of your FluentValidation rules for use in clients, documentation, or API consumers.

Custom converters can also be added for more complex scenarios. Example implementation can be found [here](https://github.com/BirajMainali/FluentValidation.Schema/blob/master/FluentValidation.Schema.Console/YourAbstractionValidator.cs).

---

## Contributing

Contributions are welcome! You can:

* Add new converters for additional FluentValidation rules
* Improve existing schema generation
* Report bugs or request features

---

## License

MIT License — free to use, modify, and distribute.
