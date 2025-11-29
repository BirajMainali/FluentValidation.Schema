namespace FluentValidation.Schema.Results;

#nullable disable

public class FluentSchemaResult
{
    public string ErrorMessage { get; set; }
    public string Type { get; set; }
    public object Min { get; set; }
    public object Max { get; set; }
    public object ValueToCompare { get; set; }
    public object Value { get; set; }
    public object From { get; set; }
    public object To { get; set; }
}