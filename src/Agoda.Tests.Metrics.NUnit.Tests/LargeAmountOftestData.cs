using System.Collections;
using NUnit.Framework;

namespace Agoda.Tests.Metrics.NUnit.Tests;

public class LargeAmountOftestData
{
    public static IEnumerable TestCases
    {
        get
        {
            for (var i = 1; i <= 1000; i++)
            {
                yield return new TestCaseData(i);
            }
            
        }
    }
}