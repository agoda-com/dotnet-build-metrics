using Agoda.DevFeedback.Common;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Shouldly;

namespace Agoda.DevFeedback.Common.Tests
{
    [TestFixture()]
    public class GitContextReaderTests
    {
        [Test()]
        [TestCase("https://gitlab-ci-token:64_-_v7W68Q4uMG1sXIeh-W@gitlab.agodadev.io/full-stack/ycs/revenue-management", "64_-_v7W68Q4uMG1sXIeh-W")]
        // Note: this is not a real token, it is just a random string
        public void WhenUrlHasToken_ShouldCleanTokenFromUrl(string url, string token)
        {
            var result = GitContextReader.CleanGitlabCIToken(url);
            result.ShouldNotContain(token);
            result.ShouldNotContain("@");
            result.ShouldNotContain("gitlab-ci-token:");
        }

        [Test()]
        [TestCase("https://gitlab.agodadev.io/full-stack/ycs/revenue-management")]
        public void WhenGetRepoNameFromUrl_ShouldReturnTheGitlabNamespacePathForTheGitlabProject(string url)
        {
            GitContextReader.GetRepositoryNameFromUrl(url)
                .ShouldBe("full-stack/ycs/revenue-management");
        }
    }
}