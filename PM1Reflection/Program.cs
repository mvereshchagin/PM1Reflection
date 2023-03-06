using PM1Reflection;
using System.Reflection;

#region Attributes
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
#endregion

#region Create by name
var item = MetadataUtils.CreateInstance<FurAnimal>();
Console.WriteLine($"Output object: {item}");

var item2 = MetadataUtils.CreateInstance("PM1Reflection.FurAnimal");
if (item2 is not null)
    Console.WriteLine(item2.GetType());


var item3 = MetadataUtils.CreateInstance2(
    assemblyPath: @"C:\Users\MVereshchagin\source\repos\BigText\BigText\bin\Debug\net6.0\ExtendedConsole.dll",
    className: "ExtendedConsole.BigTextPrinter", args: new object?[] { ConsoleColor.Magenta, ConsoleColor.Black, '&' } );

if (item3 is null)
{
    Console.WriteLine("Item has not been loaded");
    return;
}

MetadataUtils.ExecMethod(item3, "WriteLine", new object?[] { "баба" });


#endregion

#region using INotifyPropertyChanged
var newAnimal = new FurAnimal()
{
    Name = "Хорек Борька",
    CutenessLevel = CutenessLevel.VeryCute,
    DangerLevel = DangerLevel.Low,
    Description = "Очень добрый",
    Height = 0.56f,
    Weight = 2,
};

newAnimal.PropertyChanged += (sender, e) =>
{
    Console.WriteLine($"Property '{e.PropertyName}' has changed");
};

newAnimal.PropertyChangingEx += (sender, e) =>
{
    if(e.PropertyName == "Name")
    {
        if (e.NewValue == "Борька11")
        {
            e.Cancel = true;
            return;
        }

        Console.WriteLine("Property Name has changed");
    }
};

newAnimal.Weight = 3;
newAnimal.Height = 0.58f;

newAnimal.Name = "Борька";
Console.WriteLine(newAnimal.Name);
#endregion





