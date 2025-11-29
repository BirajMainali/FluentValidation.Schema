using System.Linq.Expressions;
using System.Text.Json;
using FluentValidation.Schema.Contributors;
using FluentValidation.Schema.Contributors.Interfaces;

namespace FluentValidation.Schema.Abstractions;

/// <summary>
/// Base validator abstraction that extends <see cref="AbstractValidator{T}"/> to provide
/// an in-memory representation of validation rules and a mechanism to convert them
/// into a JSON schema for client-side validation.
/// </summary>
/// <typeparam name="T">The type being validated.</typeparam>
public class CustomAbstractionValidator<T> : AbstractValidator<T>
{
    /// <summary>
    /// Collection of converters used to transform FluentValidation rules into schema results.
    /// </summary>
    private readonly List<IFluentValidatorConverter> _converters = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAbstractionValidator{T}"/> class
    /// and registers default converters.
    /// </summary>
    protected CustomAbstractionValidator()
    {
        _converters.AddRange(new IFluentValidatorConverter[]
        {
            new EmailValidatorSchemaConverter(),
            new ExactLengthValidatorSchemaConverter(),
            new InclusiveBetweenValidatorSchemaConverter(),
            new LengthValidatorSchemaConverter(),
            new MaximumLengthValidatorSchemaConverter(),
            new MinimumLengthValidatorSchemaConverter(),
            new NotEmptyValidatorSchemaConverter(),
            new NotNullValidationSchemaConverter(),
            new RegularExpressionValidatorSchemaConverter(),
            new EqualValidatorSchemaConverter(),
            new NotEqualValidatorSchemaConverter(),
            new LessThanValidatorSchemaConverter(),
            new GreaterThanValidatorSchemaConverter(),
            new GreaterThanOrEqualValidatorSchemaConverter(),
            new LessThanOrEqualValidatorSchemaConverter()
        });
    }

    /// <summary>
    /// Holds all validation rules defined for this validator.
    /// </summary>
    protected virtual List<IValidationRule<T>> Rules { get; set; } = new();

    /// <summary>
    /// Stores the converted validation schema for all properties.
    /// Key = property name, Value = list of validation results.
    /// </summary>
    protected virtual Dictionary<string, List<object>> Schema { get; set; } = new();

    /// <summary>
    /// Captures every added rule in memory.
    /// </summary>
    /// <param name="rule">The rule being added.</param>
    protected override void OnRuleAdded(IValidationRule<T> rule)
    {
        Rules.Add(rule);
        base.OnRuleAdded(rule);
    }

    /// <summary>
    /// Converts the in-memory rules into a JSON schema that can be used for client-side validation.
    /// </summary>
    /// <param name="writeIndented">If true, outputs formatted JSON; otherwise, minified JSON.</param>
    /// <returns>A JSON string representing the validation schema.</returns>
    public string GetJsonSchema(bool writeIndented = true)
    {
        ConvertToSchema();
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = writeIndented,
        };
        return JsonSerializer.Serialize(Schema, options);
    }

    /// <summary>
    /// Resolves the property name for a given rule. 
    /// Override to customize property naming conventions.
    /// </summary>
    /// <param name="rule">The validation rule.</param>
    /// <returns>The property name associated with the rule.</returns>
    protected virtual string GetPropertyName(IValidationRule<T> rule)
    {
        return rule.Expression?.Body switch
        {
            MemberExpression m => m.Member.Name,
            _ => rule.PropertyName
        };
    }

    /// <summary>
    /// Normalizes the validator type name by removing the "Validator" suffix.
    /// Override to apply custom naming conventions.
    /// </summary>
    /// <param name="name">The original validator type name.</param>
    /// <returns>The normalized name.</returns>
    protected virtual string GetNormalizedValidationTypeName(string name)
        => string.IsNullOrEmpty(name) ? name : name.Replace("Validator", string.Empty);

    /// <summary>
    /// Converts all defined validation rules into a schema format stored in <see cref="Schema"/>.
    /// Each rule is passed through its corresponding converter, and the resulting schema includes
    /// the validation type and error messages.
    /// </summary>
    protected virtual void ConvertToSchema()
    {
        foreach (var rule in Rules)
        {
            var propertyName = GetPropertyName(rule);
            if (string.IsNullOrEmpty(propertyName) || Schema.ContainsKey(propertyName)) continue;

            foreach (var component in rule.Components)
            {
                var converter = _converters.FirstOrDefault(x => x.Name == component.Validator.Name);

                if (converter == null)
                {
                    throw new NotImplementedException("You need to add converter for " + component.Validator.Name);
                }

                var result = converter.Convert(component.Validator);

                result.Type = GetNormalizedValidationTypeName(result.Type);
                result.ErrorMessage = component.GetUnformattedErrorMessage().Replace("{PropertyName}", propertyName);

                if (!Schema.TryGetValue(propertyName, out var list))
                {
                    list = new();
                    Schema[propertyName] = list;
                }

                list.Add(result);
            }
        }
    }
}