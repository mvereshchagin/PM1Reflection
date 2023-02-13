using PM1Reflection;
using System.Reflection;

var type = typeof(FurAnimal);

var propNames =
    from prop in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
    select $"{prop.PropertyType} {prop.Name}";

Console.WriteLine(
    propNames.Aggregate((x, y) => $"{x}\r\n{y}")
    );

var animal = new FurAnimal()
{
    Name = "Пушок",
    Description = "очень миленький",
    Weight = 10f,
    Height = 1.5f,
    CutenessLevel = CutenessLevel.Cute,
    DangerLevel = DangerLevel.Low
};

// var type1 = animal.GetType();

var propInfos =
    from prop in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
    let value = prop.GetValue(animal)
    select $"{prop.PropertyType} {prop.Name} = {value}";

Console.WriteLine(
    propInfos.Aggregate((x, y) => $"{x}\r\n{y}")
    );

PropertyInfo propDesc = type.GetProperty("Description") ?? throw new ArgumentException("Invalid property name");
propDesc.SetValue(animal, "Раньше был миленький, а теперь стал не очень");

Console.WriteLine(animal.Description);

Console.WriteLine("\r\nMethods:\r\n");

var methods =
    from method in type.GetMethods()
    let interior = (from parameter in method.GetParameters()
                    select $"{parameter.ParameterType} {parameter.Name}")
                    .DefaultIfEmpty()
                    .Aggregate((x, y) => $"{x}, {y}")
    select $"{method.ReturnType} {method.Name}({interior})";

Console.WriteLine(methods.Aggregate((x, y) => $"{x}\r\n{y}"));

var desc = EnumUtils.GetDescription(animal.CutenessLevel);
Console.WriteLine(desc);

Console.WriteLine(animal);




