# Dotnet Build and Test Metrics: Your F5 Experience, Quantified!

Welcome to the mono repo that's about to make your dotnet development experience smoother than a well-oiled CI/CD pipeline! We're all about measuring and improving your local dev experience, because who doesn't love a fast F5?

What is the F5 Experience? have a read [here](https://beerandserversdontmix.com/2024/08/15/an-introduction-to-the-f5-experience/)

## What's in the Box?

This repo is like a Swiss Army knife for .NET developers. We've got tools for:

1. Build Metrics: Measure your build times and kiss those coffee breaks goodbye!
2. Startup Metrics: Because waiting for your app to start is so last year.
3. Test Metrics: Know exactly how long your tests are taking (and why your coworker's tests are slower).

## Build and Startup Metrics: The Dynamic Duo

### Build Metrics: Time to Compile!

We've created a custom MSBuild task that measures the build time for each project and sends it off to a datastore. It's like a fitbit for your builds!

To use it, just install the [`Agoda.Builds.Metrics` NuGet package](https://www.nuget.org/packages/Agoda.Builds.Metrics) to all projects in your solution. It's easier than ordering pizza!

The task will do a HTTP POST to `http://compilation-metrics/dotnet` (or wherever you tell it to go). See exampel payload [here](examples/dotnetbuild.json).

| Parameter               | Environment Variable      | Default Value                     |
|-------------------------|---------------------------|-----------------------------------|
| Elastic Search Endpoint | BUILD_METRICS_ES_ENDPOINT | http://compilation-metrics/dotnet |

Pro tip: Set up a CNAME on your internal DNS servers for `compilation-metrics`. Your future self will thank you!

### Agoda.DevFeedback.AspNetStartup: Because Startup Time Matters

Why does startup time matter? read more [here](https://beerandserversdontmix.com/2024/08/15/the-f5-experience-local-setup/).

This package measures your ASP.NET application's startup time in two exciting flavors:

1. From `WebHostBuilder ConfigureServices` until `HostApplicationLifetime OnStarted` (type '.AspNetStartup')
2. From `WebHostBuilder ConfigureServices` until the first HTTP Response (type '.AspNetResponse')

To enable this magic, set these environment variables:

1. `ASPNETCORE_ENVIRONMENT` to `Development` (we promise not to slow down your production servers)
2. `ASPNETCORE_HOSTINGSTARTUPASSEMBLIES` to include `Agoda.DevFeedback.AspNetStartup`

You can set these in your `launchSettings.json`. It's like leaving a note for your future self!

## Test Metric Collection: Because Tests Should Be Fast Too!

We've got packages for both `xUnit` and `NUnit`. They're like personal trainers for your tests!

These packages will POST their data to `http://compilation-metrics/dotnet/nunit` (or wherever `BUILD_METRICS_ES_ENDPOINT` points to). See exampel payload [here](examples/dotnettests.json).

To install, just run:

For NUnit:
```bash
dotnet package add Agoda.Tests.Metrics.NUnit
```

For xUnit:
```bash
dotnet package add Agoda.Tests.Metrics.xUnit
```

It's so easy, you might accidentally make your tests faster!

## The F5 Experience

Remember, we're all about that F5 Experience here. Our goal is to make your development process smoother than a freshly waxed keyboard. Here's what that means for you:

1. **Setup Should Be a Breeze**: You should be able to clone the repo and get up and running faster than you can say "dotnet run".
2. **Fast Feedback Loop**: We want your builds, startups, and tests to run faster than a caffeinated squirrel. If you find yourself waiting, something's wrong.

## Contributing

We welcome contributions! Whether you're fixing bugs, improving documentation, or adding new features, we appreciate your help in making these tools even better. Check out our [Contributing Guide](CONTRIBUTING.md) for more details on how to get started.

Remember, in the world of Dotnet Build and Test Metrics, there are no stupid questions, only unoptimized build times!

## And Finally...

Remember, in software development, there are only two types of projects: those that are measuring their metrics, and those that are still wondering why everything is so slow. With these tools, you'll always know exactly where your time is going. (Spoiler alert: with our help, it won't be going to waste!)

Happy coding, and may your builds be ever faster! 🚀
