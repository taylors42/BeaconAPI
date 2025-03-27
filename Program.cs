var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", (HttpContext context) =>
{
    var remoteIp = context.Connection.RemoteIpAddress;

    // Handle IPv4-mapped IPv6 addresses
    if (remoteIp != null && remoteIp.IsIPv4MappedToIPv6)
    {
        remoteIp = remoteIp.MapToIPv4();
    }

    var ip = remoteIp?.ToString() ?? "unknown";

    // Convert IPv6 loopback to IPv4
    if (ip == "::1")
    {
        ip = "127.0.0.1";
    }

    return Results.Json(new { ip });
});

app.Run();
