# Contributing to Dotnet Build and Test Metrics

First off, thanks for taking the time to contribute! You're already faster than an unoptimized build! 🚀

## The Zen of Dotnet Metrics

```
Fast is better than slow.
Measured is better than guessed.
Simple is better than complex.
Complex is better than complicated.
Flat hierarchies are better than nested ones.
Sparse matrices are better than dense ones.
Readability counts.
Special cases aren't special enough to break the rules.
Although practicality beats purity.
Errors should never pass silently.
Unless explicitly silenced.
In the face of ambiguity, refuse the temptation to guess.
There should be one-- and preferably only one --obvious way to do it.
Although that way may not be obvious at first unless you're a C# ninja.
Now is better than never.
Although never is often better than *right* now.
If the implementation is hard to explain, it's probably too slow.
If the implementation is easy to explain, it may be fast enough.
Namespaces are one honking great idea -- let's do more of those!
```

## Project Structure

This project is a monorepo containing tools for build metrics, startup metrics, and test metrics for both NUnit and xUnit. They're all here because we believe in the "one repo to rule them all" philosophy (and because it's just more convenient that way).

## The F5 Experience

Remember, we're all about that F5 Experience here. Our goal is to make the development process smoother than a baby's bottom. Here's what that means for contributors:

1. **Setup Should Be a Breeze**: You should be able to clone the repo and get up and running faster than you can say "dotnet restore".
2. **Fast Feedback Loop**: We want our builds and tests to run faster than a caffeinated squirrel. If you find yourself waiting, something's wrong.

## Pull Request Process

1. Ensure any install or build dependencies are removed before the end of the layer when doing a build. We don't want any sneaky slowdowns!
2. Update the README.md with details of changes to the interface, this includes new environment variables, exposed ports, useful file locations and container parameters. Documentation is like a love letter to your future self.
3. Increase the version numbers in any examples files and the README.md to the new version that this Pull Request would represent. We use SemVer, because we're not savages.
4. You may merge the Pull Request in once you have the sign-off of two other developers, or if you don't have permission to do that, you may request the second reviewer to merge it for you. No lone wolves here!

## Code of Conduct

In the interest of fostering an open and welcoming environment, we as contributors and maintainers pledge to making participation in our project and our community a harassment-free experience for everyone, regardless of age, body size, disability, ethnicity, gender identity and expression, level of experience, nationality, personal appearance, race, religion, or sexual identity and orientation.

Unless you're a `Console.WriteLine` debugger. Then all bets are off. (Just kidding, we love all developers equally. Even those who think `print` statements are a valid form of logging.)

## Testing

We believe in test-driven development. In fact, we believe in it so much that we've created tools to test our tests. It's like Inception, but with unit tests.

Remember: "Program testing can be used to show the presence of bugs, but never to show their absence!" - Edsger W. Dijkstra (But we can at least make sure those bugs are fast!)

## Code Review

All submissions, including submissions by project members, require review. We use GitHub pull requests for this purpose. Consult [GitHub Help](https://help.github.com/articles/about-pull-requests/) for more information on using pull requests.

During code review, if you see something that could be improved, don't just say "This could be faster." Instead, submit a pull request to make it faster. Be the change you want to see in the codebase.

## Documentation

Remember, code tells you how, comments tell you why. Please document your code as if the person who ends up maintaining it is a sleep-deprived developer who knows where you live. And remember, that person could be you after a long night of debugging.

## Performance

We're all about that F5 Experience, remember? If your code takes longer to run than it takes to microwave a burrito, it's probably not fast enough. Profile early, profile often. Your future self (and your future hungry self) will thank you.

## And finally...

Remember, in software development, there are only two types of projects: those that are measuring their metrics, and those that are still wondering why everything is so slow. Let's strive to be the former!

Happy coding, and may your builds be ever faster! 🚀