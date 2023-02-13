namespace PM1Reflection;

internal class FurAnimal
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public float Weight { get; set; }

    public float Height { get; set; }

    public DangerLevel DangerLevel { get; set; }

    public CutenessLevel CutenessLevel { get; set; }

    public void Say()
    {
        Console.WriteLine("bla bla bla");
    }

    public override string ToString()
    {
        return $"{Name}, {Description}, {EnumUtils.GetDescription(CutenessLevel)}";
    }
}
