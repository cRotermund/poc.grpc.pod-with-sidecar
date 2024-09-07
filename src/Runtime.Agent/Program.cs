using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Runtime.Host.HostImpl;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.WebHost.ConfigureKestrel(opts =>
{
    opts.Listen(IPAddress.Any, 8888, listenOpts =>
    {
        listenOpts.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();
app.MapGrpcService<WorkDispatchChannelService>();
app.MapGet("/", () => "This is a GRPC server, foo!");

app.Run();
