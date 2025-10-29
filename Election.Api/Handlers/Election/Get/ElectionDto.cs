namespace Election.Api.Handlers.Election.Get;

public sealed class ElectionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}