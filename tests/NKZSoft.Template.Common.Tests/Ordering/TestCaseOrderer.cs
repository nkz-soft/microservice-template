namespace NKZSoft.Template.Common.Tests.Ordering;

using System.Reflection;
using Xunit.Internal;
using Xunit.Sdk;
using Xunit.v3;

internal class TestCaseOrderer : ITestCaseOrderer
{
    public IReadOnlyCollection<TTestCase> OrderTestCases<TTestCase>(IReadOnlyCollection<TTestCase> testCases) where TTestCase : notnull, ITestCase
    {
        return testCases.OrderBy(testCase => GetTestMethodOrder(testCase)).CastOrToReadOnlyCollection();
    }

    private static int GetTestMethodOrder<TTestCase>(TTestCase testCase) where TTestCase : notnull, ITestCase
    {
        var testMethod = testCase.TestMethod as XunitTestMethod
                         ?? throw new InvalidOperationException("Error trying to figure out test method order.");
        return testMethod.Method.GetCustomAttribute<OrderAttribute>()?.Order
               ?? throw new InvalidOperationException("Error trying to figure out test method order.");
    }
}
