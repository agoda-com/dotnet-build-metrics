using System.Net;
using System.Net.Sockets;
using Agoda.DevFeedback.Common;
using Shouldly;

namespace Agoda.Tests.Metrics.Tests;

[TestFixture]
[NonParallelizable]
public class DevFeedbackPublisherTokenTests
{
    private const string TokenEnvVar = "DEVFEEDBACK_TEST_TOKEN";
    private const string TokenHeader = "X-DevFeedback-Token";

    private string? _originalToken;
    private HttpListener _listener = null!;
    private string _endpoint = null!;

    [SetUp]
    public void SetUp()
    {
        _originalToken = Environment.GetEnvironmentVariable(TokenEnvVar);
        var port = GetFreePort();
        _endpoint = $"http://127.0.0.1:{port}/dotnet/nunit/";
        _listener = new HttpListener();
        _listener.Prefixes.Add(_endpoint);
        _listener.Start();
    }

    [TearDown]
    public void TearDown()
    {
        Environment.SetEnvironmentVariable(TokenEnvVar, _originalToken);
        _listener.Close();
    }

    [Test]
    public async Task Publish_WhenNUnitAndTokenSet_SendsHeader()
    {
        Environment.SetEnvironmentVariable(TokenEnvVar, "abc123");

        var headers = await PublishAndCaptureHeaders(DevLocalDataType.NUnit);

        headers.GetValues(TokenHeader).ShouldBe(new[] { "abc123" });
    }

    [TestCase("")]
    [TestCase("   ")]
    [TestCase(null)]
    public async Task Publish_WhenNUnitAndTokenBlank_SendsNoHeader(string? token)
    {
        Environment.SetEnvironmentVariable(TokenEnvVar, token);

        var headers = await PublishAndCaptureHeaders(DevLocalDataType.NUnit);

        headers.GetValues(TokenHeader).ShouldBeNull();
    }

    [Test]
    public async Task Publish_WhenBuildAndTokenSet_SendsNoHeader()
    {
        Environment.SetEnvironmentVariable(TokenEnvVar, "abc123");

        var headers = await PublishAndCaptureHeaders(DevLocalDataType.Build);

        headers.GetValues(TokenHeader).ShouldBeNull();
    }

    [Test]
    public async Task PublishAsync_WhenEndpointDoesNotRespond_TimesOut()
    {
        var publish = DevFeedbackPublisher.PublishAsync(_endpoint, new { }, DevLocalDataType.Build);
        var context = await _listener.GetContextAsync().WaitAsync(TimeSpan.FromSeconds(10));
        try
        {
            Assert.ThrowsAsync<TaskCanceledException>(async () => await publish.WaitAsync(TimeSpan.FromSeconds(15)));
        }
        finally
        {
            context.Response.Close();
        }
    }

    private async Task<System.Collections.Specialized.NameValueCollection> PublishAndCaptureHeaders(DevLocalDataType dataType)
    {
        var received = ReceiveOne();

        DevFeedbackPublisher.Publish(_endpoint, new { id = "contract-0001" }, dataType);

        return await received.WaitAsync(TimeSpan.FromSeconds(10));
    }

    private async Task<System.Collections.Specialized.NameValueCollection> ReceiveOne()
    {
        var context = await _listener.GetContextAsync();
        var headers = context.Request.Headers;
        context.Response.StatusCode = 200;
        context.Response.Close();
        return headers;
    }

    private static int GetFreePort()
    {
        var tcpListener = new TcpListener(IPAddress.Loopback, 0);
        tcpListener.Start();
        var port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;
        tcpListener.Stop();
        return port;
    }
}
