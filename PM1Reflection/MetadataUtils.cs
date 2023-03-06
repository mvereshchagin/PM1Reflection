using System.Reflection;
using System.Runtime.Loader;

namespace PM1Reflection;

internal static class MetadataUtils
{
    public static T? CreateInstance<T>(params object?[] args) where T : class
    {
        try
        {
            var obj = Activator.CreateInstance(typeof(T), args);
            if (obj is T item)
                return item;
        }
        catch
        { }

        return null;
    }

    public static object? CreateInstance(string typeName, params object?[] args)
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();

            var handle = Activator.CreateInstance(assembly.FullName, typeName, args);
            if (handle is not null)
                return handle.Unwrap();
        }
        catch
        { }

        return null;
    }

    public static object? CreateInstance2(string className, object[]? args = null, string? assemblyPath = null)
    {
        assemblyPath ??= Assembly.GetEntryAssembly()?.Location;

        if (assemblyPath is null)
            return null;

        AssemblyLoadContext? loadContext = null;
        try
        {
            // Create a new context and mark it as 'collectible'.
            var tempLoadContextName = Guid.NewGuid().ToString();

            loadContext = new AssemblyLoadContext(tempLoadContextName, true);
            Assembly assembly = loadContext.LoadFromAssemblyPath(assemblyPath);

            Type? classType = assembly.GetType(className);
            if (classType is not null)
            {
                var cItem = Activator.CreateInstance(classType, args);
                return cItem;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null)
                Console.WriteLine(ex.InnerException.Message);
        }
        finally
        {
            loadContext?.Unload();
        }

        return null;
    }

    public static object? ExecMethod(object obj, string methodName, object?[]? args)
    {
        var type = obj.GetType();

        var methodInfo = type.GetMethod(methodName);

        if (methodInfo is null)
            return null;

        try
        {
            return methodInfo.Invoke(obj, args);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }
}
