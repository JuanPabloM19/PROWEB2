var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    string html = """
        <form action="/" method="post">
            <label for="nom">Nombre </label><br>
            <input type="text" id="nom" name="nom"/><br>
            <label for="apellido">Apellido </label><br>
            <input type="text" id="apellido" name="apellido"/>
            <input type="submit" value="enviar"/>
        </form>
      """;
    return Results.Content(html, "text/html");
});

app.MapPost("/", async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();

    string nombre = form["nom"].FirstOrDefault() ?? "";
    string apellido = form["apellido"].FirstOrDefault() ?? "";

    // Mostrar los valores en la consola
    //Console.WriteLine($"Nombre: {nombre}");
    //Console.WriteLine($"Apellido: {apellido}");
    string res = (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido)) ?  "Faltan datos" : "Formulario recibido correctamente!";

    // Responder al cliente
    await context.Response.WriteAsync(res);
});
app.Run();