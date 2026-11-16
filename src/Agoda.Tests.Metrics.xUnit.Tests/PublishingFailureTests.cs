using System.Net;
using System.Net.Sockets;
using Agoda.Tests.Metrics.xUnit;

namespace Agoda.Tests.Metrics.xUnit.Tests;

public class PublishingFailureTests
{
    [Fact]
    public async Task Publish_WhenEndpointRejectsRequest_DoesNotThrow()
    {
        var socket = new TcpListener(IPAddress.Loopback, 0);
        socket.Start();
        var port = ((IPEndPoint)socket.LocalEndpoint).Port;
        socket.Stop();
        var endpoint = $"http://127.0.0.1:{port}/";
        using var listener = new HttpListener();
        listener.Prefixes.Add(endpoint);
        listener.Start();
        var previousEndpoint = Environment.GetEnvironmentVariable("NUNIT_METRICS_ES_ENDPOINT");
        Environment.SetEnvironmentVariable("NUNIT_METRICS_ES_ENDPOINT", endpoint);
        try
        {
            var builderType = typeof(AgodaTestFramework).Assembly.GetType("Agoda.Tests.Metrics.xUnit.TestResultsBuilder")!;
            var builder = Activator.CreateInstance(builderType, new object?[] { null })!;
            var publish = Task.Run(() => builderType.GetMethod("Publish")!.Invoke(builder, null));
            var serve = Task.Run(async () =>
            {
                while (listener.IsListening)
                {
                    HttpListenerContext context;
                    try { context = await listener.GetContextAsync(); }
                    catch (HttpListenerException) { break; }
                    catch (ObjectDisposedException) { break; }
                    context.Response.StatusCode = 403;
                    context.Response.Close();
                }
            });
            await publish.WaitAsync(TimeSpan.FromSeconds(10));
            listener.Stop();
            await serve;
        }
        finally
        {
            Environment.SetEnvironmentVariable("NUNIT_METRICS_ES_ENDPOINT", previousEndpoint);
        }
    }
}
