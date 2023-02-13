namespace PM1Reflection;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
internal class CuteLevelAttribute : Attribute
{
    public int Level { get; set; }

    public CuteLevelAttribute(int level)
    {
        Level = level;
    }
}
