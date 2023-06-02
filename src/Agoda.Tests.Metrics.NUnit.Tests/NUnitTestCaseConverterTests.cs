using NUnit.Framework;
using Shouldly;

namespace Agoda.Tests.Metrics.NUnit.Tests;

[TestFixture]
public class NUnitTestCaseConverterTests
{
    [Test]
    public void WhenInitTestCaseConverter_ShouldPopulateListOfTestCasesCorrectly()
    {
        var report =
            @"<test-run id=""0"" name=""Agoda.TestingLib.Tests.dll"" fullname=""C:\source\TestNunitEvent\src\Agoda.TestingLib.Tests\bin\Debug\net6.0\Agoda.TestingLib.Tests.dll"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""0"" engine-version=""3.15.3.0"" clr-version=""6.0.15"" start-time=""2023-03-26 05:43:46Z"" end-time=""2023-03-26 05:43:46Z"" duration=""0.085810"">
	<command-line><![CDATA[C:\Users\myUser\AppData\Local\JetBrains\Installations\ReSharperPlatformVs17_cfc2aae1\TestRunner\netcoreapp3.0\ReSharperTestRunner.dll --parentProcessId 38540 -p 61838 -r e2d19589-94d1-4168-b342-3b18129cc640]]></command-line>
	<filter>
		<id>0-1000</id>
	</filter>
	<test-suite type=""Assembly"" id=""0-1002"" name=""Agoda.TestingLib.Tests.dll"" fullname=""C:/source/TestNunitEvent/src/Agoda.TestingLib.Tests/bin/Debug/net6.0/Agoda.TestingLib.Tests.dll"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2023-03-26T05:43:46.8009450Z"" end-time=""2023-03-26T05:43:46.8464532Z"" duration=""0.045445"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""0"">
		<environment framework-version=""3.13.3.0"" clr-version=""6.0.15"" os-version=""Microsoft Windows 10.0.19044"" platform=""Win32NT"" cwd=""C:\source\TestNunitEvent\src\Agoda.TestingLib.Tests\bin\Debug\net6.0"" machine-name=""6TW24J3"" user=""jdickson"" user-domain=""AGODA"" culture=""en-AU"" uiculture=""en-US"" os-architecture=""x64"" />
		<settings>
			<setting name=""ProcessModel"" value=""InProcess"" />
			<setting name=""DomainUsage"" value=""None"" />
			<setting name=""ShadowCopyFiles"" value=""False"" />
			<setting name=""TestParametersDictionary"" value="""" />
			<setting name=""NumberOfTestWorkers"" value=""0"" />
			<setting name=""SynchronousEvents"" value=""False"" />
			<setting name=""RandomSeed"" value=""361791836"" />
			<setting name=""LOAD"" value=""System.Collections.Generic.List`1[System.String]"" />
			<setting name=""WorkDirectory"" value=""C:\source\TestNunitEvent\src\Agoda.TestingLib.Tests\bin\Debug\net6.0"" />
		</settings>
		<properties>
			<property name=""_PID"" value=""51400"" />
			<property name=""_APPDOMAIN"" value=""ReSharperTestRunner"" />
		</properties>
		<test-suite type=""TestSuite"" id=""0-1003"" name=""Agoda"" fullname=""Agoda"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2023-03-26T05:43:46.8069136Z"" end-time=""2023-03-26T05:43:46.8464495Z"" duration=""0.039535"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""0"">
			<test-suite type=""TestSuite"" id=""0-1004"" name=""TestingLib"" fullname=""Agoda.TestingLib"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2023-03-26T05:43:46.8072958Z"" end-time=""2023-03-26T05:43:46.8464463Z"" duration=""0.039150"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""0"">
				<test-suite type=""TestSuite"" id=""0-1005"" name=""Tests"" fullname=""Agoda.TestingLib.Tests"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2023-03-26T05:43:46.8073083Z"" end-time=""2023-03-26T05:43:46.8464343Z"" duration=""0.039127"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""0"">
					<test-suite type=""TestFixture"" id=""0-1000"" name=""UnitTest1"" fullname=""Agoda.TestingLib.Tests.UnitTest1"" classname=""Agoda.TestingLib.Tests.UnitTest1"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2023-03-26T05:43:46.8073115Z"" end-time=""2023-03-26T05:43:46.8460458Z"" duration=""0.038735"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""0"">
						<test-case id=""0-1001"" name=""1WhenOneIsOne_ItSHouldBeOne"" fullname=""Agoda.TestingLib.Tests.UnitTest1.1WhenOneIsOne_ItSHouldBeOne"" methodname=""WhenOneIsOne_ItSHouldBeOne"" classname=""Agoda.TestingLib.Tests.UnitTest1"" runstate=""Runnable"" seed=""396657978"" result=""Passed"" start-time=""2023-03-26T05:43:46.8096530Z"" end-time=""2023-03-26T05:43:46.8428099Z"" duration=""0.033255"" asserts=""0"" />
						<test-case id=""0-1002"" name=""2WhenOneIsOne_ItSHouldBeOne"" fullname=""Agoda.TestingLib.Tests.UnitTest1.2WhenOneIsOne_ItSHouldBeOne"" methodname=""WhenOneIsOne_ItSHouldBeOne"" classname=""Agoda.TestingLib.Tests.UnitTest1"" runstate=""Runnable"" seed=""396657978"" result=""Passed"" start-time=""2023-03-26T05:43:46.8096530Z"" end-time=""2023-03-26T05:43:46.8428099Z"" duration=""0.033255"" asserts=""0"" />
					</test-suite>
				</test-suite>
				<test-suite type=""TestSuite"" id=""0-1005"" name=""Tests"" fullname=""Agoda.TestingLib.Tests2"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2023-03-26T05:43:46.8073083Z"" end-time=""2023-03-26T05:43:46.8464343Z"" duration=""0.039127"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""0"">
					<test-suite type=""TestFixture"" id=""0-1000"" name=""UnitTest1"" fullname=""Agoda.TestingLib.Tests.UnitTest2"" classname=""Agoda.TestingLib.Tests.UnitTest1"" runstate=""Runnable"" testcasecount=""1"" result=""Passed"" start-time=""2023-03-26T05:43:46.8073115Z"" end-time=""2023-03-26T05:43:46.8460458Z"" duration=""0.038735"" total=""1"" passed=""1"" failed=""0"" warnings=""0"" inconclusive=""0"" skipped=""0"" asserts=""0"">
						<test-case id=""0-1003"" name=""3WhenOneIsOne_ItSHouldBeOne"" fullname=""Agoda.TestingLib.Tests.UnitTest2.3WhenOneIsOne_ItSHouldBeOne"" methodname=""WhenOneIsOne_ItSHouldBeOne"" classname=""Agoda.TestingLib.Tests.UnitTest1"" runstate=""Runnable"" seed=""396657978"" result=""Passed"" start-time=""2023-03-26T05:43:46.8096530Z"" end-time=""2023-03-26T05:43:46.8428099Z"" duration=""0.033255"" asserts=""0"" />
						<test-case id=""0-1004"" name=""4WhenOneIsOne_ItSHouldBeOne"" fullname=""Agoda.TestingLib.Tests.UnitTest2.4WhenOneIsOne_ItSHouldBeOne"" methodname=""WhenOneIsOne_ItSHouldBeOne"" classname=""Agoda.TestingLib.Tests.UnitTest1"" runstate=""Runnable"" seed=""396657978"" result=""Passed"" start-time=""2023-03-26T05:43:46.8096530Z"" end-time=""2023-03-26T05:43:46.8428099Z"" duration=""0.033255"" asserts=""0"" />
					</test-suite>
				</test-suite>

			</test-suite>
		</test-suite>
	</test-suite>
</test-run>
";
        var result = new NUnitXmlEventConverter(report);
		result.TestCases.Count.ShouldBe(4);
        for (int i = 1; i <= 4; i++)
        {
            result.TestCases[i - 1].Name.ShouldBe($"{i}WhenOneIsOne_ItSHouldBeOne",customMessage:$"Name on {i}");
            result.TestCases[i - 1].Id.ShouldBe($"0-100{i}", customMessage: $"Id on {i}");
            result.TestCases[i - 1].Fullname.ShouldBe($"Agoda.TestingLib.Tests.UnitTest{(i+1) / 2}.{i}WhenOneIsOne_ItSHouldBeOne", customMessage: $"Fullname on {i}");
			result.TestCases[i - 1].Duration.ShouldBe(0.033255, customMessage: $"Duration on {i}");
        }
		
    }
}