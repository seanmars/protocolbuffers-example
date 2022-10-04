// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Google.Protobuf;

try
{
    var user = new User()
    {
        Id = 1000,
        Name = "User-1000"
    };

    if (File.Exists("data.dat"))
    {
        File.Delete("data.dat");
    }

    using (var writer = File.Create("data.dat"))
    {
        user.WriteTo(writer);
        Console.WriteLine($"Length: {writer.Length}");
    }

    using (var input = File.OpenRead("data.dat"))
    {
        var u = User.Parser.ParseFrom(input);
        Console.WriteLine(JsonSerializer.Serialize(u));
    }

    var descriptor = User.Descriptor;
    foreach (var field in descriptor.Fields.InDeclarationOrder())
    {
        Console.WriteLine(
            "Field {0} ({1}): {2}",
            field.FieldNumber,
            field.Name,
            field.Accessor.GetValue(user));
    }
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}