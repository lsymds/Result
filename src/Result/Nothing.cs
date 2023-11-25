namespace LSymds.Result;

/// <summary>
/// Represents nothing, as C# doesn't have a built in nothing type.
/// </summary>
public class Nothing
{
    private Nothing()
    {
    }

    /// <summary>
    /// Gets the singleton instance of the <see cref="Nothing"/> class.
    /// </summary>
    public static Nothing Instance { get; } = new Nothing();
}