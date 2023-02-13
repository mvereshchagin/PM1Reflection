using System.ComponentModel;

namespace PM1Reflection;

internal enum CutenessLevel
{
    [Description("Уродливый"), CuteLevel(10)] Ugly,
    [Description("Обычный"), CuteLevel(30)] Usual,
    [Description("Милый")] Cute,
    [Description("Очень милый")] VeryCute
}
